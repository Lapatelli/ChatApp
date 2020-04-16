using AutoMapper;
using ChatApp.Interfaces;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence;
using ChatApp.Persistence.Context;
using ChatApp.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;

namespace ChatApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetSection("ChatDatabaseSettings:ConnectionString").Value));
            services.AddScoped(s => new ChatDbContext(s.GetRequiredService<IMongoClient>(), Configuration.GetSection("ChatDatabaseSettings:DatabaseName").Value));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();

            services.AddAuthentication(options =>
                 {
                     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                     options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                     options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 })
                .AddCookie(options =>
                    {
                        options.LoginPath = new PathString("/Account/google-login");
                        options.Cookie.HttpOnly = true;
                    }
                )
                .AddGoogle(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = "158339939419-r3gh8lgsd3dp7nl1kihedga6q44080gk.apps.googleusercontent.com";
                    options.ClientSecret = "UrHalybWKgGiIooMBJzXMFVV";
                });

            services.AddMediatR(AppDomain.CurrentDomain.Load("ChatApp.CQRS"));

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Chat Api"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

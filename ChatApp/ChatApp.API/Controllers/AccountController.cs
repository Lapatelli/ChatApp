using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using ChatApp.CQRS.Commands.Users;
using ChatApp.CQRS.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            var user = await _mediator.Send(new RegisterUserCommand(model.FirstName, model.LastName, model.EmailAddress, model.Photo));

            var result = _mapper.Map<User, UserViewModel>(user);

            return Ok(result);
        }

        [HttpGet("google-login")]
        public IActionResult Login(string returnUrl)
        {
            return new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("signin-google", new { ReturnUrl = returnUrl })
                });
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> LoginCallback(string returnUrl = "~/")
        {
            var authenticateResult = await HttpContext.AuthenticateAsync();

            if (!authenticateResult.Succeeded)
                return BadRequest();

            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var isUserExist = await _mediator.Send(new GetUserByEmailQuery(email));

            if (!isUserExist)
            {
                var userGoogleId = authenticateResult.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var firstName = authenticateResult.Principal.FindFirstValue(ClaimTypes.GivenName);
                var lastName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Surname);
                var photo = authenticateResult.Principal.FindFirstValue("urn:google:picture");

                await _mediator.Send(new RegisterUserCommand(firstName, lastName, email, photo));
            }

            await _mediator.Send(new SetUserStatusCommand(email, UserStatus.Online));

            return Redirect("http://localhost:4200/main");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            await _mediator.Send(new SetUserStatusCommand(HttpContext.User.FindFirstValue(ClaimTypes.Email), UserStatus.Offline));

            await HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            return Ok(returnUrl);
        }
    }
}
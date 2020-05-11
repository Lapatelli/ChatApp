using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("google-login")]
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
        public async Task<IActionResult> LoginCallback(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            var authenticateResult = await HttpContext.AuthenticateAsync();

            if (!authenticateResult.Succeeded)
                return BadRequest();

            string email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var isUserExist = await _mediator.Send(new GetUserByEmailQuery(email));

            if (!isUserExist)
            {
                string firstName = authenticateResult.Principal.FindFirstValue(ClaimTypes.GivenName);
                string lastName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Surname);
                string telephoneNumber = authenticateResult.Principal.FindFirstValue(ClaimTypes.HomePhone);
                string photo = authenticateResult.Principal.FindFirstValue("urn:google:picture");

                await _mediator.Send(new RegisterUserCommand(firstName, lastName, email, telephoneNumber, photo));
            }

            await _mediator.Send(new SetUserStatusCommand(email, UserStatus.Online));

            return LocalRedirect(returnUrl);
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
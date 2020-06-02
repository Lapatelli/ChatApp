using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Users;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Infrastructure.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Controllers
{
    [Produces("application/json")]
    [Route("user/{userId}/")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;


        public UserController(IMapper mapper, IMediator mediator, IHubContext<ChatHub> hubContext)
        {
            _mapper = mapper;
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute]string userId)
        {
            var authenticateResult = HttpContext.AuthenticateAsync().Result.Principal.FindFirstValue(ClaimTypes.Email);

            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            var result = _mapper.Map<User, UserInfoViewModel>(user);

            return Ok(result);
        }

        [HttpGet("allusers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(users);

            return Ok(result);
        }

        [HttpGet("allchats")]
        public async Task<IActionResult> GetAllChatsForUserAsync([FromRoute]string userId)
        {
            var user = await _mediator.Send(new GetAllChatsForUserQuery(userId));

            var userChats = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatInfoViewModel>>(user.Chats);
            var result = _mapper.Map<(User, IEnumerable<ChatInfoViewModel>), UserViewModel>((user, userChats));

            return Ok(result.Chats);
        }

        [HttpGet("search/{userName}")]
        public async Task<IActionResult> GetUserByNameAsync(string userName)
        {
            var user = await _mediator.Send(new GetUserByNameQuery(userName));
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(user);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromForm] UpdateUserViewModel updateUserViewModel)
        {
            var userUpdateCommand = _mapper.Map<(UpdateUserViewModel, string), UpdateUserCommand>((updateUserViewModel, userId));
            var user = await _mediator.Send(userUpdateCommand);

            var result = _mapper.Map<User, UserViewModel>(user);

            return Ok(result);
        }

        [HttpPut("leavechat/{chatId}")]
        public async Task<IActionResult> LeaveChatAsync([FromRoute] string userId, [FromRoute] string chatId)
        {
            var user = await _mediator.Send(new LeaveChatCommand(userId, chatId));

            var result = _mapper.Map<User, UserViewModel>(user);

            return Ok(result);
        }
    }
}
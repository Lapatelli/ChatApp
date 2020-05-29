using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Users;
using ChatApp.CQRS.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("allusers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(users);

            return Ok(result);
        }

        [HttpGet("allchats")]
        [AllowAnonymous]
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

        [HttpPut("")]
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
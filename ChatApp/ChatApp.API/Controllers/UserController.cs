using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("users")]
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

            return Ok(result);
        }

        [HttpGet("name/{userName}")]
        public async Task<IActionResult> GetUserByNameAsync(string userName)
        {
            var user = await _mediator.Send(new GetUserByNameQuery(userName));
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(user);

            return Ok(result);
        }

        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            var result = _mapper.Map<User, UserViewModel>(user);

            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UpdateUserViewModel updateUserViewModel)
        {
            var userUpdateCommand = _mapper.Map<(UpdateUserViewModel, string), UpdateUserCommand>((updateUserViewModel, userId));
            var user = await _mediator.Send(userUpdateCommand);

            var result = _mapper.Map<User, UserViewModel>(user);

            return Ok(result);
        }
    }
}
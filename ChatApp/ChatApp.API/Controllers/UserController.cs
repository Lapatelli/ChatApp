using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Produces("application/json")]
    [Route("users")]
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


        [HttpGet, Route("")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserViewModel>>(users);

            return Ok(result);
        }

        [HttpGet, Route("name/{userName}")]
        public async Task<IActionResult> GetUserByNameAsync(string userName)
        {
            var user = await _mediator.Send(new GetUserByNameQuery(userName));
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserViewModel>>(user);


            return Ok(result);
        }

        [HttpGet, Route("id/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            var result = _mapper.Map<User, GetUserViewModel>(user);

            return Ok(result);
        }
    }
}
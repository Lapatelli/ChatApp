using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using ChatApp.Interfaces;
using ChatApp.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Produces("application/json")]
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet, Route("")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers();
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserViewModel>>(users);

            return Ok(result);
        }

        [HttpGet, Route("name/{userName}")]
        public async Task<IActionResult> GetUserByNameAsync(string userName)
        {
            var userSelected = await _unitOfWork.UserRepository.SearchUserByName(userName);
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserViewModel>>(userSelected);


            return Ok(result);
        }

        [HttpGet, Route("id/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var userSelected = await _unitOfWork.UserRepository.SearchUserById(userId);
            var result = _mapper.Map<User, GetUserViewModel>(userSelected);

            return Ok(result);
        }
    }
}
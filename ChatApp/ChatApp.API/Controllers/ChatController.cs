using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.Core.Entities;
using ChatApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChatController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet, Route("{chatName}")]
        public async Task<IActionResult> GetChatByNameAsync(string chatName)
        {
            var chat = await _unitOfWork.ChatRepository.SearchChatByName(chatName);
            var userCreator = await _unitOfWork.UserRepository.SearchUserById(chat.CreatedByUser.ToString());

            List<User> usersChat = new List<User>();
            foreach (var chatUsers in chat.ChatUsers)
            {
                usersChat.Add(await _unitOfWork.UserRepository.SearchUserById(chatUsers.ToString()));
            }

            var result = _mapper.Map<(Chat, User, IEnumerable<User>), GetChatViewModel>((chat, userCreator, usersChat));

            return Ok(result);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateChatAsync([FromBody] CreateChatViewModel createChatViewModel,[FromQuery] string userId)
        {
            var chat = _mapper.Map<CreateChatViewModel, Chat>(createChatViewModel);

            var chatCreated = await _unitOfWork.ChatRepository.CreateChatAsync(chat, userId, createChatViewModel.ChatUsers);
            var userCreator = await _unitOfWork.UserRepository.SearchUserById(userId);

            List<User> usersChat = new List<User>();
            foreach (var chatUsers in createChatViewModel.ChatUsers)
            {
                usersChat.Add(await _unitOfWork.UserRepository.SearchUserById(chatUsers));
            }

            var result = _mapper.Map<(Chat, User, IEnumerable<User>), GetChatViewModel>((chatCreated, userCreator, usersChat));

            return Ok(result);
        }


    }
}
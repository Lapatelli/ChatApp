﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Queries.Chats;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("chats")]
    [ApiController]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChatController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("search/{chatName}")]
        public async Task<IActionResult> GetChatByNameAsync(string chatName)
        {
            var chat = await _mediator.Send(new GetChatByNameQuery(chatName));

            var chatUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(chat.ChatUsers);
            var result = _mapper.Map<(Chat, IEnumerable<UserInfoViewModel>), ChatViewModel>((chat, chatUsers));

            return Ok(result);
        }
        
        [HttpPost("create", Name = "CreateChat")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateChatAsync([FromForm]CreateChatViewModel createChatViewModel,[FromQuery] string userId)
        {
            var chatCreationCommand = _mapper.Map<(CreateChatViewModel, string), CreateChatCommand>((createChatViewModel, userId));
            var chat = await _mediator.Send(chatCreationCommand);

            var chatUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(chat.ChatUsers);
            var result = _mapper.Map<(Chat, IEnumerable<UserInfoViewModel>), ChatViewModel>((chat, chatUsers));

            return Ok(result);
        }

        [HttpPut("{chatId}/update")]
        public async Task<IActionResult> UpdateChatAsync([FromForm] UpdateChatViewModel model, [FromRoute] string chatId)
        {
            var updateChatCommand = _mapper.Map<(UpdateChatViewModel, string), UpdateChatCommand>((model, chatId));
            var chat = await _mediator.Send(updateChatCommand);

            var chatUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(chat.ChatUsers);
            var result = _mapper.Map<(Chat, IEnumerable<UserInfoViewModel>), ChatViewModel>((chat, chatUsers));

            return Ok(result);
        }

        [HttpPut("{chatId}/deleteuser")]
        public async Task<IActionResult> DeleteUserFromChatAsync([FromBody] DeleteUserFromChatViewModel model, [FromRoute] string chatId)
        {
            var deleteUserFromChatCommand = _mapper.Map<(DeleteUserFromChatViewModel, string), DeleteUserFromChatCommand>((model, chatId));
            var chat = await _mediator.Send(deleteUserFromChatCommand);

            var chatUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(chat.ChatUsers);
            var result = _mapper.Map<(Chat, IEnumerable<UserInfoViewModel>), ChatViewModel>((chat, chatUsers));

            return Ok(result);
        }

        [HttpPut("{chatId}/adduser")]
        public async Task<IActionResult> AddUserToChatAsync([FromBody] AddUserToChatViewModel model, [FromRoute] string chatId)
        {
            var addUserToChatCommand = _mapper.Map<(AddUserToChatViewModel, string), AddUserToChatCommand>((model, chatId));
            var chat = await _mediator.Send(addUserToChatCommand);

            var chatUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(chat.ChatUsers);
            var result = _mapper.Map<(Chat, IEnumerable<UserInfoViewModel>), ChatViewModel>((chat, chatUsers));

            return Ok(result);
        }

        [HttpDelete("{chatId}/delete")]
        public async Task<IActionResult> DeleteChatAsync([FromRoute] string chatId)
        {
            await _mediator.Send(new DeleteChatCommand(chatId));
            return NoContent();
        }
    }
}
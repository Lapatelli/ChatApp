using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Queries.Chats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChatController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet, Route("{chatName}")]
        public async Task<IActionResult> GetChatByNameAsync(string chatName)
        {
            var chat = await _mediator.Send(new GetChatByNameQuery(chatName));
            var result = _mapper.Map<Chat, ChatViewModel>((chat));

            return Ok(result);
        }
        
        [HttpPost, Route("")]
        public async Task<IActionResult> CreateChatAsync([FromBody] CreateChatViewModel createChatViewModel,[FromQuery] string userId)
        {
            var chatCreationCommand = _mapper.Map<(CreateChatViewModel, string), CreateChatCommand>((createChatViewModel, userId));
            var chat = await _mediator.Send(chatCreationCommand);

            var result = _mapper.Map<Chat, ChatViewModel>(chat);

            return Ok(result);
        }


    }
}
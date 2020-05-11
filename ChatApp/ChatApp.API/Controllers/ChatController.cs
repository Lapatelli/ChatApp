using System.Threading.Tasks;
using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Queries.Chats;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
            var result = _mapper.Map<Chat, ChatViewModel>((chat));

            return Ok(result);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateChatAsync([FromBody] CreateChatViewModel createChatViewModel,[FromQuery] string userId)
        {
            var chatCreationCommand = _mapper.Map<(CreateChatViewModel, string), CreateChatCommand>((createChatViewModel, userId));
            var chat = await _mediator.Send(chatCreationCommand);

            var result = _mapper.Map<Chat, ChatViewModel>(chat);

            return Ok(result);
        }

        [HttpPut("update/{chatId}")]
        public async Task<IActionResult> UpdateChatAsync([FromBody] UpdateChatViewModel model, [FromRoute] string chatId)
        {
            var updateChatCommand = _mapper.Map<(UpdateChatViewModel, string), UpdateChatCommand>((model, chatId));
            var chat = await _mediator.Send(updateChatCommand);

            var result = _mapper.Map<Chat, ChatViewModel>(chat);

            return Ok(result);
        }

        [HttpPut("deleteuser/{chatId}")]
        public async Task<IActionResult> DeleteUserFromChatAsync([FromBody] DeleteUserFromChatViewModel model, [FromRoute] string chatId)
        {
            var deleteUserFromChatCommand = _mapper.Map<(DeleteUserFromChatViewModel, string), DeleteUserFromChatCommand>((model, chatId));
            var chat = await _mediator.Send(deleteUserFromChatCommand);

            var result = _mapper.Map<Chat, ChatViewModel>(chat);

            return Ok(result);
        }

        [HttpPut("adduser/{chatId}")]
        public async Task<IActionResult> AddUserToChatAsync([FromBody] AddUserToChatViewModel model, [FromRoute] string chatId)
        {
            var addUserToChatCommand = _mapper.Map<(AddUserToChatViewModel, string), AddUserToChatCommand>((model, chatId));
            var chat = await _mediator.Send(addUserToChatCommand);

            var result = _mapper.Map<Chat, ChatViewModel>(chat);

            return Ok(result);
        }

        [HttpDelete("delete/{chatId}")]
        public async Task<IActionResult> DeleteChatAsync([FromRoute] string chatId)
        {
            await _mediator.Send(new DeleteChatCommand(chatId));
            return NoContent();
        }
    }
}
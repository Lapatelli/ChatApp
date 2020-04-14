using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class UpdateChatHandler : IRequestHandler<UpdateChatCommand, Chat>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateChatHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Chat> Handle(UpdateChatCommand command, CancellationToken cancellationToken)
        {
            List<User> usersChat = new List<User>();

            var chatUpdateDTO = _mapper.Map<UpdateChatCommand, ChatDTO>(command);
            var chat = await _unitOfWork.ChatRepository.UpdateChatAsync(chatUpdateDTO);
            var userCreator = await _mediator.Send(new GetUserByIdQuery(chat.CreatedByUser.ToString()));

            foreach (var chatUsers in chat.ChatUsers)
            {
                usersChat.Add(await _mediator.Send(new GetUserByIdQuery(chatUsers.ToString())));
            }

            var result = _mapper.Map<(ChatDTO, User, IEnumerable<User>), Chat>((chat, userCreator, usersChat));

            return result;
        }
    }
}

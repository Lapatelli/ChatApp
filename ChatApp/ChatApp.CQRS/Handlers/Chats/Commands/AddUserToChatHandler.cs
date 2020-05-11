﻿using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class AddUserToChatHandler : IRequestHandler<AddUserToChatCommand, Chat>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserToChatHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Chat> Handle(AddUserToChatCommand command, CancellationToken cancellationToken)
        {
            List<User> usersChat = new List<User>();

            var chat = await _unitOfWork.ChatRepository.AddUserToChatAsync(command.ChatId, command.UserId);
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
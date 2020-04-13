using AutoMapper;
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
    public class CreateChatHandler : IRequestHandler<CreateChatCommand, (Chat, User, IEnumerable<User>)>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateChatHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<(Chat, User, IEnumerable<User>)> Handle(CreateChatCommand command, CancellationToken cancellationToken)
        {
            var chat = _mapper.Map<CreateChatCommand, Chat>(command);
            var chatCreated = await _unitOfWork.ChatRepository.CreateChatAsync(chat, command.CreatedByUser, command.ChatUsers);

            var userCreator = await _mediator.Send(new GetUserByIdQuery(chatCreated.CreatedByUser.ToString()));

            List<User> usersChat = new List<User>();
            foreach (var chatUsers in chatCreated.ChatUsers)
            {
                usersChat.Add(await _mediator.Send(new GetUserByIdQuery(chatUsers.ToString())));
            }

            return (chatCreated, userCreator, usersChat);
        }
    }
}

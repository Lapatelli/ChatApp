using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class CreateChatHandler : IRequestHandler<CreateChatCommand, Chat>
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

        public async Task<Chat> Handle(CreateChatCommand command, CancellationToken cancellationToken)
        {
            List<ObjectId> chatUsersObjectId = new List<ObjectId>();
            List<User> usersChat = new List<User>();

            foreach (var chatUserObjectId in command.ChatUsers)
            {
                chatUsersObjectId.Add(ObjectId.Parse(chatUserObjectId));
            }

            var chat = _mapper.Map<(CreateChatCommand, List<ObjectId>), ChatDTO>((command, chatUsersObjectId));

            var chatCreated = await _unitOfWork.ChatRepository.CreateChatAsync(chat);
            var userCreator = await _mediator.Send(new GetUserByIdQuery(chatCreated.CreatedByUser.ToString()));


            foreach (var chatUsers in chatCreated.ChatUsers)
            {
                usersChat.Add(await _mediator.Send(new GetUserByIdQuery(chatUsers.ToString())));
            }

            var result = _mapper.Map<(ChatDTO, User, List<User>), Chat>((chatCreated, userCreator, usersChat));

            return result;
        }
    }
}

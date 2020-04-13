using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Chats;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Queries
{
    public class GetChatByNameHandler : IRequestHandler<GetChatByNameQuery, Chat>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public GetChatByNameHandler (IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Chat> Handle(GetChatByNameQuery query, CancellationToken cancellationToken)
        {

            var chat = await _unitOfWork.ChatRepository.SearchChatByName(query.Name);

            var userCreator = await _mediator.Send(new GetUserByIdQuery(chat.CreatedByUser.ToString()));

            List<User> usersChat = new List<User>();
            foreach (var chatUsers in chat.ChatUsers)
            {
                usersChat.Add(await _mediator.Send(new GetUserByIdQuery(chatUsers.ToString())));
            }

            var result = _mapper.Map<(ChatDTO, User, IEnumerable<User>), Chat>((chat, userCreator, usersChat));

            return result;
        }
    }
}

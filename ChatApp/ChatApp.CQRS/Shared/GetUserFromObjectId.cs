using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Shared
{
    public class GetUserFromObjectId
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserFromObjectId(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsers(IEnumerable<ObjectId> chatUsers)
        {
            List<User> usersChat = new List<User>();

            foreach (var chatUser in chatUsers)
            {
                usersChat.Add(await _mediator.Send(new GetUserByIdQuery(chatUser.ToString())));
            }

            return usersChat;

            //var result = _mapper.Map<(ChatDTO, User, List<User>), Chat>((chat, userCreator, usersChat));

            //return result;
        }
    }
}

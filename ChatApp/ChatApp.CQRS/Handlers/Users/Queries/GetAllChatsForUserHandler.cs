using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Queries
{
    public class GetAllChatsForUserHandler : IRequestHandler<GetAllChatsForUserQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllChatsForUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> Handle(GetAllChatsForUserQuery query, CancellationToken cancellationToken)
        {
            var userWithChatsDTO = await _unitOfWork.UserRepository.GetAllChatsForUser(query.Email);
            var userWithChats = _mapper.Map<IEnumerable<ChatDTO>, IEnumerable<Chat>>(userWithChatsDTO.Chats);
            var result = _mapper.Map<(UserWithChatsDTO, IEnumerable<Chat>), User>((userWithChatsDTO , userWithChats));

            return result;
        }
    }
}

using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Chats;
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
        private readonly IMapper _mapper; 

        public GetChatByNameHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Chat> Handle(GetChatByNameQuery query, CancellationToken cancellationToken)
        {
            var chatWithUsersDTO = await _unitOfWork.ChatRepository.SearchChatByName(query.Name);
            var chatUsers = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(chatWithUsersDTO.ChatUsers);
            var result = _mapper.Map<(ChatWithUsersDTO, IEnumerable<User>), Chat>((chatWithUsersDTO, chatUsers));

            return result;
        }
    }
}

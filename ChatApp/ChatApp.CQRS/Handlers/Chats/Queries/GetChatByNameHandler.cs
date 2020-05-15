using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Chats;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Queries
{
    public class GetChatByNameHandler : IRequestHandler<GetChatByNameQuery, Chat>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChatByNameHandler (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Chat> Handle(GetChatByNameQuery query, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatRepository.SearchChatByName(query.Name);

            return chat;
        }
    }
}

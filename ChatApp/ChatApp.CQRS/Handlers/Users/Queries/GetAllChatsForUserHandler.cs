using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Queries
{
    public class GetAllChatsForUserHandler : IRequestHandler<GetAllChatsForUserQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllChatsForUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetAllChatsForUserQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UserRepository.GetAllChatsForUser(ObjectId.Parse(query.Id));

            return result;
        }
    }
}

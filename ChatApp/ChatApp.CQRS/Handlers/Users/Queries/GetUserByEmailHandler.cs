using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Queries
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByEmailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.IsUserExist(query.Email);
        }
    }
}

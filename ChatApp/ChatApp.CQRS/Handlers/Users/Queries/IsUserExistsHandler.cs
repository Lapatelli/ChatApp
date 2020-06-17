using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Queries
{
    public class IsUserExistsHandler : IRequestHandler<IsUserExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IsUserExistsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(IsUserExistsQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.IsUserExist(query.Email);
        }
    }
}

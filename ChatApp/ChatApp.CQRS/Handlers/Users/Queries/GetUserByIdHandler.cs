using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Queries.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {

             var user = await _unitOfWork.UserRepository.SearchUserById(query.Id);

             if (user == null)
             {
                 throw new Exception($"User with id {query.Id} does not exist");
             }

             return user;
        }
    }
}

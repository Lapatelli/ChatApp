using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Queries
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers();

            if (users == null)
            {
                throw new Exception("Bad Request");
            }

            return users;
        }
    }
}

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
    public class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> Handle(GetUserByNameQuery query, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.UserRepository.SearchUserByName(query.Name);

            if (user == null)
            {
                throw new Exception($"User with name {query.Name} does not exist");
            }

            return user;
        }
    }
}

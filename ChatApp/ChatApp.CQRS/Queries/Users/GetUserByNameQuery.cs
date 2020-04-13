using ChatApp.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetUserByNameQuery : IRequest<IEnumerable<User>>
    {
        public GetUserByNameQuery(string userName)
        {
            Name = userName;
        }

        public string Name { get; set; }
    }
}

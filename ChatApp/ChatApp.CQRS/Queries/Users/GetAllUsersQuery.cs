using ChatApp.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}

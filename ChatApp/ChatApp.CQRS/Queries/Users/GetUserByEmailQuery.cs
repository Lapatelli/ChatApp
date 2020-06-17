using ChatApp.Core.Entities;
using MediatR;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetUserByEmailQuery : IRequest<User>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}

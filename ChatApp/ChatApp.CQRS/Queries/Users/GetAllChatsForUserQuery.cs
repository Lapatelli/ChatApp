using ChatApp.Core.Entities;
using MediatR;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetAllChatsForUserQuery : IRequest<User>
    {
        public GetAllChatsForUserQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}

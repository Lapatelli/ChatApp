using ChatApp.Core.Entities;
using MediatR;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetAllChatsForUserQuery : IRequest<User>
    {
        public GetAllChatsForUserQuery(string userId)
        {
            Id = userId;
        }

        public string Id { get; set; }
    }
}

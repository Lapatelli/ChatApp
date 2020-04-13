using ChatApp.Core.Entities;
using MediatR;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public GetUserByIdQuery(string userId)
        {
            Id = userId;
        }

        public string Id { get; set; }
    }
}

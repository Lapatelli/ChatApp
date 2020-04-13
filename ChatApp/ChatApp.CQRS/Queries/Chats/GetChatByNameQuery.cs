using ChatApp.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace ChatApp.CQRS.Queries.Chats
{
    public class GetChatByNameQuery : IRequest<(Chat, User, IEnumerable<User>)>
    {
        public GetChatByNameQuery(string userName)
        {
            Name = userName;
        }

        public string Name { get; set; }
    }
}

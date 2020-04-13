using ChatApp.Core.Entities;
using MediatR;

namespace ChatApp.CQRS.Queries.Chats
{
    public class GetChatByNameQuery : IRequest<Chat>
    {
        public GetChatByNameQuery(string chatName)
        {
            Name = chatName;
        }

        public string Name { get; set; }
    }
}

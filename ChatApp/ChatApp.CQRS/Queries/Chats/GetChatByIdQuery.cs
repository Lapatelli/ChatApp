using ChatApp.Core.Entities;
using MediatR;

namespace ChatApp.CQRS.Queries.Chats
{
    public class GetChatByIdQuery : IRequest<Chat>
    {
        public GetChatByIdQuery(string chatId)
        {
            Id = chatId;
        }

        public string Id { get; set; }
    }
}

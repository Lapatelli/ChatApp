using MediatR;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Chats
{
    public class DeleteChatCommand : IRequest
    {
        public DeleteChatCommand(string chatId)
        {
            ChatId = ObjectId.Parse(chatId);
        }

        public ObjectId ChatId { get; set; }
    }
}

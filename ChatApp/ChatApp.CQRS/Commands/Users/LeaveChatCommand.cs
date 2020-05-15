using ChatApp.Core.Entities;
using MediatR;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Users
{
    public class LeaveChatCommand : IRequest<User>
    {
        public LeaveChatCommand(string userId, string chatId)
        {
            UserId = ObjectId.Parse(userId);
            ChatId = ObjectId.Parse(chatId);
        }

        public ObjectId UserId { get; set; }
        public ObjectId ChatId { get; set; }
    }
}

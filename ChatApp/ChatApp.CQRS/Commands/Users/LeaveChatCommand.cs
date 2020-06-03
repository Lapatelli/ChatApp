using ChatApp.Core.Entities;
using MediatR;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Users
{
    public class LeaveChatCommand : IRequest<User>
    {
        public LeaveChatCommand(string email, string chatId)
        {
            Email = email;
            ChatId = ObjectId.Parse(chatId);
        }

        public string Email { get; set; }
        public ObjectId ChatId { get; set; }
    }
}

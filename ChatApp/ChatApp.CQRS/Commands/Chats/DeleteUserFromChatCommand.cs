using ChatApp.Core.Entities;
using MediatR;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Chats
{
    public class DeleteUserFromChatCommand : IRequest<Chat>
    {
        public ObjectId UserId { get; set; }
        public ObjectId ChatId { get; set; }
    }
}

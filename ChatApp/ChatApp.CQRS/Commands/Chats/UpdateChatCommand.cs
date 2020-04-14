using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using MediatR;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Chats
{
    public class UpdateChatCommand : IRequest<Chat>
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy ChatPrivacy { get; set; }
    }
}

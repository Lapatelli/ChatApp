using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ChatApp.CQRS.Commands.Chats
{
    public class UpdateChatCommand : IRequest<Chat>
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy? ChatPrivacy { get; set; }
        public List<string> ChatUsers { get; set; }
        public IFormFile Picture { get; set; }
    }
}

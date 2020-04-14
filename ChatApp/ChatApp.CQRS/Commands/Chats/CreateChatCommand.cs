using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace ChatApp.CQRS.Commands.Chats
{
    public class CreateChatCommand : IRequest<Chat>
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedByUser { get; set; }
        public ChatPrivacy ChatPrivacy { get; set; }
        public IEnumerable<string> ChatUsers { get; set; }

        //add Picture property
    }
}

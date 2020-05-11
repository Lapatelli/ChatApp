using ChatApp.Core.Entities;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ChatApp.CQRS.Commands.Users
{
    public class UpdateUserCommand : IRequest<User>
    {
        public UpdateUserCommand(ObjectId id, string createdChats, ObjectId chat)
        {
            Id = id;
            Chats.Add(chat);
        }

        public UpdateUserCommand(ObjectId id, ObjectId createdChats)
        {
            Id = id;
            CreatedChats.Add(createdChats);
        }

        public UpdateUserCommand()
        {
        }

        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public List<ObjectId> CreatedChats { get; set; }
        public List<ObjectId> Chats { get; set; }
    }
}

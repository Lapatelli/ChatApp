using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ChatApp.CQRS.Commands.Users
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegisterUserCommand(string firstName, string lastName, string emailAddress, string photo)
        {
            Id = ObjectId.GenerateNewId();
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Photo = photo;
            CreatedChatsId = new List<ObjectId>();
            ChatsId = new List<ObjectId>();
            UserStatus = UserStatus.Online;
        }

        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<ObjectId> CreatedChatsId { get; set; }
        public List<ObjectId> ChatsId { get; set; }
        public string Photo { get; set; }
    }
}

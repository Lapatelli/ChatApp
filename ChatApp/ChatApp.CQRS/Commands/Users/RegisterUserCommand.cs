using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using MediatR;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Users
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegisterUserCommand(string firstName, string lastName, string emailAddress, string telephoneNumber, string photo)
        {
            Id = ObjectId.GenerateNewId();
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            TelephoneNumber = telephoneNumber;
            Photo = photo;
            UserStatus = UserStatus.Online;
        }

        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public UserStatus UserStatus { get; set; }
        public string Photo { get; set; }
    }
}

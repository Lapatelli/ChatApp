using ChatApp.Core.Entities;
using ChatApp.Core.Enums;
using MediatR;

namespace ChatApp.CQRS.Commands.Users
{
    public class SetUserStatusCommand : IRequest<User>
    {
        public SetUserStatusCommand(string emailAddress, UserStatus userStatus)
        {
            UserStatus = userStatus;
            EmailAddress = emailAddress;
        }

        public UserStatus UserStatus { get; set; }
        public string EmailAddress { get; set; }
    }
}

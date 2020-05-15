using ChatApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace ChatApp.CQRS.Commands.Users
{
    public class UpdateUserCommand : IRequest<User>
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IFormFile Photo { get; set; }
    }
}

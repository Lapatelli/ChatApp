using ChatApp.Core.Enums;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserStatus UserStatus { get; set; }
        public byte[] BytePhoto { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
    }
}

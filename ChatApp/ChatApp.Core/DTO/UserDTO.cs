using ChatApp.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ChatApp.Core.DTO
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("EmailAddress")]
        public string EmailAddress { get; set; }

        [BsonElement("TelephoneNumber")]
        public string TelephoneNumber { get; set; }

        [BsonElement("CreatedChats")]
        public IEnumerable<ObjectId> CreatedChats { get; set; }

        [BsonElement("Chats")]
        public IEnumerable<ObjectId> Chats { get; set; }

        [BsonElement("UserStatus")]
        public UserStatus UserStatus { get; set; }

        // Add Photo Property
    }
}

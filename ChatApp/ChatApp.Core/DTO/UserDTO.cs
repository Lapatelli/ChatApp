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

        [BsonElement("CreatedChatsId")]
        public List<ObjectId> CreatedChatsId { get; set; }

        [BsonElement("ChatsId")]
        public List<ObjectId> ChatsId { get; set; }

        [BsonElement("UserStatus")]
        public UserStatus UserStatus { get; set; }

        [BsonElement("Photo")]
        public string Photo { get; set; }

        [BsonElement("BytePhoto")]
        public byte[] BytePhoto { get; set; }
    }
}

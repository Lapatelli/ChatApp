using ChatApp.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ChatApp.Core.DTO
{
    public class ChatDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("CreatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("CreatedBy")]
        public ObjectId CreatedByUser { get; set; }

        [BsonElement("ChatPrivacy")]
        public ChatPrivacy? ChatPrivacy { get; set; }

        [BsonElement("ChatUsers")]
        public List<ObjectId> ChatUsers { get; set; }

        [BsonElement("Picture")]
        public byte[] Picture { get; set; }
    }
}

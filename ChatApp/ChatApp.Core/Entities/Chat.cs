﻿using ChatApp.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class Chat
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
        public ChatPrivacy ChatPrivacy { get; set; }

        [BsonElement("ChatUsers")]
        public IEnumerable<ObjectId> ChatUsers { get; set; }

        //add Picture property
    }
}

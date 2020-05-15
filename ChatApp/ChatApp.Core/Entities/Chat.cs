using ChatApp.Core.Enums;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class Chat
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy? ChatPrivacy { get; set; }
        public byte[] Picture { get; set; }
        public IEnumerable<User> ChatUsers { get; set; }
    }
}

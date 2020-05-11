using ChatApp.Core.DTO;
using ChatApp.Core.Enums;
using System;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class Chat : ChatDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public User CreatedByUser { get; set; }
        public ChatPrivacy? ChatPrivacy { get; set; }
        public List<User> ChatUsers { get; set; }
        public byte[] Picture { get; set; }
    }
}

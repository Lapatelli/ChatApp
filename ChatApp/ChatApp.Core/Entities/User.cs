using ChatApp.Core.Enums;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public List<Chat> CreatedChats { get; set; }
        public List<Chat> Chats { get; set; }
        public UserStatus UserStatus { get; set; }
        public string Photo { get; set; }
    }
}

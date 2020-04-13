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
        public IEnumerable<Chat> CreatedChats { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public UserStatus UserStatus { get; set; }

        // Add Photo Property
    }
}

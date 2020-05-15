using ChatApp.API.ViewModels.Chat;
using ChatApp.Core.Enums;
using System.Collections.Generic;

namespace ChatApp.API.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserStatus UserStatus { get; set; }
        public IEnumerable<ChatInfoViewModel> Chats { get; set; }
        public byte[] BytePhoto {get; set;}
    }
}

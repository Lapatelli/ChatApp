using ChatApp.Core.Enums;
using System.Collections.Generic;
using ChatApp.Core.Entities;

namespace ChatApp.API.ViewModels.User
{
    public class GetUserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string TelephoneNumber { get; set; }

        public IEnumerable<Core.Entities.Chat> CreatedChats { get; set; }

        public IEnumerable<Core.Entities.Chat> Chats { get; set; }

        public UserStatus UserStatus { get; set; }
    }
}

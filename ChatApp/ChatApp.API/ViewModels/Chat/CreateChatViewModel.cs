using ChatApp.Core.Enums;
using System.Collections.Generic;

namespace ChatApp.API.ViewModels.Chat
{
    public class CreateChatViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy ChatPrivacy { get; set; }
        public IEnumerable<string> ChatUsers { get; set; }
    }
}

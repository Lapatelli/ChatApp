using ChatApp.API.ViewModels.User;
using ChatApp.Core.Enums;
using System.Collections.Generic;

namespace ChatApp.API.ViewModels.Chat
{
    public class ChatViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy? ChatPrivacy { get; set; }
        public IEnumerable<UserInfoViewModel> ChatUsers { get; set; }
        public byte[] Picture { get; set; }
    }
}

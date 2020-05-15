using ChatApp.Core.Enums;

namespace ChatApp.API.ViewModels.Chat
{
    public class ChatInfoViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy? ChatPrivacy { get; set; }
        public byte[] Picture { get; set; }
    }
}

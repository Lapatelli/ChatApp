using ChatApp.Core.Enums;

namespace ChatApp.API.ViewModels.Chat
{
    public class UpdateChatViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy ChatPrivacy { get; set; }

        //public IEnumerable<User> ChatUsers { get; set; }
    }
}

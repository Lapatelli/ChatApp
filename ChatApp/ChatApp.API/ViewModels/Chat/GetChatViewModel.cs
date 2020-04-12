using System.Collections.Generic;

namespace ChatApp.API.ViewModels.Chat
{
    public class GetChatViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Core.Entities.User CreatedBy { get; set; }
        public IEnumerable<Core.Entities.User> ChatUsers { get; set; }
    }
}

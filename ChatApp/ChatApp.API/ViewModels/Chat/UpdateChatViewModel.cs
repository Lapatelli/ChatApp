using ChatApp.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ChatApp.API.ViewModels.Chat
{
    public class UpdateChatViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public ChatPrivacy? ChatPrivacy { get; set; }
        public List<string> ChatUsers { get; set; }
        public IFormFile Picture { get; set; }
    }
}

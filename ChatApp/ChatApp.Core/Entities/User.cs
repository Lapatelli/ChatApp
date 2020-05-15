using ChatApp.Core.DTO;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class User : UserDTO
    {
        public List<ChatDTO> CreatedChats { get; set; }
        public List<ChatDTO> Chats { get; set; }
    }
}

using System.Collections.Generic;

namespace ChatApp.Core.DTO
{
    public class UserWithChatsDTO : UserDTO
    {
        public List<ChatDTO> Chats { get; set; }
    }
}

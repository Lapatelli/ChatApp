using System.Collections.Generic;

namespace ChatApp.Core.DTO
{
    public class ChatWithUsersDTO : ChatDTO
    {
        public List<UserDTO> ChatUsers { get; set; }
    }
}

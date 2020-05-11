using ChatApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Core.DTO
{
    public class ChatsWithUsers : ChatDTO
    {
        public List<UserDTO> CreatedByUser { get; set; }
        public List<UserDTO> ChatUsersFull { get; set; }
    }
}

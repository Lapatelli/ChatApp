using ChatApp.Core.DTO;
using System.Collections.Generic;

namespace ChatApp.Core.Entities
{
    public class Chat : ChatDTO
    {
        public List<UserDTO> CreatedByUser { get; set; }
        public List<UserDTO> ChatUsers { get; set; }
    }
}

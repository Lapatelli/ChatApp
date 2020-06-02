using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Core.Entities
{
    public class Message
    {
        public string Content { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
    }
}

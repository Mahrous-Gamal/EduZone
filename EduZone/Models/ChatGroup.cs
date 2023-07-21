using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class ChatGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public bool IsImage { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string time { get; set; }
        public string MessageContant { get; set; }
    }
}
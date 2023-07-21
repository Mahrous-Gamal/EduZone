using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string userId { get; set; }
        public string SenderId { get; set; }
        public string GroupCode { get; set; }
        public DateTime TimeOfNotify { get; set; }
        public string TypeOfPost { get; set; }
        public bool IsReaded { get; set; }


    }
}
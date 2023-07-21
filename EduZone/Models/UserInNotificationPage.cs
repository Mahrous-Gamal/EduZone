using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class UserInNotificationPage
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConnectionID { get; set; }
        public DateTime TimeOfLastOpen { get; set; }

    }
}
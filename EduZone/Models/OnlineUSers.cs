using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class OnlineUSers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConnectionID { get; set; }
        public string ReseverId { get; set; }
        public DateTime TimeOfOpen { get; set; }
    }
}
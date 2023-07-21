using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models.ViewModels
{
    public class UsersAndLastSeenViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string TimeOfLastSeenStr { get; set; }
        public DateTime TimeOfLastSeenTi { get; set; }
        public string OnLineOrNot { get; set; }
    }
}
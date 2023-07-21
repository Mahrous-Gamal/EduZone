using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class P_Registration
    {
        public int ID { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }
}
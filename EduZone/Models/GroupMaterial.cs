using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class GroupMaterial
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string GroupCode { get; set; }
    }
}
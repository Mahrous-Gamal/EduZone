using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Educator
    {
        [Key]
        public int ID { get; set; }
        public string AcademicDegree { get; set; }
        public string CVURL { get; set; }
        public string Available { get; set; }
        public string office { get; set; }
        // for connect tables
        public string AccountID { get; set; }
    }
}
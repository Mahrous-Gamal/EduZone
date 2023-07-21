using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int ID { get; set; }
       
        public int CollegeID { get; set; }

        public int GroupNo { get; set; }

        public double GPA { get; set; }

        public int Batch { get; set; }
        public string Department { get; set; }
        public int Section { get; set; }

        // for connect tables
        public string AccountID { get; set; }
    }
}
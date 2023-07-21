using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "Form title is required")]
        public string FormTitle { get; set; }
        public string CreatorID { get; set; }
        public bool IsStart { get; set; }
        public bool IsDelete { get; set; }
        public string GroupCode { get; set; }
    }
}
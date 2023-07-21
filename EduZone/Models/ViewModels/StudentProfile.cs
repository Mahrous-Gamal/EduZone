using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models.ViewModels
{
    public class BasicInfo
    {
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Must be Number Not charactor")]
        [MaxLength(14, ErrorMessage = "Must be 14 Number")]
        public string NationalID { get; set; }

        public string Address { get; set; }

        [Required, RegularExpression(@"[a-zA-Z\s]{3,}", ErrorMessage = "Name must be character only ")]
        public string Name { get; set; }

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60 years")]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
    }
    public class CollegeInfo
    {
        [Required]
        public int CollegeID { get; set; }

        [Required, Range(1, 5)]
        public int GroupNo { get; set; }

        [Required, Range(0, 5)]
        public double GPA { get; set; }
        [Required]
        public int Batch { get; set; }
        [Required]
        public int Section { get; set; }
        [Required]
        public String Department { get; set; }
    }

    public class ChanageEmail
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
        public string NewEmail { get; set; }
        public int code { get; set; }
    }
}
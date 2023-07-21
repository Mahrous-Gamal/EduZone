using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Models.ViewModels
{
    public class AddAdminViewModel
    {
        public string id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Must be Number Not charactor")]
        [MaxLength(14, ErrorMessage = "Must be 14 Number")]
        public string NationalID { get; set; }
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string phone { get; set; }
        public string password { get; set; }

    }
}
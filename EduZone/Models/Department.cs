using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EduZone.Models
{
    [Table("Department")]
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Department")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Name  of Department")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Description  of Department")]
        public string Description { get; set; }

        public string AdminId { get; set; }
    }
}
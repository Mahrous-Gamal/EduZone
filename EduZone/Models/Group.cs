using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Group
    {
        [Key]
        public string Code { get; set; }

        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfCreate { get; set; }

        [Required]
        public string CreatorID { get; set; }

        public string image { get; set; }
    }
}
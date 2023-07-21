using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models.ViewModels
{
    public class ShowProfileViewModel
    {
        public ApplicationUser user { get; set; }
        public Student student { get; set; }
        public Educator educator { get; set; }
    }
}
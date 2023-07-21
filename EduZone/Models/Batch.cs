using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("Batch")]
    public class Batch
    {

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public double AverageGPA { get; set; }

    }
}
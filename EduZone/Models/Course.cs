using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    [Table("Course")]
    public class Course
    {
        public int Id { get; set; }
        [Display(Name = "Course name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string CourseName { get; set; }

        [Display(Name = "Description ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Description { get; set; }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "Required")]
        [NotEqual("--select--", ErrorMessage = "Required")]
        public string Level { get; set; }

        [Display(Name = "Semester")]
        [Required(ErrorMessage = "Required")]
        [NotEqual("--select--", ErrorMessage = "Required")]
        public string Semester { get; set; }

        [Display(Name = "Doctor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string DoctorOfCourse { get; set; }

        [Display(Name = "Hours")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        [NotEqual("--select--", ErrorMessage = "Required")]
        public string NumberOfHours { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonValue;

        public NotEqualAttribute(string comparisonValue)
        {
            _comparisonValue = comparisonValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.ToString() == _comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Question text is required")]
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public int Point { get; set; }
        public int ExamId { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class StudentAnswer
    {
        [Key]
        public int ID { get; set; }
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public string StudentID { get; set; }
        public int AnswerVale { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class QuestionOption
    {
        public int ID { get; set; }
        public string OptionContent { get; set; }
        public bool IsCorrect { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
    }
}
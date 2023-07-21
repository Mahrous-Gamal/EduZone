using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models.ViewModels
{
    public class StudentAnswerDegreeViewModel
    {
        public StudentExamDegree examDegree { get; set; }
        public List<StudentAnswer> studentAnswers { get; set; }
    }
}
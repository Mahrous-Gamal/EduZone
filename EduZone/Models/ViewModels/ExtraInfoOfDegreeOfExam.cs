using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models.ViewModels
{
    public class ExtraInfoOfDegreeOfExam
    {
        
        public string StudentID { get; set; }
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public int ExamDegree { get; set; }
        public int TotalDegreeOfExam { get; set; }
        public string DoctorCreate { get; set; }

    }
}
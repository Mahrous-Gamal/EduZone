using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class StudentExamDegree
    {
        public int ID { get; set; }
        public int ExamID { get; set; }
        public string GroupCode { get; set; }
        public string StudentID { get; set; }
        public int Degree { get; set; }
        public string StudentName { get;  set; }
        public string Sitting_Number { get;  set; }
    }
}
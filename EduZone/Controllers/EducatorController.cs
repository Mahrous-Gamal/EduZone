using EduZone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class EducatorController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult dashboard()
        {
            return View();
        }
        // GET: Educator
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var Exams = context.GetExams.Where(e => e.CreatorID == id&&e.IsDelete == false).ToList();
            return View(Exams);
        }

        public ActionResult CreateExam()
        {
            string id = User.Identity.GetUserId();
            var Gro = context.GetGroups.Where(e => e.CreatorID == id).ToList();
            return View(Gro);
        }

        [HttpPost]
        public ActionResult CreateExam(string Group_Name, string Form_Title,string N_question)
        {
            // First Add Exam
            Add_Exam(Group_Name, Form_Title, N_question);

            // Retern To Back
            string id = User.Identity.GetUserId();
            var Exams = context.GetExams.Where(e => e.CreatorID == id).ToList();
            return RedirectToAction("Index", Exams);
        }
        
        public ActionResult UpdateExam(int id)
        {
            var Exams = context.GetExams.FirstOrDefault(e => e.Id == id);
            return View(Exams);
        }

        [HttpPost]
        public ActionResult UpdateExam(int id, string Group_Name, string Form_Title, string N_question)
        {
            //Delete first 
            Del_Exam(id);

            //Add New Exam
            Add_Exam(Group_Name, Form_Title, N_question);

            // Retern To Back
            string idx = User.Identity.GetUserId();
            var Exams = context.GetExams.Where(e => e.CreatorID == idx).ToList();
            return RedirectToAction("Index", Exams);
        }

        private void Add_Exam(string Group_Name, string Form_Title, string N_question)
        {
            var GN = context.GetGroups.FirstOrDefault(e => e.Code == Group_Name).GroupName;
            Exam exam = new Exam()
            {
                CreatorID = User.Identity.GetUserId(),
                GroupName = GN,
                FormTitle = Form_Title,
                GroupCode = Group_Name,
                IsDelete = false,
                IsStart = false
            };
            context.GetExams.Add(exam);
            context.SaveChanges();

            Question question = new Question();
            //QuestionOption questionOption = new QuestionOption();

            int N = int.Parse(N_question);
            for (int i = 0; i < N; i++)
            {

                if (Request.Form[$"Q{i}"] != null)
                {
                    question.QuestionText = Request.Form[$"Q{i}"].ToString();
                    question.ExamId = exam.Id;
                    if (Request.Form[$"QPI{i}"] != null)
                    {
                        question.Point = int.Parse(Request.Form[$"QPI{i}"]);
                    }
                    if (Request.Form[$"QR{i}"] != null)
                    {
                        question.CorrectAnswer = Request.Form[$"QR{i}"].ToString();
                    }
                    context.GetQuestions.Add(question);
                    context.SaveChanges();

                    for (int j = 0; j < 4; j++)
                    {
                        if (Request.Form[$"Q{i}I{j}"] != null)
                        {
                            QuestionOption questionOption = new QuestionOption();
                            questionOption.ExamId = exam.Id;
                            questionOption.QuestionId = question.Id;
                            questionOption.OptionContent = Request.Form[$"Q{i}I{j}"].ToString();
                            context.GetQuestionOptions.Add(questionOption);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
        public ActionResult DeleteExam(int id)
        {
            Del_Exam(id);

            // Retern To Index
            string idx = User.Identity.GetUserId();
            var Exams = context.GetExams.Where(e => e.CreatorID == idx).ToList();
            return RedirectToAction("Index", Exams);
        }
        
        public ActionResult StartExam(int id)
        {
            var ex = context.GetExams.FirstOrDefault(e => e.Id == id);
            if (ex.IsStart == false)
            {
                ex.IsStart = true;
            }
            else
            {
                ex.IsStart = false;
            }
            context.SaveChanges();
            // Retern To Index
            string idx = User.Identity.GetUserId();
            var Exams = context.GetExams.Where(e => e.CreatorID == idx).ToList();
            return RedirectToAction("Index", Exams);
        }
        private void Del_Exam(int id)
        {
            //Exam
            var exam = context.GetExams.FirstOrDefault(e => e.Id == id);
            exam.IsDelete = true;
            exam.IsStart = false;
            context.SaveChanges();
        }
    }
}
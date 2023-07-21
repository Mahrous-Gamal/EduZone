using EduZone.Models.ViewModels;
using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Rotativa;
using EduZone.Services;

namespace EduZone.Controllers
{
    public class StudentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: User
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult CourseEnrollment()
        {
            return View();
        }
        public ActionResult GetBatches(int BN=1)
        {
            ViewBag.BN = BN;
            var memberes = context.GetStudents.Where(e => e.Batch == BN);
            List<ApplicationUser> students = new List<ApplicationUser>();
            if (memberes != null)
            {
                foreach (var item in memberes)
                {
                    var val = context.Users.FirstOrDefault(e => e.Id == item.AccountID);
                    students.Add(val);
                }
            }
            return View(students);
        }
        public ActionResult Exam()
        {
            List<Exam> exams = new List<Exam>();
            var ex = context.GetExams.Where(e => e.IsStart == true).ToList();
            var idx = User.Identity.GetUserId();
            var take = context.GetSudentExamDegrees.Where(e => e.StudentID == idx);
            foreach (var item in ex)
            {
                bool find = true;
                foreach (var item1 in take)
                {
                    
                    if (item.Id == item1.ExamID)
                    {
                        find = false;
                    } 
                }
                if (find == true)
                {
                    exams.Add(item);
                }
            }
            return View(exams);
        }
        public ActionResult OpenExam(int id)
        {
            var Exam = context.GetExams.FirstOrDefault(e => e.Id == id);
            return View(Exam);
        }
        [HttpPost]
        public async Task<ActionResult> SaveExam(int id,string StudentName,string SNumber,string Student_ID)
        {
            var exam = context.GetExams.FirstOrDefault(e => e.Id == id);
            int Degre = 0;
            var Questions = context.GetQuestions.Where(e => e.ExamId == id).ToList();
            int N = Questions.Count();
            StudentAnswer answer = null;
            for (int i = 0; i < N; i++)
            {
                string v = Request.Form[$"QR{i}"].ToString();
                 answer = new StudentAnswer()
                {
                    Answer = v,
                    QuestionID = Questions[i].Id,
                    ExamID = Questions[i].ExamId,
                    StudentID = Student_ID,
                    AnswerVale = 0
                };

                if (Request.Form[$"QR{i}"] != null&& Questions[i].CorrectAnswer == Request.Form[$"QR{i}"].ToString())
                {
                    Degre += Questions[i].Point;
                    answer.AnswerVale = Questions[i].Point;

                }
                context.GetStudentAnswers.Add(answer);
                context.SaveChanges();
            }

            StudentExamDegree sudentExamDegree = new StudentExamDegree()
            {
                ExamID = id,
                Degree = Degre,
                GroupCode = exam.GroupCode,
                StudentID = Student_ID,
                StudentName = StudentName,
                Sitting_Number = SNumber
            };
            context.GetSudentExamDegrees.Add(sudentExamDegree);
            context.SaveChanges();

            //Send Email ?
            if (Request.Form["Send"]!=null&& Request.Form["Send"].ToString() == "1")
            {
                string Calback = Url.Action("StudentAnswer", "Student", new { ExamId = answer.ExamID, StudentID = answer.StudentID }, protocol: Request.Url.Scheme);
                SendEmailDegree send = new SendEmailDegree(Calback);
                var Sidx = answer.StudentID;
                var mail = context.Users.FirstOrDefault(e => e.Id == Sidx).Email;
                await send.SendEmailAsync(mail);
            }

            // return to First Page
            List<Exam> exams = new List<Exam>();
            var idx = User.Identity.GetUserId();
            var ex = context.GetExams.Where(e => e.IsStart == true).ToList();
            var take = context.GetSudentExamDegrees.Where(e => e.StudentID == idx);
            foreach (var item in ex)
            {
                bool find = true;
                foreach (var item1 in take)
                {

                    if (item.Id == item1.ExamID)
                    {
                        find = false;
                    }
                }
                if (find == true)
                {
                    exams.Add(item);
                }
            }

            return RedirectToAction("Exam",exams);
        }
        public ActionResult StudentAnswer(int ExamId , string StudentID)
        {
            var ExDegree = context.GetSudentExamDegrees.FirstOrDefault(e => e.ExamID == ExamId && e.StudentID == StudentID);
            var studentAnswer = context.GetStudentAnswers.Where(e => e.ExamID == ExamId && e.StudentID == StudentID).ToList();
            StudentAnswerDegreeViewModel model = new StudentAnswerDegreeViewModel()
            {
                studentAnswers = studentAnswer,
                examDegree = ExDegree
            };
            return View(model);
        }
        public async Task <ActionResult> SendTest(int ExamId, string StudentID)
        {
            string Calback = Url.Action("StudentAnswer", "Student", new { ExamId = ExamId, StudentID = StudentID }, protocol: Request.Url.Scheme);
            SendEmailDegree send = new SendEmailDegree(Calback);
            await send.SendEmailAsync("heshamabdelbast87@gmail.com");

            return RedirectToAction("Index");
        }
        public ActionResult Pre_Registration()
        {
            var currentUser = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(c => c.Id == currentUser);
            var student = context.GetStudents.FirstOrDefault(c => c.AccountID == currentUser);
            var department = context.GetDepartments.FirstOrDefault(c => c.Name.ToLower() == student.Department.ToLower());
            var ss = context.GetP_Registrations.Where(c => c.UserId == currentUser).ToList();
            // var courses = context.GetCourses.Where(c =>c.DepartmentId==department.Id).ToList();
            var time = DateTime.Now.Month;
            ViewBag.Year = DateTime.Now.Year;
            if ((time >= 9 && time <= 12) || time == 1 || time == 2)
            {
                ViewBag.Semester = "First";
            }
            else
            {
                ViewBag.Semester = "Second";

            }
            if (student.Batch == 1)
            {
                if (ViewBag.Semester == "First")
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.Level == "First level" && c.Semester == "First semester").ToList();
                }
                else
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.Level == "First level" && c.Semester == "Second semester").ToList();

                }
            }
            else if (student.Batch == 2)
            {
                if (ViewBag.Semester == "First")
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.Level == "Second level" && c.Semester == "First semester").ToList();
                }
                else
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.Level == "Second level" && c.Semester == "Second semester").ToList();

                }
            }
            else if (student.Batch == 3)
            {

                if (ViewBag.Semester == "First")
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.DepartmentId == department.Id && c.Level == "Third level" && c.Semester == "First semester").ToList();

                }
                else
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.DepartmentId == department.Id && c.Level == "Third level" && c.Semester == "Second semester").ToList();


                }
            }
            else if (student.Batch == 4)
            {
                if (ViewBag.Semester == "First")
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.DepartmentId == department.Id && c.Level == "Fourth level" && c.Semester == "First semester").ToList();

                }
                else
                {
                    ViewBag.courses = context.GetCourses.Where(c => c.DepartmentId == department.Id && c.Level == "Fourth level" && c.Semester == "Second semester").ToList();
                }
            }

            foreach (var item in ss)
            {
                var d = item.CourseId;
                var f = context.GetCourses.FirstOrDefault(c => c.Id == d);
                ViewBag.courses.Remove(f);
            }

            ViewBag.studentName = user.Name;
            ViewBag.studentId = student.ID;
            ViewBag.Department = student.Department;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.Email = user.Email;
            ViewBag.Batch = student.Batch;
            if (ss != null)
            {
                ViewBag.P_RegistrationsBefor = true;
            }

            return View(ss);
        }
        public ActionResult Notification()
        {
            var userid = User.Identity.GetUserId();
            var notifications = context.GetNotifications.Where(e => e.userId == userid).OrderByDescending(e => e.TimeOfNotify).ToList();
            List<NotificationViewModel> notificationList = new List<NotificationViewModel>();

            FormatOtherUser formatOtherUser = new FormatOtherUser();
            int NumOfNotifcationUnreaded = 0;
            foreach (var notification in notifications)
            {
                Group name = null;
                if (notification.IsReaded == false)
                {
                    NumOfNotifcationUnreaded++;
                }

                var user = context.Users.FirstOrDefault(e => e.Id == notification.SenderId);
                if (notification.GroupCode != null)
                {
                    name = context.GetGroups.FirstOrDefault(e => e.Code == notification.GroupCode);
                    NotificationViewModel notify = new NotificationViewModel()
                    {
                        NotificationId = notification.Id,
                        PostId = notification.PostId,
                        TypOfPost = notification.TypeOfPost,
                        TimeOfNotifyBeforFormat = notification.TimeOfNotify,
                        GroupName = name.GroupName,
                        NameOfCreatorPost = user.Name,
                        ImageOfCreatorPost = user.Image,
                        IsReaded = notification.IsReaded,
                        TimeOfNotifyAfterFormat = formatOtherUser.FormatTimeOfNotification(notification.TimeOfNotify),
                    };
                    notificationList.Add(notify);
                }
                else
                {
                    NotificationViewModel notify = new NotificationViewModel()
                    {
                        NotificationId = notification.Id,
                        PostId = notification.PostId,
                        TypOfPost = notification.TypeOfPost,
                        TimeOfNotifyBeforFormat = notification.TimeOfNotify,
                        GroupName = null,
                        NameOfCreatorPost = user.Name,
                        ImageOfCreatorPost = user.Image,
                        IsReaded = notification.IsReaded,
                        TimeOfNotifyAfterFormat = formatOtherUser.FormatTimeOfNotification(notification.TimeOfNotify),
                    };
                    notificationList.Add(notify);
                }

            }
            ViewBag.numofnotification = NumOfNotifcationUnreaded;
            return View(notificationList);
        }
        public ActionResult NotificationIsReaded(int id)
        {
            var user = User.Identity.GetUserId();
            var notification = context.GetNotifications.FirstOrDefault(e => e.PostId == id && e.userId == user);

            if (notification == null)
            {
                return RedirectToAction("Notification");
            }
            notification.IsReaded = true;
            context.SaveChanges();

            if (notification.TypeOfPost == "timeline")
            {
                return RedirectToAction("ShowCommentOfPost", "Timeline", new { id = id });
            }
            return RedirectToAction("ShowPostInNewPage", "Group", new { id = id });
        }
    }
}
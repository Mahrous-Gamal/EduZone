using EduZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        ApplicationDbContext context = new ApplicationDbContext();
        private List<ApplicationUser> ListOfDoctor()
        {
            var mailOfDoctor = context.MailOfDoctors.ToList();
            List<ApplicationUser> Doctors = new List<ApplicationUser>();
            var doc = new ApplicationUser();

            foreach (var doctor in mailOfDoctor)
            {
                doc = context.Users.FirstOrDefault(e => e.Email == doctor.DoctorMail);
                Doctors.Add(doc);
            }
            return Doctors;
        }
        public ActionResult Index()
        {
            // class Container
            ViewBag.Con = "No";

            ViewBag.AllDoctors = ListOfDoctor();
            ViewBag.AllDepartments = context.GetDepartments.ToList();
            var courses = context.GetCourses.ToList();
            ViewBag.LstOfCourses = courses;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Course course)
        {
            // class Container
            ViewBag.Con = "No";
            if (ModelState.IsValid)
            {
                context.GetCourses.Add(course);
                context.SaveChanges();
                var crs = context.GetCourses.ToList();
                ViewBag.LstOfCourses = crs;
                ViewBag.AllDoctors = ListOfDoctor();
                ViewBag.AllDepartments = context.GetDepartments.ToList();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllDoctors = ListOfDoctor();
                ViewBag.AllDepartments = context.GetDepartments.ToList();
                var crs = context.GetCourses.ToList();
                ViewBag.LstOfCourses = crs;
                return View(course);
            }
        }
        public ActionResult Update(int Id)
        {
            // class Container
            ViewBag.Con = "No";
            Course course = context.GetCourses.FirstOrDefault(c => c.Id == Id);
            var crs = context.GetCourses.ToList();
            ViewBag.course = course;
            ViewBag.method = "Update";
            ViewBag.AllDoctors = ListOfDoctor();
            ViewBag.AllDepartments = context.GetDepartments.ToList();
            ViewBag.LstOfCourses = crs;
            return View("Index", course);
        }
        public ActionResult SaveUpdate(int Id, Course course)
        {
            // class Container
            ViewBag.Con = "No";

            if (ModelState.IsValid)
            {
                Course c = context.GetCourses.FirstOrDefault(e => e.Id == Id);
                c.CourseName = course.CourseName;
                c.Description = course.Description;
                c.DoctorOfCourse = course.DoctorOfCourse;
                c.NumberOfHours = course.NumberOfHours;
                c.DepartmentId = course.DepartmentId;
                c.Level = course.Level;
                c.Semester = course.Semester;
                context.SaveChanges();
                ViewBag.AllDoctors = ListOfDoctor();
                ViewBag.AllDepartments = context.GetDepartments.ToList();
                var crs = context.GetCourses.ToList();
                ViewBag.LstOfCourses = crs;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index",course);
            }
        }
        public ActionResult Delete(int Id)
        {
            // class Container
            ViewBag.Con = "No";

            ViewBag.method = "Delete";
            Course c = context.GetCourses.FirstOrDefault(e => e.Id == Id);
            context.GetCourses.Remove(c);
            context.SaveChanges();
            var crs = context.GetCourses.ToList();
            ViewBag.LstOfCourses = crs;
            ViewBag.AllDoctors = ListOfDoctor();
            ViewBag.AllDepartments = context.GetDepartments.ToList();
            return RedirectToAction("Index");
        }
    }
}
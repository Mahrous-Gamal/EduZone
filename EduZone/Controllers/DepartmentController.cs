using EduZone.Models;
using EduZone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class DepartmentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Department

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
            var departments = context.GetDepartments.ToList();

            ViewBag.Doctors = ListOfDoctor();
            return View(departments);
        }

        public ActionResult NewDepartment(string name, string description, string DepartmentID)
        {

            Department department = new Department();
            department.Name = name;
            department.Description = description;
            department.AdminId = DepartmentID;

            context.GetDepartments.Add(department);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateDepartment(int depId)
        {

            var dept = context.GetDepartments.FirstOrDefault(e => e.Id == depId);
            if (dept != null)
            {
                ViewBag.Department = dept;
            }
            ViewBag.curd = "update";
            ViewBag.Doctors = ListOfDoctor();
            var departments = context.GetDepartments.ToList();
            return View("Index", departments);
        }

        [HttpPost]
        public ActionResult UpdateDepartment(int depId, string name, string description, string DepartmentID)
        {

            var dept = context.GetDepartments.FirstOrDefault(e => e.Id == depId);
            if (dept != null)
            {
                dept.Name = name;
                dept.Description = description;
                dept.AdminId = DepartmentID;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult DeleteDepartment(int id)
        {

            var dept = context.GetDepartments.FirstOrDefault(e => e.Id == id);
            if (dept != null)
            {
                context.GetDepartments.Remove(dept);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
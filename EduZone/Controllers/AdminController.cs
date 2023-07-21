using EduZone.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Models
{
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AdminController()
        {
        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult dashboard()
        {
            return View();
        }
        //this function to get list of Mail Of Doctor
        private List<ExtraInfoOfMailOfDoctorViewModel> ListOfMailOfDoctor()
        {
            var mails = context.MailOfDoctors.ToList();
            var ExterInfo = new List<ExtraInfoOfMailOfDoctorViewModel>();
            foreach (var mail in mails)
            {
                var ex = new ExtraInfoOfMailOfDoctorViewModel();
                var nameOfDoc = context.Users.FirstOrDefault(e => e.Email == mail.DoctorMail);
                if (nameOfDoc != null)
                {
                    ex.DoctorName = nameOfDoc.Name;
                }
                ex.DoctorMail = mail.DoctorMail;
                ex.ID = mail.ID;
                ExterInfo.Add(ex);
            }
            return ExterInfo;
        }

        public ActionResult AddEducatorMail()
        {
            var ExterInfo = ListOfMailOfDoctor();
            return View(ExterInfo);
        }

        [HttpPost]
        public ActionResult AddEducatorMail(string email)
        {
            if (email != "")
            {
                var Mail = context.MailOfDoctors.FirstOrDefault(e => e.DoctorMail == email);
                if (Mail != null)
                {
                    ViewBag.mailExist = "The email already exists";
                    var ExterInfo = ListOfMailOfDoctor();
                    return View(ExterInfo);
                }
                MailOfDoctors doctor = new MailOfDoctors();
                doctor.DoctorMail = email;
                context.MailOfDoctors.Add(doctor);
                context.SaveChanges();
            }

            return RedirectToAction("AddEducatorMail");
        }

        public ActionResult UpdateEducatorMail(int mailId)
        {
            if (TempData["mailExist"] != null)
            {
                ViewBag.mailExist = TempData["mailExist"];
            }
            ViewBag.curd = "update";
            ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            var ExterInfo = ListOfMailOfDoctor();
            return View("AddEducatorMail", ExterInfo);
        }

        [HttpPost]
        public ActionResult UpdateEducatorMail(int mailId, string email)
        {
            var ExterInfo = ListOfMailOfDoctor();
            var Mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            if (Mail != null)
            {
                if (email != "")
                {
                    var EmailExist = context.MailOfDoctors.FirstOrDefault(e => e.DoctorMail == email);
                    if (Mail.DoctorMail != email)
                    {
                        if (EmailExist == null)
                        {
                            Mail.DoctorMail = email;
                            context.SaveChanges();
                            return RedirectToAction("AddEducatorMail");
                        }
                        TempData["mailExist"] = "The email after Update is already exists";
                        return RedirectToAction("UpdateEducatorMail", new { mailId = mailId });
                    }
                    else
                    {
                        TempData["mailExist"] = "Email and e-mail after Update are the same";

                        return RedirectToAction("UpdateEducatorMail", new { mailId = mailId });
                    }
                }
            }
            ViewBag.curd = "update";
            ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == mailId);
            return View("AddEducatorMail", ExterInfo);
        }
        public ActionResult DeleteEducatorMail(int id)
        {
            var Mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == id);
            if (Mail != null)
            {
                context.MailOfDoctors.Remove(Mail);
                context.SaveChanges();
                return RedirectToAction("AddEducatorMail");
            }
            ViewBag.curd = "delete";
            ViewBag.mail = context.MailOfDoctors.FirstOrDefault(e => e.ID == id);
            var ExterInfo = ListOfMailOfDoctor();
            return View("AddEducatorMail", ExterInfo);
        }
        public ActionResult ManageUserStudent()
        {
            List<ApplicationUser> _student = new List<ApplicationUser>();
            var user = context.Users.ToList();
            foreach (var item in user)
            {
                string role = GetRole(item.Id);
                if (role!="-1"&&role == "Student")
                {
                    _student.Add(item);
                }
            }
            return View(_student);
        }
        public ActionResult ManageUserDoctor()
        {
            List<ApplicationUser> _Doctor = new List<ApplicationUser>();
            var user = context.Users.ToList();
            foreach (var item in user)
            {
                string role = GetRole(item.Id);
                if (role != "-1" && role == "Educator")
                {
                    _Doctor.Add(item);
                }
            }
            return View(_Doctor);
        }
        private string GetRole(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Retrieve the user by user id
            var user = userManager.FindById(id);

            if (user != null)
            {
                // Retrieve all the roles for the user
                var roles = userManager.GetRoles(id);

                if (roles != null && roles.Count > 0)
                {
                    return roles[0];
                }
            }
            return "-1";
        }
        public ActionResult DeleteUser(string id)
        {
            
            string role = GetRole(id);
            if (role == "Student")
            {
                List<ApplicationUser> _student = new List<ApplicationUser>();
                var user = context.Users.ToList();
                foreach (var item in user)
                {
                    string role1 = GetRole(item.Id);
                    if (role1 != "-1" && role1 == "Student")
                    {
                        _student.Add(item);
                    }
                }
                var userx = context.Users.FirstOrDefault(e => e.Id == id);
                context.Users.Remove(userx);
                context.SaveChanges();
                //comin
                return RedirectToAction("ManageUserStudent", _student);
            }
            else
            {
                List<ApplicationUser> _Doctor = new List<ApplicationUser>();
                var user = context.Users.ToList();
                foreach (var item in user)
                {
                    string role1 = GetRole(item.Id);
                    if (role1 != "-1" && role1 == "Educator")
                    {
                        _Doctor.Add(item);
                    }
                }
                var userx = context.Users.FirstOrDefault(e => e.Id == id);
                context.Users.Remove(userx);
                context.SaveChanges();
                //post
                var std = context.Posts.Where(e => e.UserId == id);
                context.Posts.RemoveRange(std);
                context.SaveChanges();
                //post
                var p = context.GetGroups.Where(e => e.CreatorID == id);
                context.GetGroups.RemoveRange(p);
                context.SaveChanges();

                return RedirectToAction("ManageUserDoctor", _Doctor);
            }
            
        }

        public ActionResult AddAdmin()
        {
            ViewBag.Operation = "Add";
            ViewBag.ListOfAdmin = ListOfAdmin();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAdmin(AddAdminViewModel model)
        {
            ViewBag.ListOfAdmin = ListOfAdmin();
            ApplicationUser applicationUser = new ApplicationUser
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                Address = model.Address,
                NationalID = model.NationalID,
                EmailActive = true,
                Image = "admin.png",
                Gender = model.Gender,
                Age = model.Age,
                PhoneNumber = model.phone,
            };

            var result = await UserManager.CreateAsync(applicationUser, "Admin100100");
            if (result.Succeeded)
            {
                ViewBag.Operation = "Add";
                return RedirectToAction("AddAdmin");
            }
            ViewBag.Operation = "update";
            AddErrors(result);
            return View(model);
        }
        public ActionResult UpdateAdmin(string id)
        {
            ViewBag.ListOfAdmin = ListOfAdmin();
            ViewBag.Operation = "Update";
            var item = context.Users.FirstOrDefault(e => e.Id == id);
            AddAdminViewModel model = new AddAdminViewModel()
            {
                Name = item.Name,
                NationalID = item.NationalID,
                Age = item.Age,
                phone = item.PhoneNumber,
                Address = item.Address,
                Email = item.Email,
                Gender = item.Gender,
                id = item.Id,
            };
            return RedirectToAction("AddAdmin",model);
        }
        public ActionResult SaveUpdateAdmin(string id, AddAdminViewModel model)
        {

            if (ModelState.IsValid)
            {
                ViewBag.ListOfAdmin = ListOfAdmin();
                ViewBag.Operation = "Add";
                var item = context.Users.FirstOrDefault(e => e.Id == id);
                item.Name = model.Name;
                item.Age = model.Age;
                item.Address = model.Address;
                item.Gender = model.Gender;
                item.PhoneNumber = model.phone;
                item.NationalID = model.NationalID;
                item.Email = model.Email;
                context.SaveChanges();
                return RedirectToAction("AddAdmin", model);
            }
            return View(model);
        }
        private List<AddAdminViewModel> ListOfAdmin()
        {
            List<AddAdminViewModel> _Admin = new List<AddAdminViewModel>();
            var user = context.Users.ToList();
            foreach (var item in user)
            {
                string role = GetRole(item.Id);
                if (role != "-1" && role == "Admin")
                {
                    AddAdminViewModel model = new AddAdminViewModel()
                    {
                        Name = item.Name,
                        NationalID = item.NationalID,
                        Age = item.Age,
                        phone = item.PhoneNumber,
                        Address = item.Address,
                        Email = item.Email,
                        Gender = item.Gender,
                        id = item.Id,
                    };
                    _Admin.Add(model);
                }
            }
            return _Admin;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
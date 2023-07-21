using EduZone.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EduZone.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Role
        public ActionResult NewRole()
        {
            ViewBag.curd = null;

            var roles = context.Roles.ToList();
            return View(roles);
        }

        [HttpPost]
        public async Task<ActionResult> NewRole(RoleViewModel roleModel)
        {

            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manger = new RoleManager<IdentityRole>(store);
            IdentityRole role = new IdentityRole();
            role.Name = roleModel.RoleName;
            IdentityResult result = await manger.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("NewRole");
            }
            else
            {
                return View(roleModel);
            }
        }

        public ActionResult Update(string roleid)
        {
            ViewBag.curd = "update";
            ViewBag.rolename = context.Roles.FirstOrDefault(e => e.Id == roleid);
            var roles = context.Roles.ToList();
            return View("NewRole", roles);
        }

        [HttpPost]
        public ActionResult Update(string roleid, string rolename)
        {
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manger = new RoleManager<IdentityRole>(store);
            IdentityRole roleToEdit = manger.FindById(roleid);

            if (roleToEdit == null)
            {
                return HttpNotFound();
            }
            if (roleToEdit.Name != rolename)
            {
                roleToEdit.Name = rolename;
            }

            IdentityResult result = manger.Update(roleToEdit);

            if (result.Succeeded)
            {
                return RedirectToAction("NewRole");

            }

            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return RedirectToAction("NewRole");
        }
        //public ActionResult Delete(string roleid)
        //{
        //    ViewBag.curd = "delete";
        //    ViewBag.rolename = context.Roles.FirstOrDefault(e => e.Id == roleid);
        //    var roles = context.Roles.ToList();
        //    return View("NewRole", roles);
        //}
        //[HttpPost]
        public ActionResult DeleteRole(string id)
        {
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manger = new RoleManager<IdentityRole>(store);
            IdentityRole roleToEdit = manger.FindById(id);

            if (roleToEdit == null)
            {
                return HttpNotFound();
            }
            IdentityResult result = manger.Delete(roleToEdit);

            if (result.Succeeded)
            {
                return RedirectToAction("NewRole");
            }
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return RedirectToAction("NewRole");
        }
    }
}
using EduZone.Models;
using EduZone.Models.Class;
using EduZone.Models.ViewModels;
using EduZone.MyHubs;
using EduZone.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Group
        public ActionResult Index()
        {
            ViewBag.Con = "No";
            List<Group> _groups = new List<Group>();
            string userid = User.Identity.GetUserId();
            var Groups = context.GetGroupsMembers.Where(e => e.MemberId == userid);
            if (Groups != null)
            {
                foreach (var item in Groups)
                {
                    var GName = context.GetGroups.FirstOrDefault(e => e.Code == item.GroupId);
                    _groups.Add(GName);
                }
            }
            return View(_groups);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string GroupName,string Description, HttpPostedFileBase file)
        {
            //Add Group 
            Group group = new Group();
            group.GroupName = GroupName;
            group.Description = Description;
            group.image = "GroupImage.jfif";
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(path);
                group.image = file.FileName;
            }
            group.DateOfCreate = DateTime.Now.Date;
            group.CreatorID = User.Identity.GetUserId();
            string codex = RandomGroupCode.GetCode();
            var len = context.GetGroups.Where(e => e.Code == codex);
            while (len.Count()!=0)
            {
                codex = RandomGroupCode.GetCode();
                len = context.GetGroups.Where(e => e.Code == codex);
            }
            group.Code = codex;
            context.GetGroups.Add(group);
            context.SaveChanges();

            //Add to memberGroup
            GroupsMembers GM = new GroupsMembers();
            GM.GroupId = codex;
            GM.IsCreate = true;
            GM.MemberId = User.Identity.GetUserId();
            GM.TimeGoin = DateTime.Now;
            context.GetGroupsMembers.Add(GM);
            context.SaveChanges();

            //return to page of group
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //From Index View
        public ActionResult JoinGroup(string CodeOfGroup)
        {
            string userId = User.Identity.GetUserId();
            var IsFound = context.GetGroups.FirstOrDefault(e=>e.Code == CodeOfGroup);
            if (IsFound != null)
            {

                var YouInGroup = context.GetGroupsMembers.Where(e => e.GroupId == CodeOfGroup && e.MemberId == userId).ToList();
                if (YouInGroup.Count != 0)
                {
                    //return Content("You Are allredy Joined !");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Add to memberGroup
                    GroupsMembers GM = new GroupsMembers();
                    GM.GroupId = CodeOfGroup;
                    GM.IsCreate = false;
                    GM.MemberId = userId;
                    GM.TimeGoin = DateTime.Now;
                    context.GetGroupsMembers.Add(GM);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            else
            {
                //tempdate
                //return Content("Not found");
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<ActionResult> Group_Post(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;
            var data = await context.PostInGroups.Where(e=>e.GroupId==GroupCode).OrderByDescending(x => x.Date).ToListAsync();

            return View(data);
        }
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(PostInGroup post, string GrpCode, HttpPostedFileBase File)
        {
            post.UserName = User.Identity.Name;
            post.UserId = User.Identity.GetUserId();
            post.Date = DateTime.Now;
            post.GroupId = GrpCode;
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Group_Post), new { GroupCode = GrpCode });
            }
            string PostImage = "";
            if (File != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(File.FileName));
                File.SaveAs(path);
                post.ImageUrl = File.FileName;
                PostImage = File.FileName;
            }
            context.PostInGroups.Add(post);
            context.SaveChanges();

            var idx = post.UserId;
            var Image = context.Users.FirstOrDefault(e => e.Id == idx).Image;
            var name = context.Users.FirstOrDefault(e => e.Id == idx).Name;
            IHubContext adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.NewPostAddedInGroup(post, Image,name, PostImage);
            //Beign abdallah
            var postNotify = context.PostInGroups.FirstOrDefault(e => e.ContentOfPost == post.ContentOfPost && e.GroupId == GrpCode && e.UserId == post.UserId);

            var GroupMembers = context.GetGroupsMembers.Where(c => c.GroupId == post.GroupId).ToList();
            foreach (var item in GroupMembers)
            {
                if (item.MemberId == post.UserId)
                {
                    continue;
                }
                Notifications notifications = new Notifications()
                {
                    PostId = postNotify.Id,
                    SenderId = post.UserId,
                    TimeOfNotify = DateTime.Now,
                    GroupCode = GrpCode,
                    userId = item.MemberId,
                    IsReaded = false,
                    TypeOfPost = "group",
                };
                context.GetNotifications.Add(notifications);
                context.SaveChanges();
            }


            var usersInNorificationPage = context.GetUserInNotificationPages.OrderByDescending(e => e.TimeOfLastOpen).ToList();
            List<UserInNotificationPage> ListOfUserInNotificationPagesNow = new List<UserInNotificationPage>();
            foreach (var item in usersInNorificationPage)
            {
                if (DateTime.Now - item.TimeOfLastOpen <= TimeSpan.FromMinutes(3))
                {
                    foreach (var u in GroupMembers)
                    {
                        if (u.MemberId == item.UserId)
                        {
                            UserInNotificationPage userin = new UserInNotificationPage()
                            {
                                Id = item.Id,
                                TimeOfLastOpen = item.TimeOfLastOpen,
                                ConnectionID = item.ConnectionID,
                                UserId = item.UserId
                            };
                            ListOfUserInNotificationPagesNow.Add(userin);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            var notificationInDB = context.GetNotifications.OrderByDescending(t => t.TimeOfNotify).Take(1).ToArray();
            IHubContext Notivication = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            FormatOtherUser formatOtherUser = new FormatOtherUser();
            var GroupName = context.GetGroups.FirstOrDefault(c => c.Code == GrpCode);
            foreach (var item in ListOfUserInNotificationPagesNow)
            {
                if (postNotify.Id == notificationInDB[0].PostId)
                {
                    var time = formatOtherUser.FormatTimeOfNotification(notificationInDB[0].TimeOfNotify);
                    var CreatorOfPost = context.Users.FirstOrDefault(e => e.Id == post.UserId);
                    Notivication.Clients.Client(item.ConnectionID).NewNotificationFromGrop(postNotify.Id, CreatorOfPost.Name, CreatorOfPost.Image, time, GroupName.GroupName, "Timeline");

                }
            }
            //End abdallah
            return RedirectToAction(nameof(Group_Post), new { GroupCode = GrpCode });
        }


        public ActionResult ShowPostInNewPage(int id)
        {
            var postingroup = context.PostInGroups.FirstOrDefault(e => e.Id == id);
            return View(postingroup);
        }
        public ActionResult UpdatePost(int id, string GrpCode)
        {
            ViewBag.GC = GrpCode;
            var post = context.PostInGroups.Find(id);
            TempData["PostIdGrop"] = id.ToString();
            return View(post);
        }
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePost(string content, string GrpCode)
        {
            var post = context.PostInGroups.Find(Int32.Parse(TempData["PostIdGrop"].ToString()));
            post.ContentOfPost = content;
            context.SaveChanges();
            var adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.EditPostGroup(post.Id, content);
            return RedirectToAction(nameof(Group_Post), new { GroupCode = GrpCode });
        }
        public ActionResult Group_Material(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            // GCR --> Group Creater
            // 7 --> Childhood love player
            ViewBag.GCR7 = GroupValue.CreatorID;

            var listOfMaterial = context.GetMaterials.Where(e => e.GroupCode == GroupCode).ToList();
            return View(listOfMaterial);
        }
        public ActionResult SaveMaterial(HttpPostedFileBase file,string GCode)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var find = context.GetMaterials.FirstOrDefault(e => e.Name == fileName);
                if (find != null)
                {
                    fileName += context.GetMaterials.Count().ToString();
                }
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                file.SaveAs(path);

                GroupMaterial group = new GroupMaterial()
                {
                    Name = fileName,
                    Size = GetFileSize.Get(file),
                    Type = GetTypeOfFile.Get(file),
                    GroupCode = GCode
                };
                context.GetMaterials.Add(group);
                context.SaveChanges();
            }
            return RedirectToAction("Group_Material",new { GroupCode = GCode });
        }
        public ActionResult DeleteMaterial(string GCode,int id)
        {
            var obj = context.GetMaterials.FirstOrDefault(e => e.ID == id);
            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), obj.Name);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            context.GetMaterials.Remove(obj);
            context.SaveChanges();
            return RedirectToAction("Group_Material", new { GroupCode = GCode });
        }
        public ActionResult DownloadMaterial(string GCode, int id)
        {
            var obj = context.GetMaterials.FirstOrDefault(e => e.ID == id);
            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), obj.Name);
            if (System.IO.File.Exists(path))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, "application/octet-stream", obj.Name);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Group_Member(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;
            var GroupMembers = context.GetGroupsMembers.Where(c => c.GroupId == GroupCode).ToList();
            return View(GroupMembers);
        }
        public ActionResult Delete_Member(string id)
        {
            var ss = context.GetGroupsMembers.FirstOrDefault(c => c.MemberId == id);
            context.GetGroupsMembers.Remove(ss);
            context.SaveChanges();
            string code = TempData["GroupCode"].ToString();
            return RedirectToAction("Group_Member", new { GroupCode = code });
        }
        public ActionResult Group_Chat(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            string userId = User.Identity.GetUserId();
            var membar = context.GetGroupsMembers.FirstOrDefault(e => e.MemberId == userId);
            var datOfJoin = membar.TimeGoin;

            var chatGroup = context.GetChatGroups.OrderBy(e => e.CreatedAt).Where(e => e.GroupName == GroupValue.GroupName && e.CreatedAt >= datOfJoin).ToList();
            foreach (var item in chatGroup)
            {
                if (DateTime.Now.Day - item.CreatedAt.Day == 0 && DateTime.Now.Month - item.CreatedAt.Month == 0
                                        && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.time = item.CreatedAt.ToString("h: mm tt") + " Today";
                }
                else if (DateTime.Now.Day - item.CreatedAt.Day == 1
                                        && DateTime.Now.Month - item.CreatedAt.Month == 0
                                        && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.time = item.CreatedAt.ToString("h: mm tt") + " Yestarday";
                }
                else if (DateTime.Now.Day - item.CreatedAt.Day <= 7
                                        && DateTime.Now.Month - item.CreatedAt.Month == 0
                                        && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.time = item.CreatedAt.ToString("h: mm tt ") + item.CreatedAt.DayOfWeek;
                }
                else
                {
                    item.time = item.CreatedAt.ToString("MM/dd/yyyy hh:mmtt");
                }
            }
            //chatGroup.Reverse();

            return View(chatGroup);
        }
        public ActionResult Group_About(string GroupCode)
        {
            // first Get Group
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;
            ViewBag.OwnerName = context.Users.FirstOrDefault(e => e.Id == GroupValue.CreatorID).Name;
            ViewBag.Count = context.GetGroupsMembers.Where(e => e.GroupId == GroupValue.Code).Count();
            return View();
        }
        public ActionResult LeaveGroup(string GroupCode)
        {
            var userId = User.Identity.GetUserId();
            var MyGroup = context.GetGroupsMembers.FirstOrDefault(e => e.GroupId == GroupCode && e.MemberId == userId);
            context.GetGroupsMembers.Remove(MyGroup);
            context.SaveChanges();

            List<Group> _groups = new List<Group>();
            string userid = User.Identity.GetUserId();
            var Groups = context.GetGroupsMembers.Where(e => e.MemberId == userid);
            if (Groups != null)
            {
                foreach (var item in Groups)
                {
                    var GName = context.GetGroups.FirstOrDefault(e => e.Code == item.GroupId);
                    _groups.Add(GName);
                }
            }
            return RedirectToAction("Index", _groups);
        }
        public ActionResult DeleteGroup(string GCode)
        {
            var userId = User.Identity.GetUserId();
            var Group = context.GetGroups.FirstOrDefault(e => e.Code == GCode && e.CreatorID == userId);
            if (Group != null)
            {
                var MembersINGroupe = context.GetGroupsMembers.Where(e => e.GroupId == GCode).ToList();
                context.GetGroupsMembers.RemoveRange(MembersINGroupe);
                context.SaveChanges();
                context.GetGroups.Remove(Group);
                context.SaveChanges();
                
                var PostsINGroupe = context.PostInGroups.Where(e => e.GroupId == GCode).ToList();
                context.PostInGroups.RemoveRange(PostsINGroupe);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var Group1 = context.GetGroups.FirstOrDefault(e => e.Code == GCode);

            List <Group> _groups = new List<Group>();
            string userid = User.Identity.GetUserId();
            var Groups = context.GetGroupsMembers.Where(e => e.MemberId == userid);
            if (Groups != null)
            {
                foreach (var item in Groups)
                {
                    var GName = context.GetGroups.FirstOrDefault(e => e.Code == item.GroupId);
                    _groups.Add(GName);
                }
            }
            return RedirectToAction("index", _groups);
        }
        public ActionResult ShowDegreeOfExam(string GroupCode)
        {
            var GroupValue = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode);
            ViewBag.GN = GroupValue.GroupName;
            ViewBag.GC = GroupValue.Code;
            ViewBag.GD = GroupValue.Description;
            ViewBag.GCR7 = GroupValue.CreatorID;

            List<ExtraInfoOfDegreeOfExam> extraInfoOfDegreeOfExams = new List<ExtraInfoOfDegreeOfExam>();
            var userid = User.Identity.GetUserId();
            var ListOfDegreeOfExam = context.GetSudentExamDegrees.Where(e => e.GroupCode == GroupCode && e.StudentID == userid).ToList();
            foreach (var item in ListOfDegreeOfExam)
            {
                var doctorID = context.GetExams.FirstOrDefault(e => e.Id == item.ExamID);
                var doctorName = context.Users.FirstOrDefault(e => e.Id == doctorID.CreatorID);
                var listOfQustions = context.GetQuestions.Where(e => e.ExamId == item.ExamID).ToList();
                var examName = context.GetExams.FirstOrDefault(e => e.Id == item.ExamID);
                int toteldegreeofEzam = context.GetQuestions.Where(e => e.ExamId == item.ExamID)
                                                            .Select(e=>e.Point).Sum();
                ExtraInfoOfDegreeOfExam extraInfoOfDegree = new ExtraInfoOfDegreeOfExam()
                {
                    TotalDegreeOfExam = toteldegreeofEzam,
                    ExamDegree = item.Degree,
                    ExamName = examName.FormTitle,
                    DoctorCreate = doctorName.Name,
                    ExamID = item.ExamID,
                    StudentID = item.StudentID
                };

                extraInfoOfDegreeOfExams.Add(extraInfoOfDegree);
            }
            return View(extraInfoOfDegreeOfExams);

        }
    }
}
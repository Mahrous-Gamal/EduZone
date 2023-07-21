using EduZone.Models;
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
    public class TimelineController : Controller
    {
        
        ApplicationDbContext context = new ApplicationDbContext();
        public async Task<ActionResult> TimeLine()
        {
            var data = await context.Posts.OrderByDescending(x => x.Date).ToListAsync();
            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(Post post , HttpPostedFileBase File)
        {
            post.UserName = User.Identity.Name;
            post.UserId = User.Identity.GetUserId();
            post.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(TimeLine));
            }
            string PostImage = "";
            if (File != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(File.FileName));
                File.SaveAs(path);
                post.ImageUrl = File.FileName;
                PostImage = File.FileName;
            }
            context.Posts.Add(post);
            context.SaveChanges();
            var Profileimage = context.Users.Find(post.UserId).Image;
            var name = context.Users.Find(post.UserId).Name;
            var adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.NewPostAdded(post,name,Profileimage,PostImage);

            //<abdallah>

            var postNotify = context.Posts.FirstOrDefault(e => e.ContentOfPost == post.ContentOfPost && e.UserId == post.UserId);
            var AllMembers = context.Users.ToList();
            foreach (var item in AllMembers)
            {
                if (item.Id == User.Identity.GetUserId())
                {
                    continue;
                }
                Notifications notifications = new Notifications()
                {
                    PostId = postNotify.Id,
                    SenderId = post.UserId,
                    TimeOfNotify = DateTime.Now,
                    GroupCode = null,
                    userId = item.Id,
                    IsReaded = false,
                    TypeOfPost = "timeline",
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
                    UserInNotificationPage userin = new UserInNotificationPage()
                    {
                        Id = item.Id,
                        TimeOfLastOpen = item.TimeOfLastOpen,
                        ConnectionID = item.ConnectionID,
                        UserId = item.UserId
                    };
                    ListOfUserInNotificationPagesNow.Add(userin);
                }
                else
                {
                    break;
                }
            }
            var notificationInDB = context.GetNotifications.OrderByDescending(t => t.TimeOfNotify).Take(1).ToArray();
            IHubContext Notivication = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            FormatOtherUser formatOtherUser = new FormatOtherUser();
            foreach (var item in ListOfUserInNotificationPagesNow)
            {
                if (postNotify.Id == notificationInDB[0].PostId)
                {
                    var time = formatOtherUser.FormatTimeOfNotification(notificationInDB[0].TimeOfNotify);
                    var CreatorOfPost = context.Users.FirstOrDefault(e => e.Id == post.UserId);
                    Notivication.Clients.Client(item.ConnectionID).NewNotificationFromGrop(postNotify.Id, CreatorOfPost.Name, CreatorOfPost.Image, time, null, "Timeline");

                }
            }

            //</abdallah>
            return RedirectToAction(nameof(TimeLine));
        }
        public ActionResult ShowCommentOfPost(int id)
        {
            var pst = context.Posts.FirstOrDefault(i => i.Id == id);
            return View(pst);
        }
        public ActionResult UpdatePost(int id)
        {
            var post = context.Posts.Find(id);
            TempData["PostId"] = id.ToString();
            return View(post);
        }
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePost(string content)
        {
            var post = context.Posts.Find(Int32.Parse(TempData["PostId"].ToString()));
            post.ContentOfPost = content;
            context.SaveChanges();
            var adminhubcontext = GlobalHost.ConnectionManager.GetHubContext<HubClass>();
            adminhubcontext.Clients.All.EditPost(post.Id, content);
            return RedirectToAction(nameof(TimeLine));
        }

    }
}
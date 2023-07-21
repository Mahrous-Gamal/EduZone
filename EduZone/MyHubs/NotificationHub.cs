using EduZone.Models;
using EduZone.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;


namespace EduZone.MyHubs
{
    [HubName("NotificationHub")]
    public class NotificationHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void onlineUser(string userId, string connectionId)
        {


            if (userId != "")
            {
                var user = db.GetUserInNotificationPages.FirstOrDefault(x => x.UserId == userId);
                if (user != null)
                {
                    user.TimeOfLastOpen = DateTime.Now;
                    user.ConnectionID = connectionId;
                    db.SaveChanges();
                }
                else
                {
                    UserInNotificationPage userInNotificationPage = new UserInNotificationPage()
                    {
                        UserId = userId,
                        TimeOfLastOpen = DateTime.Now,
                        ConnectionID = connectionId,
                    };
                    db.GetUserInNotificationPages.Add(userInNotificationPage);
                    db.SaveChanges();
                }
            }
        }

        public void markAllAsRead(string userId)
        {
            var notificationsBeforReaded = db.GetNotifications.Where(e => e.userId == userId).OrderByDescending(e => e.TimeOfNotify).ToList();
            foreach (var notification in notificationsBeforReaded)
            {
                var notify = db.GetNotifications.FirstOrDefault(e => e.Id == notification.Id);
                notify.IsReaded = true;
                db.SaveChanges();
            }

            var notificationsAfterReeaded = db.GetNotifications.Where(e => e.userId == userId).OrderByDescending(e => e.TimeOfNotify).ToList();
            FormatOtherUser formatOtherUser = new FormatOtherUser();
            int x = 0;
            foreach (var item in notificationsAfterReeaded)
            {
                var time = formatOtherUser.FormatTimeOfNotification(item.TimeOfNotify);
                var CreatorOfPost = db.Users.FirstOrDefault(e => e.Id == item.SenderId);
                if (item.TypeOfPost == "timeline")
                {
                    Clients.Caller.NotificationAfterMarkRead(item.PostId, CreatorOfPost.Name, CreatorOfPost.Image, time, null, x);
                }
                else
                {
                    var post = db.PostInGroups.FirstOrDefault(e => e.Id == item.PostId);
                    var GroupName = db.GetGroups.FirstOrDefault(c => c.Code == post.GroupId);
                    Clients.Caller.NotificationAfterMarkRead(item.PostId, CreatorOfPost.Name, CreatorOfPost.Image, time, GroupName.GroupName, x);
                }
                x++;
            }
        }
        public override Task OnConnected()
        {
            Clients.Caller.addUser(Context.ConnectionId);
            return base.OnConnected();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
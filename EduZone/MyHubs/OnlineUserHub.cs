using EduZone.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EduZone.MyHubs
{
    [HubName("OnlineUserHub")]
    public class OnlineUserHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public void setOnlineUser(string userId)
        {
            var user = db.GetIsOnlines.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                user.CreatedAt = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                CurrantUserIsOnline currantUserIsOnline = new CurrantUserIsOnline();
                currantUserIsOnline.UserId = userId;
                currantUserIsOnline.CreatedAt = DateTime.Now;
                db.GetIsOnlines.Add(currantUserIsOnline);
                db.SaveChanges();
            }
        }
        public override Task OnConnected()
        {
            Clients.Caller.onlineUser();
            return base.OnConnected();
        }
    }
}
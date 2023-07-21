using EduZone.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace EduZone.Hubs
{
    [HubName("ChatGroupHub")]
    public class ChatGroupHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HubMethodName("SendMessage")]
        public void SendMessage(string userid, string message, string groupCode)
        {

            ChatGroup chatGroup = new ChatGroup();

            var group = db.GetGroups.FirstOrDefault(e => e.Code == groupCode);

            var lastRow = db.GetChatGroups.OrderByDescending(row => row.Id).Where(e => e.GroupName == group.GroupName).FirstOrDefault();
            if (group != null)
            {
                chatGroup.GroupName = group.GroupName;
            }
            var user = db.Users.FirstOrDefault(e => e.Id == userid);
            if (user != null)
            {
                chatGroup.Image = user.Image;
                chatGroup.UserName = user.Name;
            }
            chatGroup.UserId = userid;
            chatGroup.IsImage = false;
            chatGroup.MessageContant = message;
            chatGroup.CreatedAt = DateTime.Now;
            db.GetChatGroups.Add(chatGroup);
            db.SaveChanges();

            Clients.All.showMessage(userid, DateTime.Now.ToString("h: mm tt"), message, user.Image, user.Name, lastRow.UserId);

        }
    }
}
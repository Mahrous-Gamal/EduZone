using EduZone.Models;
using EduZone.Models.ViewModels;
using EduZone.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EduZone.MyHubs
{
    [HubName("ChatIndividualHub")]
    public class ChatIndividualHub : Hub
    {

        ApplicationDbContext db = new ApplicationDbContext();
        private static int _userCount = 0;
        [HubMethodName("SendMessage")]
        public void SendMessage(string userid, string message, string otherUserId)
        {
            //Save new message in DB
            var mess = new ChatIndividual();
            mess.SenderId = userid;
            mess.IsImage = false;
            mess.ReceiverId = otherUserId;
            mess.MessageContant = message;
            mess.CreatedAt = DateTime.Now;
            db.GetChatIndividual.Add(mess);
            db.SaveChanges();

            // add/update time of last message  between two users
            var lastMessage = new LastMessageInChatIndividual();
            var ExistLastInDB = db.GetLastMessage.FirstOrDefault(e => e.SendId == userid && e.ReseverID == otherUserId);
            if (ExistLastInDB == null)
            {
                lastMessage.SendId = userid;
                lastMessage.ReseverID = otherUserId;
                lastMessage.LastMessage = DateTime.Now;
                db.GetLastMessage.Add(lastMessage);
                db.SaveChanges();

                lastMessage = new LastMessageInChatIndividual();
                lastMessage.SendId = otherUserId;
                lastMessage.ReseverID = userid;
                lastMessage.LastMessage = DateTime.Now;
                db.GetLastMessage.Add(lastMessage);
                db.SaveChanges();
            }

            else
            {
                ExistLastInDB.LastMessage = DateTime.Now;
                db.SaveChanges();

                ExistLastInDB = db.GetLastMessage.FirstOrDefault(e => e.SendId == otherUserId && e.ReseverID == userid);
                ExistLastInDB.LastMessage = DateTime.Now;
                db.SaveChanges();

            }

            //if other user is online--> send message to them
            string connectionId = Context.ConnectionId;
            var connectionIdForotherUser = db.GetOnlineUSers.FirstOrDefault(e => e.UserId == otherUserId && e.ReseverId == userid);
            if (connectionIdForotherUser != null && connectionId != connectionIdForotherUser.ConnectionID)
            {
                Clients.Client(connectionIdForotherUser.ConnectionID).showMessage(userid, DateTime.Now.ToString("h: mm tt"), message);
            }

            //Send the message to the user who sent the message
            Clients.Caller.showMessage(userid, DateTime.Now.ToString("h: mm tt"), message);


            //update  the list of users at the user who sent the message
            FormatOtherUser formatOtherUserFromSender = new FormatOtherUser();
            var lastMess = db.GetLastMessage.
                Where(e => e.SendId == userid).
                ToList();


            var userAfterFormat = formatOtherUserFromSender.UsersAndLastSeens(formatOtherUserFromSender.OtherUsers(userid), lastMess, userid);

            int x = 0;
            foreach (var user in userAfterFormat)
            {
                Clients.Caller.otherUsers(user.Id, user.Image, user.Name, user.TimeOfLastSeenStr, user.OnLineOrNot, x);
                x++;
            }
            //update  the list of users at the other user
            FormatOtherUser formatOtherUserFromResever = new FormatOtherUser();
            var lastMessFromResever = db.GetLastMessage.
                Where(e => e.SendId == otherUserId).
                ToList();
            var userAfterFormatFromResever = formatOtherUserFromResever.UsersAndLastSeens(formatOtherUserFromResever.OtherUsers(otherUserId), lastMessFromResever, otherUserId);
            x = 0;
            var connectionIdForotherUserToformatUsers = db.GetOnlineUSers.Where(e => e.UserId == otherUserId).OrderByDescending(e => e.TimeOfOpen).Select(e => e.ConnectionID);

            var connID = connectionIdForotherUserToformatUsers.FirstOrDefault();
            if (connID != null)
            {
                foreach (var user in userAfterFormatFromResever)
                {
                    if (connectionId != connID)
                    {
                        Clients.Client(connID).otherUsers(user.Id, user.Image, user.Name, user.TimeOfLastSeenStr, user.OnLineOrNot, x); x++;

                    }
                }
            }

        }

        public void onlineUser(string connectedid, string id, string resever)
        {
            var onlineuser = db.GetOnlineUSers.FirstOrDefault(e => e.UserId == id && e.ReseverId == resever);
            if (onlineuser != null)
            {
                onlineuser.ConnectionID = connectedid;
                onlineuser.TimeOfOpen = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                OnlineUSers NewOnlineUSer = new OnlineUSers()
                {
                    ConnectionID = connectedid,
                    ReseverId = resever,
                    TimeOfOpen = DateTime.Now,
                    UserId = id
                };

                db.GetOnlineUSers.Add(NewOnlineUSer);
                db.SaveChanges();
            }
        }
        public void deletOnlineUser(string connectedid, string id, string resever)
        {
            var item = db.GetOnlineUSers.FirstOrDefault(c => c.ConnectionID == connectedid);
            if (item != null)
            {
                //item.ConnectionID = null;
                db.GetOnlineUSers.Remove(item);
                db.SaveChanges();
            }

        }

        public void search(string word, string userid)
        {
            List<UsersAndLastSeenViewModel> users = new List<UsersAndLastSeenViewModel>();
            var lastMessage = db.GetLastMessage.Where(e => e.SendId == userid).ToList();

            FormatOtherUser formatOtherUser = new FormatOtherUser();
            var other = formatOtherUser.UsersAndLastSeens(formatOtherUser.OtherUsers(userid), lastMessage, userid);

            foreach (var item in other)
            {
                if (item.Name.ToLower().Contains(word.ToLower()))
                {
                    users.Add(item);
                }
            }

            if (users.Count == 0)
            {
                Clients.Caller.otherUsers(null, null, null, null, null, 0);
            }
            else
            {
                int x = 0;
                foreach (var user in users)
                {
                    Clients.Caller.otherUsers(user.Id, user.Image, user.Name, user.TimeOfLastSeenStr, user.OnLineOrNot, x);
                    x++;
                }
            }

        }
        public override Task OnConnected()
        {
            Clients.Caller.addUser(Context.ConnectionId);

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Clients.Caller.deleteUser(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
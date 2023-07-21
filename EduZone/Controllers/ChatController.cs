using EduZone.Models;
using EduZone.Models.ViewModels;
using EduZone.Services;
using Microsoft.AspNet.Identity;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EduZone.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Chat
        static string otherIIID = null;

        private List<ChatIndividual> FormatTime(List<ChatIndividual> Messages)
        {
            foreach (var item in Messages)
            {
                if (DateTime.Now.Day - item.CreatedAt.Day == 0
                    && DateTime.Now.Month - item.CreatedAt.Month == 0
                    && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.Time = item.CreatedAt.ToString("h: mm tt") + " Today";
                }
                else if (DateTime.Now.Day - item.CreatedAt.Day == 1
                         && DateTime.Now.Month - item.CreatedAt.Month == 0
                         && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.Time = item.CreatedAt.ToString("h: mm tt") + " Yestarday";
                }
                else if (DateTime.Now.Day - item.CreatedAt.Day <= 7
                         && DateTime.Now.Month - item.CreatedAt.Month == 0
                         && DateTime.Now.Year - item.CreatedAt.Year == 0)
                {
                    item.Time = item.CreatedAt.ToString("h: mm tt ") + item.CreatedAt.DayOfWeek;
                }
                else
                {
                    item.Time = item.CreatedAt.ToString("MM/dd/yyyy hh:mmtt");
                }
            }
            return Messages;
        }

        public ActionResult Chat_with_one(string id)
        {
            ViewBag.con = "No";
            var userid = User.Identity.GetUserId();
            if (id == null)
            {
                id = userid;
            }
            ViewBag.id = id;
            otherIIID = id;
            FormatOtherUser formatOtherUser = new FormatOtherUser();
            var usr = context.GetIsOnlines.FirstOrDefault(e => e.UserId == id);
            if (usr != null)
            {
                ViewBag.online = formatOtherUser.FormatTimeOfLastSeen(usr.CreatedAt);
            }
            ViewBag.OtherUser = context.Users.FirstOrDefault(x => x.Id == id);


            var lastMessage = context.GetLastMessage.
                Where(e => e.SendId == userid).
                ToList();

            ViewBag.users = formatOtherUser.UsersAndLastSeens(formatOtherUser.OtherUsers(userid), lastMessage, userid);

            var Messages = context.GetChatIndividual.Where(e => e.SenderId == userid && e.ReceiverId == id || e.SenderId == id && e.ReceiverId == userid).OrderBy(e => e.CreatedAt).ToList();
            Messages = FormatTime(Messages);
            return View(Messages);
        }

        #region Search in users
        public ActionResult Search(string search)
        {
            var userid = User.Identity.GetUserId();

            List<UsersAndLastSeenViewModel> users = new List<UsersAndLastSeenViewModel>();
            var lastMessage = context.GetLastMessage.Where(e => e.SendId == userid).ToList();

            FormatOtherUser formatOtherUser = new FormatOtherUser();
            var other = formatOtherUser.UsersAndLastSeens(formatOtherUser.OtherUsers(userid), lastMessage, userid);

            foreach (var item in other)
            {
                if (item.Name.ToLower().Contains(search.ToLower()))
                {
                    users.Add(item);
                }
            }

            ViewBag.users = users;

            ViewBag.OtherUser = context.Users.FirstOrDefault(x => x.Id == otherIIID);

            var Messages = context.GetChatIndividual.Where(e => e.SenderId == userid && e.ReceiverId == otherIIID || e.SenderId == otherIIID && e.ReceiverId == userid).OrderBy(e => e.CreatedAt).ToList();
            Messages = FormatTime(Messages);
            return View("Chat_with_one", Messages);
        }
        #endregion
    }
}
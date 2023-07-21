using EduZone.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace EduZone.Hubs
{
    public class postGroupHub : Hub
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        public void addPost(string imageBytes,string GroupCode,string content)
        {
            //save here
            string id = context.GetGroups.FirstOrDefault(e => e.Code == GroupCode).CreatorID;
            var user = context.Users.FirstOrDefault(e => e.Id == id);
            //SaveImageToFile(imageBytes);
            Clients.All.newPostAdd(GroupCode, content,user.Image, user.Name);
        }
        private void SaveImageToFile(string stringImage)
        {
            byte[] imageBytes = Convert.FromBase64String(stringImage);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(ms);
                image.Save("image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
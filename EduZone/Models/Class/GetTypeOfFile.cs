using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EduZone.Models.Class
{
    public static class GetTypeOfFile
    {
        public static String Get(HttpPostedFileBase file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string Type = "";
            if (fileExtension == ".pdf"|| fileExtension == ".docx"|| fileExtension == ".docm" || fileExtension == ".doc" ||
                fileExtension == ".pptx" || fileExtension == ".pptm" || fileExtension == ".ppt")
            {
                Type = "Document";
            }
            else if(fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
            {
                Type = "Image";
            }
            else if (fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov")
            {
                Type = "Video";
            }
            else if(fileExtension == ".rec" || fileExtension == ".wav" || fileExtension == ".mp3")
            {
                Type = "Record";
            }
            else
            {
                Type = "File";
            }
            return Type;
        }
    }
}
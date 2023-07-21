using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models.Class
{
    public static class GetFileSize
    {
        public static string Get(HttpPostedFileBase file)
        {
            string size = "";
            long filesize = file.ContentLength;
            double F_GB = (double)filesize / 1073741824; 
            double F_MB = (double)filesize / 1048576;
            double F_KB = (double)filesize / 1024;
            if (F_GB > 1)
            {
                size = Math.Round(F_GB, 2, MidpointRounding.AwayFromZero).ToString() + "GB";
            }
            else if (F_MB > 1)
            {
                size = Math.Round(F_MB, 2, MidpointRounding.AwayFromZero).ToString() + "MB";
            }
            else
            {
                size = Math.Round(F_KB, 2, MidpointRounding.AwayFromZero).ToString() + "KB";
            }
            return size;
        }
    }
}
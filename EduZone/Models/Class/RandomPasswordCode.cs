using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public static class RandomPasswordCode
    {
        static string code;
        static RandomPasswordCode()
        {
            code = "";
        }
     
        public static string GetCode()
        {
            Random random = new Random();
            code = random.Next(10000000, 100000000).ToString();

            return code;
        }
    }
}
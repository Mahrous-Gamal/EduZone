using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public static class RandomGroupCode
    {
        public static string GetCode()
        {
            string Code = "";
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    var ch = rand.Next(26);

                    // if 0 will be small char else capital char
                    if (rand.Next(2) == 0)
                        Code += (char)(ch + 'a');
                    else
                        Code += (char)(ch + 'A');
                }
                else
                {
                    var num = rand.Next(10);
                    Code += (char)(num + '0');
                }
                //  Console.WriteLine (Code);
            }
            return Code;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utility
{
    public static class Logger
    {
        public static void Log(string s)
        {
            StreamWriter sw = new StreamWriter("c:\\ZBHBlog.txt", true, Encoding.Default);
            sw.WriteLine(System.DateTime.Now.ToString("yyyyMMddhhmmss")+"----"+s);
            sw.Close();
        }

        public static void LogC(string s)
        {
            System.Console.WriteLine(s);
        }
    }
}

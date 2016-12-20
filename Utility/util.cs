using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Utility
{
   public class util
    {
        public static string StrEncode(object obj)
        {
            return HttpContext.Current.Server.HtmlEncode(obj.ToString());
        }
        public static string UrlCode(object obj)
        {
            return "http://localhost/New.aspx?Id=" + obj.ToString();
        }
    }
}

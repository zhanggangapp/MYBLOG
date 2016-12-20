using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace System.Web
{
   public static class RequestHelper
    {
        public static bool IsPostBack(this HttpRequestBase request)
        {
            return request.HttpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using System.Drawing;
namespace BLOG.handlers
{
    /// <summary>
    /// vcode 的摘要说明
    /// </summary>
    public class vcode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            var code = CaptchaHelper.CreateRandomCode(4);
            var img = CaptchaHelper.DrawImage(code, 20, background: Color.White);
            context.Response.ContentType = "image/gif";
            context.Response.BinaryWrite(img);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

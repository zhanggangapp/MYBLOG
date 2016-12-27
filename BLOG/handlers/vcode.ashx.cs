using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using System.Web.Mvc;
using System.Drawing;
using System.Web.SessionState;

namespace BLOG.handlers
{
    /// <summary>
    /// vcode 的摘要说明
    /// </summary>
    public class vcode : IHttpHandler, IRequiresSessionState////不实现这个接口,Session为Null.
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            var code = CaptchaHelper.CreateRandomCode(4);
            context.Session["vcode"] = code;

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

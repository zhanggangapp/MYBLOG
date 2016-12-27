using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLOG.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user"] == null)
            {
                filterContext.Result = RedirectToRoute(new { Controller = "Login", Action = "Index",Url= Request.Url.ToString() });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
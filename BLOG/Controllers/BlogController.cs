using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
namespace BLOG.Controllers
{
    public class BlogController : Controller
    {
        BlogInfoService blogService = new BlogInfoService();
        // GET: Blog
        public ActionResult Index()
        {
            int BlogId =Convert.ToInt32(Request["blogId"]);
            BlogInfo blog = new BlogInfo();
            blog = blogService.GetBlogById(BlogId);
            ViewData["blog"] = blog;
            return View();
        }
       
    }
}
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
        CommentInfoService commentService = new CommentInfoService();

        // GET: Blog
        public ActionResult Index()
        {
            long BlogId =Convert.ToInt64(Request["blogId"]);
            BlogInfo blog = new BlogInfo();
            blog = blogService.GetBlogById(BlogId);
            ViewData["blog"] = blog;

            List<CommentInfo> listComment = new List<CommentInfo>();
            listComment = commentService.GetCommentList(BlogId);
            ViewData["listComment"] = listComment;
            return View();
        }
        public ActionResult CommentAdd()
        {
            long BlogId = Convert.ToInt64(Request["BlogId"]);
            if (Request.IsPostBack())
            {
                string userName = Request["UserName"];
                string comment = Request["Comment"];
                if (commentService.AddComment(BlogId, userName, comment))
                    return Content("ok:" + userName + "说--" + comment);
                else
                    return Content("no:填写评论失败");
            }
            return View();
            
        }
    }
}
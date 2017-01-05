using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
namespace BLOG.Controllers
{
    public class CommentController : Controller
    {
        BLL.CommentInfoService commentInfoService = new CommentInfoService();
        // GET: Comment
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]):1;
            int pageSize = 15;
            int pageCount = commentInfoService.GetCommentCount(15);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<CommentInfo> list = commentInfoService.GetCommentListByPage(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }

        
    }
}
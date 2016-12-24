﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Utility;
using BLL;

namespace BLOG.Controllers
{

    public class AdminController : BaseController
    {
        BLL.BlogInfoService blogService = new BLL.BlogInfoService();
        BLL.CommentInfoService commentService = new CommentInfoService();

        // GET: Admin
        public ActionResult Index()
        {
            int Id = Convert.ToInt32(Request["Id"]);
            string Title = Request["Title"];
            string Context = Request["Content"];
            return View();
        }
        //第一种添加方法
        //[ValidateInput(false)]
        //public ActionResult AddBlog()
        //{
        //    if (Request.IsPostBack())
        //    {
        //        string title = Request["Title"];
        //        string content = Request["Content"];
        //        if (blogService.AddBlogInfo(title, content))
        //        {
        //            //BlogInfo blog = new BlogInfo() { Title = Title, Content = context, CreatedTime = DateTime.Now };
        //            //ViewData["blog"] = blog;
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return View();

        //}

        [ValidateInput(false)]
        public ActionResult AddBlog(BlogInfo bloginfo)
        {
            if (Request.IsPostBack())
            {
                if (blogService.AddBlogInfo(bloginfo.Title, bloginfo.Content))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [ValidateInput(false)]
        public ActionResult EditBlog()
        {
            int BlogId = Convert.ToInt32(Request["Id"]);
            if (Request.IsPostBack())
            {
                string title = Request["Title"];
                string content = Request["Content"];

                if (blogService.EditBlogInfo(BlogId, title, content))
                {
                    return RedirectToAction("Index", "Blog", new { blogId = BlogId });//todi id=BlogId取参数问题?
                    //return RedirectToAction("Action2", new { DateTime1 = myType.DateTime1, DateTime2 = myType.DateTime2});
                }
            }
            else
            {
                BlogInfo blog = new BlogInfo();
                blog = blogService.GetBlogById(BlogId);
                ViewData["blog"] = blog;
            }
            return View();
        }
        public ActionResult DeleteComment()
        {
            int CommentId = Convert.ToInt32(Request["CommentId"]);
            if (commentService.DeletCommentInfo(CommentId))
                return Content("ok:删除成功");
            else
                return Content("no:删除失败");
        }
        public ActionResult DeleteBlog()
        {
            int BlogId = Convert.ToInt32(Request["Id"]);

            if (blogService.DeletBlogInfo(BlogId))
            {
                return RedirectToAction("Index", "Home");
            }
            return Content("删除失败");
        }
    }
}
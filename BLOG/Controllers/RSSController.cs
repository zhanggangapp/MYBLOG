using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace BLOG.Controllers
{
    public class RSSController : Controller
    {
        // GET: RSS
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Rss()
        {
            RSSEntity rss = ArticleModel.GetList().ToDefaultRss();
            RssResult result = new RssResult();
            result.Data = rss;
            return result;
        }
        
    }
}
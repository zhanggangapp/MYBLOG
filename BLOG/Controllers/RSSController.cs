using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
namespace BLOG.Controllers
{
    public class RSSController : Controller
    {
        ArticleInfoService articleService = new ArticleInfoService();
        // GET: RSS
        public ActionResult Index()
        {
            DateTime beginTime = System.DateTime.Now.AddDays(-15);
            RSSEntity rss = articleService.GetArticleList(beginTime).ToDefaultRss();
            RssResult result = new RssResult();
            result.Data = rss;
            return result;
            //return View();
        }
        public ActionResult Rss()
        {
            //修正,此处control应该从bll层获取数据。Index是修正的方案.
            RSSEntity rss = ArticleModel.GetList().ToDefaultRss();
            RssResult result = new RssResult();
            result.Data = rss;


            return result;

        }

    }
}
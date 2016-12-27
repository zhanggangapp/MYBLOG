using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace BLOG.Controllers
{
    public class HomeController : Controller
    {
        BLL.BlogInfoService blogInfoService = new BLL.BlogInfoService();

        // GET: Home
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 10;
            int pageCount = blogInfoService.GetBlogPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<BlogInfo> list = blogInfoService.GetBlogListByPag(pageIndex, pageSize);//获取分页数据
           
            ViewData["list"] = list;
            //分布码使用
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }

        public ActionResult List()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 20;
            int pageCount = blogInfoService.GetBlogPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<BlogInfo> list = blogInfoService.GetBlogTitleListByPage(pageIndex, pageSize);//获取分页数据
            ViewData["list"] = list;

            //分布码使用
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }

        //搜索功能实现
        public ActionResult SearchList()
        {
            if (Request.IsPostBack())
            {
                string keyword = Request["keyword"] != null ? Request["keyword"] : string.Empty;
                int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
                int pageSize = 15;
                int pageCount = blogInfoService.GetBlogListCountByKeyWord(pageSize, keyword);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                List<BlogInfo> list = blogInfoService.GetBlogTitleListByKeyWord(pageIndex, pageSize, keyword);//获取分页数据
                ViewData["searchlist"] = list;

                Session["keyword"] = keyword;
                //分布码使用
                ViewData["pageIndex"] = pageIndex;
                ViewData["pageCount"] = pageCount;
            }
            else//翻页时,为get 请求.
            {
                int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
                int pageSize = 15;
                string keyword = Session["keyword"] != null ? Session["keyword"].ToString() : "博客";
                int pageCount = blogInfoService.GetBlogListCountByKeyWord(pageSize, keyword);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                List<BlogInfo> list = blogInfoService.GetBlogTitleListByKeyWord(pageIndex, pageSize, keyword);//获取分页数据
                ViewData["searchlist"] = list;

                //分布码使用
                ViewData["pageIndex"] = pageIndex;
                ViewData["pageCount"] = pageCount;
            }
            return View();
        }
    }
}
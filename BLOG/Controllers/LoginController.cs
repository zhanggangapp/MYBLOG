using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Utility;
using Model;
namespace BLOG.Controllers
{
    public class LoginController : Controller
    {
        BLL.UserInfoService userInfoService = new UserInfoService();
        // GET: Login
        public ActionResult Index()
        {
            //if (Request.HttpMethod.Equals("post",StringComparison.InvariantCultureIgnoreCase))
            //{
            //    //回发回来的数据
            //    //var id = Request["Id"].ToInt32();--扩展方法 立方+向下箭头
            //    string email = Request["Email"];
            //    string password = Request["Password"];
            //    string vcode = Request["Vcode"];
            //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(vcode))
            //    {
            //        ViewData["result"] = "邮箱或密码或验证码未输入,或不正常,请重新输入";
            //    }
            //    else
            //    {
            //        if (!userInfoService.IsExist(email, password))
            //        {
            //            ViewData["result"] = "用户或密码错误,请重新输入";
            //        }
            //    }
            //}
            if (Request.IsPostBack())//RequstHelper扩展方法
            {
                //回发回来的数据
                //var id = Request["Id"].ToInt32();--扩展方法 立方+向下箭头
                string email = Request["Email"];
                string password = Request["Password"];
                string vcode = Request["Vcode"];
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(vcode))
                {
                    ViewData["result"] = "邮箱或密码或验证码未输入,或不正常,请重新输入";
                }
                else if (!userInfoService.IsExist(email, password))
                {

                    ViewData["result"] = "用户或密码错误,请重新输入";
                }
                else
                {
                    // return RedirectToAction("Index","Admin");
                    Session["user"] = email;
                    if (Request["Url"]!=null)
                    {
                        return Redirect(Request["Url"]);
                    }
                }
               
            }
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}
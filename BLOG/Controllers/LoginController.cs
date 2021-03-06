﻿using System;
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

            //if (Request.HttpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase))
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
            ////        string msg; //使用Out参数来返回登陆消息。
            ////        userInfoService.IsExist(email, password, out msg);
            ////        ViewData["result"] = msg;
            //    }
            //}

            if (Request.IsPostBack())//RequstHelper扩展方法
            {
                //回发回来的数据
                //var id = Request["Id"].ToInt32();--扩展方法 立方+向下箭头
                string email = Request["Email"];
                string password = Request["Password"];
                string vcode = Request["Vcode"];


                //测试，直接登陆算了。来回登陆太麻烦
                if (Convert.ToInt32(HttpContext.Application["DataType"]) == 0)
                {
                    string AdminPwd = System.Configuration.ConfigurationManager.AppSettings["Admin"];
                    Session["user"] = "zhanggang@outlook.com";
                    return RedirectToAction("Index", "Home");
                }

                //使用es存储时,先不用登陆.todo
                if (Convert.ToInt32(HttpContext.Application["DataType"]) == 2)
                {
                    string AdminPwd = System.Configuration.ConfigurationManager.AppSettings["Admin"];
                    if (AdminPwd == password)
                    {
                        Session["user"] = "zhanggang@outlook.com";
                        return RedirectToAction("Index", "Home");
                    }
                    return View();
                }
                



                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(vcode))
                {
                    ViewData["result"] = "邮箱或密码或验证码未输入,或不正常,请重新输入";
                    ViewData["email"] = string.IsNullOrEmpty(email) ? "" : email;
                    ViewData["password"] = string.IsNullOrEmpty(password) ? "" : password;
                }
                else if (!userInfoService.IsExist(email, password))
                {
                    ViewData["result"] = "用户或密码错误,请重新输入";
                    ViewData["email"] = string.IsNullOrEmpty(email) ? "" : email;
                    ViewData["password"] = string.IsNullOrEmpty(password) ? "" : password;
                }
                else if (Session["vcode"].ToString()!=vcode)
                {
                    ViewData["result"] = "验证码错误,请重新输入";
                    ViewData["email"] = string.IsNullOrEmpty(email) ? "" : email;
                    ViewData["password"] = string.IsNullOrEmpty(password) ? "" : password;

                }
                else
                {

                    


                    // return RedirectToAction("Index","Admin");
                    Session["user"] = email;
                    //todo:zhanggang:这里返回地址后面再搞.应该反回登陆前访问的地址
                    //if (((System.Web.HttpRequestWrapper)Request).UrlReferrer.Query != null)
                    //    return Redirect(((System.Web.HttpRequestWrapper)Request).UrlReferrer.Query.Replace("?Url=",""));
                    //else
                        return RedirectToAction("Index", "Home");
                }
               
            }
            return View();
        }

        public ActionResult LoginOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}
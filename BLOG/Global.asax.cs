﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Text.RegularExpressions;

namespace BLOG
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public static int DATATYPE;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["DataType"] = System.Configuration.ConfigurationManager.AppSettings["DataType"];
        }
        protected void Application_BeginRequest(object sender,EventArgs e)
        {
            //~/Home/Index/1
            var exePath = Request.AppRelativeCurrentExecutionFilePath;
            //target ~/Home/Index?PageIndex=1

            Regex regex3 = new Regex(@"Blog/Index/(\d+)");
            Regex regex4 = new Regex(@"Comment/Index/(\d+)");
            Regex regex = new Regex(@"Index/(\d+)");
            Regex regex2 = new Regex(@"/List/(\d+)");
            Regex regex0 = new Regex(@"~/(\d+)");
            Regex regex1 = new Regex(@"SearchList/(\d+)");
            if (regex3.IsMatch(exePath))
            {
                var realPath = regex3.Replace(exePath, @"Blog/Index?blogId=$1");
                Context.RewritePath(realPath);
            }
            else if(regex0.IsMatch(exePath))
            {
                var realPath = regex0.Replace(exePath, @"~/Home/Index/?pageIndex=$1");
                Context.RewritePath(realPath);
            }
            else if(regex.IsMatch(exePath))
            {
                //当前是伪静态格式
                var realPath = regex.Replace(exePath, @"Index?pageIndex=$1");
                Context.RewritePath(realPath);
            }
            else if (regex2.IsMatch(exePath))
            {
                //当前是伪静态格式
                var realPath = regex2.Replace(exePath, @"/List?pageIndex=$1");
                Context.RewritePath(realPath);
            }
            else if (regex4.IsMatch(exePath))
            {
                var realPath = regex4.Replace(exePath, @"Comment/Index?pageIndex=$1");
            }
            else if(regex1.IsMatch(exePath))
            {
                var realPath = regex1.Replace(exePath, @"SearchList?pageIndex=$1");
                Context.RewritePath(realPath);
            }
             
            
        }
    }
}

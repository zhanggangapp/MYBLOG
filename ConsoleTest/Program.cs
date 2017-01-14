using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Utility;
using DAL;
using Model;
namespace ConsoleTest
{
    public class Program
    {
        static void Main(string[] args)
        {

            //Utility.ESHelper.CreateIndex("test");
            //var info = ESHelper.client.Index("");
            Person p1 = new Person();
            p1.Name = "zg";
            p1.Age = 30;
            Person p2 = new Person() { Name = "zt", Age = 22 };
            Person p3 = p1;

            //要比较两个对象是否为同一个对象时，使用object.ReferenceEquals（）方法。
            Console.WriteLine(object.ReferenceEquals(p1, p2));//false
            Console.WriteLine(object.ReferenceEquals(p1, p3));//true

            Console.ReadKey();
            BlogInfoESDal bied = new BlogInfoESDal();
            CommentInfoESDal cied = new CommentInfoESDal();

            //int a1 = bied.AddBlogInfo("我的第4个ES博客", "博客内容4");



            // long blogId = Convert.ToInt64(System.DateTime.Now.ToString("yyyyMMddhhmmssffff"));
            long blogId = 1;
            var bloginfo = new BlogInfo
            {
                BlogId = blogId,
                Title = "标题1111",
                Content = "内容1111",
                CreatedTime = System.DateTime.Now
            };
            //var response = ESHelper.client.Index(bloginfo,idx=>idx.Index("myblog"));
            var response1 = ESHelper.client.Index(bloginfo);


            //var response = ESHelper.client.Search<BlogInfo>(s => s.Query(q => q.MatchAll()).From(0).Size(3));
            var response = ESHelper.client.Search<BlogInfo>(s => s.Query(q => q.MatchAll()).From(0).Size(20));
            foreach (BlogInfo item in response.Documents)
            {
                long a = item.BlogId;
            }
            //根据id获取一个Blog
            //BlogInfo blog = bied.GetBlogById(2);
            //测试添加一个索引

            //测试添加一个评论文档
            int ao = cied.AddCommentInfo(3, "张刚77", "这是我的一条留言");

            //测试添加一个博客文档

            int a1 = bied.AddBlogInfo("我的第4个ES博客", "博客内容4");



            Console.ReadKey();
            Console.WriteLine("BLOG测试结束");
            Console.ReadKey();


            Console.WriteLine("string测试");
            ThreadTest t1 = new ThreadTest();
            ThreadTest tt1 = new ThreadTest();
            Thread td11 = new Thread(t1.Run1);
            td11.Start();
            Thread td21 = new Thread(t1.Run2);
            //Thread td21 = new Thread(t1.Run1);
            td21.Start();

            Console.WriteLine("wait 1S ");
            Thread.Sleep(1000);

            Console.WriteLine("stringbuild测试");
            ThreadTest t = new ThreadTest();
            Thread td1 = new Thread(t.Run1);
            td1.Start();
            Thread td2 = new Thread(t.Run2);
            td2.Start();
            Console.ReadKey();

            //Console.WriteLine("stringbuild测试");
            //ThreadTest t = new ThreadTest();
            ////t.Test();
            //ThreadTest tt = new ThreadTest();
            //Thread td1 = new Thread(t.Run1);
            //td1.Start();
            //Thread td2 = new Thread(tt.Run2);
            //td2.Start();
            //Console.ReadKey();

            //Console.WriteLine("string测试");
            //ThreadTest t1 = new ThreadTest();
            //ThreadTest tt1 = new ThreadTest();
            //Thread td11 = new Thread(t1.Run1);
            //td11.Start();
            //Thread td21 = new Thread(tt1.Run2);
            //td21.Start();

            Console.ReadKey();
        }
    }

    public class ThreadTest
    {
        private StringBuilder sb = new StringBuilder();
        private string sb2 = string.Empty;
        public void Test()
        {
            Thread td1 = new Thread(Run1);
            td1.Start();
            Thread td2 = new Thread(Run2);
            td2.Start();


        }
        public void Run1()//线程执行的方法需要参数,则必须是Object类型.
        {
            int i = 0;
            while (i++ < 10)
            {
                sb.Append("aaaa").Append(Thread.CurrentThread.ManagedThreadId);
                //sb += "aaaa";
                Console.WriteLine(sb.ToString());
            }
        }
        public void Run11()
        {
            int i = 0;
            while (i++ < 10)
            {
                sb2 += "aaaa" + Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(sb.ToString());
            }
        }

        public void Run2()
        {
            int i = 0;
            while (i++ < 10)
            {
                sb.Append("bbbb").Append(Thread.CurrentThread.ManagedThreadId);
                //sb += "bbbb";
                Console.WriteLine(sb.ToString());
            }
        }
        public void Run22()
        {
            int i = 0;
            while (i++ < 10)
            {
                sb2 += "bbbb" + Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(sb.ToString());
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Utility;
using DAL;
using Model;
using System.Diagnostics;
using System.Net;

namespace ConsoleTest
{
    public static class Calculator
    {
        private static int Add(int n,int m) { return n + m; }
        public static async Task<int> AddAsync(int n,int m)
        {
            int val = await Task.Run(() => Add(n,m));
            return val;
        }
    }

    public class Program
    {
        private static readonly Stopwatch Watch = new Stopwatch();
        static void Main(string[] args)
        {
            var t5 = DoExceptionAsync();
            t5.Wait();
            Console.WriteLine($"{nameof(t5.Status)} :{t5.Status}");
            Console.WriteLine($"{nameof(t5.IsCompleted)}:{t5.IsCompleted}");
            Console.WriteLine($"{nameof(t5.IsCanceled)}:{t5.IsCanceled}");

            Console.ReadKey();

            Task<int> ttt = Calculator.AddAsync(1,2);
            Console.WriteLine($"结果：{ttt.Result}");
            Console.ReadLine();
            //不用异步的程序 
            Watch.Start();
            const string url1 = "http://www.cnblogs.com/";
            const string url2 = "http://www.cnblogs.com/liqingwen/";

            ////两次调用 CountCharacters 方法（下载某网站内容，并统计字符的个数）
            //var result1 = CountCharacters(1, url1);
            //var result2 = CountCharacters(2, url2);

            //异步的程序
            Task<int> t11 = CountCharactersAsync(1, url1);
            Task<int> t21 = CountCharactersAsync(2, url2);

            //三次调用 ExtraOperation 方法（主要是通过拼接字符串达到耗时操作）
            for (int i = 0; i < 3; i++)
            {
                ExtraOperation(i + 1);
            }

            //Console.WriteLine($"{url1} 的字符个数为：{result1}");
            //Console.WriteLine($"{url2} 的字符个数为：{result2}");

            Console.WriteLine($"{url1} 的字符个数为：{t11.Result}");
            Console.WriteLine($"{url2} 的字符个数为：{t21.Result}");

     //async / await 结构

     //先解析一下专业名词：
     //同步方法：一个程序调用某个方法，等到其执行完成之后才进行下一步操作。这也是默认的形式。
     //异步方法：一个程序调用某个方法，在处理完成之前就返回该方法。通过 async/ await 我们就可以实现这种类型的方法。

     //async / await 结构可分成三部分：
     //（1）调用方法：该方法调用异步方法，然后在异步方法执行其任务的时候继续执行；
     //（2）异步方法：该方法异步执行工作，然后立刻返回到调用方法；
     //（3）await 表达式：用于异步方法内部，指出需要异步执行的任务。一个异步方法可以包含多个 await 表达式（不存在 await 表达式的话 IDE 会发出警告）。

            Console.ReadKey();

            #region 1

            MyClass my = new MyClass();
            Console.ReadLine();

            //理解Async与Await
            Console.WriteLine("主线程测试开始..");
            AsyncMethod();
            Thread.Sleep(1000);
            Console.WriteLine("主线程测试结束..");

            Console.ReadLine();
            Console.ReadKey();
            //----旧方法
            Console.WriteLine("主线程测试开始..");
            Thread th = new Thread(ThMethod);
            th.Start();
            Thread.Sleep(1000);
            Console.WriteLine("主线程执行结束..");
            Console.ReadLine();
            Console.ReadKey();

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
#endregion
        }

        private static async Task DoExceptionAsync()
        {
            try
            {
                await Task.Run(() => { throw new Exception(); });
            }
            catch (Exception)
            {
                Console.WriteLine($"{nameof(DoExceptionAsync)}出现异常");
            }
        }

        //额外操作
        private static void ExtraOperation(int id)
        {
            //字符串拼接来表达相对耗时的操作
            var s = "";
            for (var i = 0; i < 600; i++)
            {
                s += i;
            }
            Console.WriteLine($"id={id}的Extraoperations方法完成:{Watch.ElapsedMilliseconds} ms");
        }
        private static int CountCharacters(int id,string addrsss)
        {
            var wc = new WebClient();
            Console.WriteLine($"开始调用 id={id}:{Watch.ElapsedMilliseconds} ms");
            var result = wc.DownloadString(addrsss);
            Console.WriteLine($"调用完成 id={id}:{Watch.ElapsedMilliseconds} ms");
            return result.Length;
        }
        private static async Task<int> CountCharactersAsync(int id,string address)
        {
            var wc = new WebClient();
            Console.WriteLine($"开始调用 id={id}:{Watch.ElapsedMilliseconds} ms");
            var result = await wc.DownloadStringTaskAsync(address);
            Console.WriteLine($"调用完成 id={id}:{Watch.ElapsedMilliseconds} ms");
            return result.Length;
        }
        static void ThMethod()
        {
            Console.WriteLine("异步执行开始");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("异步执行" + i.ToString() + "..");
                Thread.Sleep(1000);
            }
            Console.WriteLine("异步执行完成");
        }

        //使用Async和Await进步异步编程

        static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");
            var result = await MyMethod();
            Console.WriteLine("异步代码执行完毕");
        }
        static async Task<int> MyMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("异步执行" + i.ToString() + "..");
                await Task.Delay(1000);//模拟耗时操作
            }
            int j = 0;
           
            return j;
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

    //async 和 await方法使用示例
    public class MyClass
    {
        public MyClass()
        {
            Console.WriteLine("MyClass() Begin.");
            DisplayValue(); //这里不会阻塞  
            Console.WriteLine("MyClass() End.");
        }
        public Task<double> GetValueAsync(double num1, double num2)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }
        public async void DisplayValue()
        {
            double result = await GetValueAsync(1234.5, 1.01);//此处会开新线程处理GetValueAsync任务，然后方法马上返回  
                                                              //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
            Console.WriteLine("Value is : " + result);
        }
    }
}

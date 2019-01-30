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
using System.Collections;
using System.IO;
using System.Reflection;

namespace ConsoleTest
{
    public static class Calculator
    {
        private static int Add(int n, int m) { return n + m; }
        public static async Task<int> AddAsync(int n, int m)
        {
            int val = await Task.Run(() => Add(n, m));
            return val;
        }
    }

    //async 和 await方法使用示例
    public class MyClass
    {
        public MyClass()
        {
            Console.WriteLine("MyClass() Begin.");

            Task<string> msg = DisplayValue(); //这里不会阻塞  
            string ss = msg.Result;
            Console.WriteLine("msg:" + ss);
            Console.WriteLine("MyClass() End."+ss);
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
        public Task<string> GetStringAsync()
        {
            return Task.Run(() =>
            {
                for (Int32 iii = 0; iii < 100009; iii++)
                {

                }
                return "zhanggnagb";
            });
        }

        //public async void DisplayValue()
        //{
        //    double result = await GetValueAsync(1234.5, 1.01);//此处会开新线程处理GetValueAsync任务，然后方法马上返回  
        //                                                      //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
        //    Console.WriteLine("Value is : " + result);
        //}
        public async Task<string> DisplayValue()
        {
            string result = await GetStringAsync();//此处会开新线程处理GetValueAsync任务，然后方法马上返回  
                                                  //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
            Console.WriteLine("Value is : " + result);
            return result;
        }
    }





    public class Program
    {
        private static readonly Stopwatch Watch = new Stopwatch();
        static void Main(string[] args)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uril = new UriBuilder(codeBase);
            string dllPath = Uri.UnescapeDataString(uril.Path);
            string result = Path.GetDirectoryName(dllPath);











            return;
            MyClass my = new MyClass();

            Console.ReadLine();


            return;


            var t5 = DoExceptionAsync();
            t5.Wait();
            Console.WriteLine($"{nameof(t5.Status)} :{t5.Status}");
            Console.WriteLine($"{nameof(t5.IsCompleted)}:{t5.IsCompleted}");
            Console.WriteLine($"{nameof(t5.IsCanceled)}:{t5.IsCanceled}");

            Console.ReadKey();

            Task<int> ttt = Calculator.AddAsync(1, 2);
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

            MyClass my2 = new MyClass();
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

        void TestHashtable()
        {
            Hashtable hash = new Hashtable();
            hash.Add("hl", "黄林");
            hash.Add("zzz", new object());

            hash.Add("hl", "huanlei");//报错 键值对集合的”键“一定不能重复。
            //判断一个集合中是否存在某个键
            if (!hash.ContainsKey("hl"))
            {

            }

            //通过键获取值 
            Console.WriteLine(hash["hl"].ToString());

            //遍历Hashtable
            //1。遍历键
            foreach (object item in hash.Keys)
            {
                Console.WriteLine($"键：{item}---值 ：{hash[item]}");
            }
            //2.遍历值 
            foreach (object item in hash.Values)
            {
                Console.WriteLine($"值 ：{item}");
            }
            //3.遍历键值 对
            foreach (DictionaryEntry kv in hash)
            {
                Console.WriteLine($"键：{kv.Key}---值：{kv.Value}");
            }


        }
        ////List<T>\Dictionary<K,V>  （arraylistt 和 hashtable以后就不要用了）

        void TestListDictionary()
        {
            List<string> list = new List<string>();
            Console.WriteLine(list.ToString());

            //Hashtable的泛型版本 Generic
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("hl", 35);
            Console.WriteLine($"{dict["hl"]}");
            foreach (string item in dict.Keys)
            {
                Console.WriteLine(item);
            }
            foreach (int item in dict.Values)
            {
                Console.WriteLine(item);
            }
            foreach (KeyValuePair<string, int> item in dict)
            {
                Console.WriteLine($"key:{item.Key}----value:{item.Value}");

            }
        }

        //var
        void TestVr()
        {
            //var 【类型推断】
            //1.使用var声明变量与直接使用对应的类型声明是完全是一样的。编译器在编译时，var被替换为对应的数据类型。
            //2.c#中的var与js中的var完全不一样。c#中var仍是强类型，js中var是弱类型。
            //3.var只能用作局部变量（方法中声明的变量）。不能用作类的成员变量，不用用途方法的返回值 ，也不能用作方法的参数。
            //var s;
            var s = 123;
            int s1 = 123;
            //s = "123";
        }

        //装箱与拆箱
        void TestBox()
        {
            int n = 100;
            //这个不是装箱和折箱，是类型转称。（创建了新的变量）
            string s = Convert.ToString(n);
            int m = int.Parse(s);

            int n2 = 100;
            object o = n2;//发生了一次装箱
            int m2 = (int)o;//发生了一次折箱。

            double d = 100.9;
            object o2 = d;
            //int n3 = (int)o2;//报错：指定的转换无效。拆箱时必须用装箱的类型。
            double d2 = (double)o2;
            Console.WriteLine(n2);

        }

        //TestGeneric
        void TestGeneric()
        {
            MyClass<int> mint = new MyClass<int>();
            mint[0] = 5;
            MyClass<string> mstring = new MyClass<string>();
            mstring[0] = "5";

        }
        public class MyClass<T>
        {
            private T[] _data = new T[5];
            public T this[int index]
            {
                get { return _data[index]; }
                set { _data[index] = value; }
            }
        }
        //约束，T2必须是值类型 K为引用类型 V：实现某接口的类型  W：是K的子类。 x：引用类型，且带有无参构造函数
        public class MyClass2<T2, K, V, W, X> where T2 : struct where K : class where V : IComparable where W : K where X : class, new()
        {
            private T2[] _data = new T2[5];
            public T2 this[int index]
            {
                get { return _data[index]; }
                set { _data[index] = value; }
            }
        }

        //foreach(*) 
        void TestForeach()
        {
            int[] arr = { 1, 2, 45 };
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            //凡是具有GetEnumerator()方法的类型可以使用foreach来遍历。foreach遍历是调用了一个”枚举器“来遍历数据，和foreach关没系，foreach只是一个语法上的简化而已。

            Person p = new Person();
            IEnumerator etor = p.GetEnumerator();
            while (etor.MoveNext())
            {
                Console.WriteLine(etor.Current.ToString());
            }
            etor.Reset();
            while (etor.MoveNext())
            {
                Console.WriteLine(etor.Current.ToString());
            }
            //foreach是上面的简写形式.il代码中,没有foreach这个函数.
            foreach (string item in p)
            {
                Console.WriteLine(item);
            }
        }
        //1.实现IEnumeralbe的接口，通过这个接口，实现GetEnumerator方法
        public class Person : IEnumerable
        {
            private string[] Friends = new string[] { "a", "b", "c", "d" };


            public string Name { get; set; }
            public int Age { get; set; }

            public IEnumerator GetEnumerator()
            {
                //2.这个方法要返回一个枚举器
                return new PersonEnumerator(this.Friends);
            }
        }
        //这个类型就是一个“枚举器”。希望一个类被“枚举”、“遍历”，要给这个类person实现一个枚举器类：PersonEnumerator。
        public class PersonEnumerator : IEnumerator
        {
            private string[] _friends;
            //下标一般是指向了第一条的前一条.所以是-1
            private int index = -1;
            public PersonEnumerator(string[] fs)
            {
                _friends = fs;
            }
            public object Current
            {
                get
                {
                    if (index >= 0 && index < _friends.Length)
                    {
                        return _friends[index];
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
            public bool MoveNext()
            {
                if (index + 1 < _friends.Length)
                {
                    index++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                index = -1;
            }
        }
        void TestForeach2()
        {
            string[] ss = { "a", "b", "c" };
            IEnumerable ie = ss;
            foreach (var item in ie)
            {
                Console.WriteLine(item);
            }
        }
        void TestFile()
        {
            string path = @"D:\zg\SOS.dll";
            //1.获取文件名
            Console.WriteLine(Path.GetFileName(path));
            //2.获取文件的后缀
            Console.WriteLine(Path.GetExtension(path));
            //3.获取不带后缀的文件名
            Console.WriteLine(Path.GetFileNameWithoutExtension(path));
            //4.获取路径的目录部分
            Console.WriteLine(Path.GetDirectoryName(path));
            //5.将路径中的文件名的后续缀改为.exe
            Console.WriteLine(Path.ChangeExtension(path, ".exe"));

            string s1 = @"c:\zg\zg";
            string s2 = "zt.txt";
            Console.WriteLine(Path.Combine(s1, s2));
        }
        //delegate：委托是一种数据类型，像类一样。（可以声明委托类型变量），方法参数可以是int,string、类类型。
        void TestDelegate()
        {
            //2.声明委托变量md，并且new了一个委托对象并且把方法m1传递进去。那md委托保存了M1方法。
            MyDelete md = new MyDelete(M1);

            //3。调用md委托的时候，相当于调用了m1方法。
            md();
            Console.WriteLine("OK");

        }
        static void M1()
        {
            Console.WriteLine("我是一个无返回值，无参数的方法");
        }
        //1.定义一个委托，用来保存无参数无返回值 的方法。
        public delegate void MyDelete();
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
        private static int CountCharacters(int id, string addrsss)
        {
            var wc = new WebClient();
            Console.WriteLine($"开始调用 id={id}:{Watch.ElapsedMilliseconds} ms");
            var result = wc.DownloadString(addrsss);
            Console.WriteLine($"调用完成 id={id}:{Watch.ElapsedMilliseconds} ms");
            return result.Length;
        }
        private static async Task<int> CountCharactersAsync(int id, string address)
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


}

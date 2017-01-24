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
            Console.WriteLine($"�����{ttt.Result}");
            Console.ReadLine();
            //�����첽�ĳ��� 
            Watch.Start();
            const string url1 = "http://www.cnblogs.com/";
            const string url2 = "http://www.cnblogs.com/liqingwen/";

            ////���ε��� CountCharacters ����������ĳ��վ���ݣ���ͳ���ַ��ĸ�����
            //var result1 = CountCharacters(1, url1);
            //var result2 = CountCharacters(2, url2);

            //�첽�ĳ���
            Task<int> t11 = CountCharactersAsync(1, url1);
            Task<int> t21 = CountCharactersAsync(2, url2);

            //���ε��� ExtraOperation ��������Ҫ��ͨ��ƴ���ַ����ﵽ��ʱ������
            for (int i = 0; i < 3; i++)
            {
                ExtraOperation(i + 1);
            }

            //Console.WriteLine($"{url1} ���ַ�����Ϊ��{result1}");
            //Console.WriteLine($"{url2} ���ַ�����Ϊ��{result2}");

            Console.WriteLine($"{url1} ���ַ�����Ϊ��{t11.Result}");
            Console.WriteLine($"{url2} ���ַ�����Ϊ��{t21.Result}");

     //async / await �ṹ

     //�Ƚ���һ��רҵ���ʣ�
     //ͬ��������һ���������ĳ���������ȵ���ִ�����֮��Ž�����һ����������Ҳ��Ĭ�ϵ���ʽ��
     //�첽������һ���������ĳ���������ڴ������֮ǰ�ͷ��ظ÷�����ͨ�� async/ await ���ǾͿ���ʵ���������͵ķ�����

     //async / await �ṹ�ɷֳ������֣�
     //��1�����÷������÷��������첽������Ȼ�����첽����ִ���������ʱ�����ִ�У�
     //��2���첽�������÷����첽ִ�й�����Ȼ�����̷��ص����÷�����
     //��3��await ���ʽ�������첽�����ڲ���ָ����Ҫ�첽ִ�е�����һ���첽�������԰������ await ���ʽ�������� await ���ʽ�Ļ� IDE �ᷢ�����棩��

            Console.ReadKey();

            #region 1

            MyClass my = new MyClass();
            Console.ReadLine();

            //���Async��Await
            Console.WriteLine("���̲߳��Կ�ʼ..");
            AsyncMethod();
            Thread.Sleep(1000);
            Console.WriteLine("���̲߳��Խ���..");

            Console.ReadLine();
            Console.ReadKey();
            //----�ɷ���
            Console.WriteLine("���̲߳��Կ�ʼ..");
            Thread th = new Thread(ThMethod);
            th.Start();
            Thread.Sleep(1000);
            Console.WriteLine("���߳�ִ�н���..");
            Console.ReadLine();
            Console.ReadKey();

            //Utility.ESHelper.CreateIndex("test");
            //var info = ESHelper.client.Index("");
            Person p1 = new Person();
            p1.Name = "zg";
            p1.Age = 30;
            Person p2 = new Person() { Name = "zt", Age = 22 };
            Person p3 = p1;

            //Ҫ�Ƚ����������Ƿ�Ϊͬһ������ʱ��ʹ��object.ReferenceEquals����������
            Console.WriteLine(object.ReferenceEquals(p1, p2));//false
            Console.WriteLine(object.ReferenceEquals(p1, p3));//true

            Console.ReadKey();
            BlogInfoESDal bied = new BlogInfoESDal();
            CommentInfoESDal cied = new CommentInfoESDal();

            //int a1 = bied.AddBlogInfo("�ҵĵ�4��ES����", "��������4");



            // long blogId = Convert.ToInt64(System.DateTime.Now.ToString("yyyyMMddhhmmssffff"));
            long blogId = 1;
            var bloginfo = new BlogInfo
            {
                BlogId = blogId,
                Title = "����1111",
                Content = "����1111",
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
            //����id��ȡһ��Blog
            //BlogInfo blog = bied.GetBlogById(2);
            //�������һ������

            //�������һ�������ĵ�
            int ao = cied.AddCommentInfo(3, "�Ÿ�77", "�����ҵ�һ������");

            //�������һ�������ĵ�

            int a1 = bied.AddBlogInfo("�ҵĵ�4��ES����", "��������4");



            Console.ReadKey();
            Console.WriteLine("BLOG���Խ���");
            Console.ReadKey();


            Console.WriteLine("string����");
            ThreadTest t1 = new ThreadTest();
            ThreadTest tt1 = new ThreadTest();
            Thread td11 = new Thread(t1.Run1);
            td11.Start();
            Thread td21 = new Thread(t1.Run2);
            //Thread td21 = new Thread(t1.Run1);
            td21.Start();

            Console.WriteLine("wait 1S ");
            Thread.Sleep(1000);

            Console.WriteLine("stringbuild����");
            ThreadTest t = new ThreadTest();
            Thread td1 = new Thread(t.Run1);
            td1.Start();
            Thread td2 = new Thread(t.Run2);
            td2.Start();
            Console.ReadKey();

            //Console.WriteLine("stringbuild����");
            //ThreadTest t = new ThreadTest();
            ////t.Test();
            //ThreadTest tt = new ThreadTest();
            //Thread td1 = new Thread(t.Run1);
            //td1.Start();
            //Thread td2 = new Thread(tt.Run2);
            //td2.Start();
            //Console.ReadKey();

            //Console.WriteLine("string����");
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
                Console.WriteLine($"{nameof(DoExceptionAsync)}�����쳣");
            }
        }

        //�������
        private static void ExtraOperation(int id)
        {
            //�ַ���ƴ���������Ժ�ʱ�Ĳ���
            var s = "";
            for (var i = 0; i < 600; i++)
            {
                s += i;
            }
            Console.WriteLine($"id={id}��Extraoperations�������:{Watch.ElapsedMilliseconds} ms");
        }
        private static int CountCharacters(int id,string addrsss)
        {
            var wc = new WebClient();
            Console.WriteLine($"��ʼ���� id={id}:{Watch.ElapsedMilliseconds} ms");
            var result = wc.DownloadString(addrsss);
            Console.WriteLine($"������� id={id}:{Watch.ElapsedMilliseconds} ms");
            return result.Length;
        }
        private static async Task<int> CountCharactersAsync(int id,string address)
        {
            var wc = new WebClient();
            Console.WriteLine($"��ʼ���� id={id}:{Watch.ElapsedMilliseconds} ms");
            var result = await wc.DownloadStringTaskAsync(address);
            Console.WriteLine($"������� id={id}:{Watch.ElapsedMilliseconds} ms");
            return result.Length;
        }
        static void ThMethod()
        {
            Console.WriteLine("�첽ִ�п�ʼ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("�첽ִ��" + i.ToString() + "..");
                Thread.Sleep(1000);
            }
            Console.WriteLine("�첽ִ�����");
        }

        //ʹ��Async��Await�����첽���

        static async void AsyncMethod()
        {
            Console.WriteLine("��ʼ�첽����");
            var result = await MyMethod();
            Console.WriteLine("�첽����ִ�����");
        }
        static async Task<int> MyMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("�첽ִ��" + i.ToString() + "..");
                await Task.Delay(1000);//ģ���ʱ����
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
        public void Run1()//�߳�ִ�еķ�����Ҫ����,�������Object����.
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

    //async �� await����ʹ��ʾ��
    public class MyClass
    {
        public MyClass()
        {
            Console.WriteLine("MyClass() Begin.");
            DisplayValue(); //���ﲻ������  
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
            double result = await GetValueAsync(1234.5, 1.01);//�˴��Ὺ���̴߳���GetValueAsync����Ȼ�󷽷����Ϸ���  
                                                              //��֮������д��붼�ᱻ��װ��ί�У���GetValueAsync�������ʱ����  
            Console.WriteLine("Value is : " + result);
        }
    }
}

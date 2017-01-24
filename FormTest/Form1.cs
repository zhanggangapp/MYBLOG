using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //同步实现方式。（等待，不友好)
        //private void btnClick_Click(object sender, EventArgs e)
        //{
        //    this.btnClick.Enabled = false;
        //    long length = AccessWeb();
        //    this.btnClick.Enabled = true;
        //    //这里可以做一些不依赖回复的操作。
        //    OtherWork();

        //    this.richTextBox1.Text += string.Format("\n 回复的字节长度为：{0}.\r\n", length);
        //    txtMainTID.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        //}

        //private long AccessWeb()
        //{
        //    MemoryStream content = new MemoryStream();
        //    //对百度发起一个请求
        //    HttpWebRequest webRequest = WebRequest.Create("http://www.baidu.com") as HttpWebRequest;
        //    if (webRequest!=null)
        //    {
        //        using (WebResponse response = webRequest.GetResponse())
        //        {
        //            using (Stream responseStream= response.GetResponseStream())
        //            {
        //                responseStream.CopyTo(content);
        //                Thread.Sleep(2000);
        //            }
        //        }
        //    }
        //    txtAsynTid.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        //    return content.Length;
        //}
        private void OtherWork()
        {
            this.richTextBox1.Text += "\r\n等待服务器回复k...\n";
        }

        //#region 使用APM实现异步编程 .net1.0
        ////同步方法
        //private string TestMethod()
        //{
        //    //耗时操作，实际可能是读取一个大文件
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(200);
        //    }
        //    return "点击我按钮事件完成";
        //}
        ////回调方法
        //private void GetResult(IAsyncResult result)
        //{
        //    AsyncMethodCaller caller = (AsyncMethodCaller)((AsyncResult)result).AsyncDelegate;
        //    // 调用EndInvoke去等待异步调用完成并且获得返回值
        //    // 如果异步调用尚未完成，则 EndInvoke 会一直阻止调用线程，直到异步调用完成
        //    string resultvalue = caller.EndInvoke(result);
        //    //sc.Post(ShowState,resultvalue);
        //    richTextBox1.Invoke(showStateCallback, resultvalue);
        //}
        //// 显示结果到richTextBox
        //private void ShowState(object result)
        //{
        //    richTextBox1.Text = result.ToString();
        //    btnClick.Enabled = true;
        //}
        //#endregion

        #region 基于事件的异步模式.net2.0
        #endregion
        #region 基于任务的异步模式.net4.0
        #endregion
        #region 提供async 和 await关键字的的异步编程.net4.5
        private async void btnClick_Click(object sender, EventArgs e)
        {
            long length = await AccessWebAsync();
            //这里可以做一些不依赖回复的操作。
            OtherWork();

            this.richTextBox1.Text += string.Format("\n 回复的字节长度为：{0}.\r\n", length);
            txtMainTID.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }
        // 使用C# 5.0中提供的async 和await关键字来定义异步方法
        // 从代码中可以看出C#5.0 中定义异步方法就像定义同步方法一样简单。
        // 使用async 和await定义异步方法不会创建新线程,
        // 它运行在现有线程上执行多个任务.
        // 此时不知道大家有没有一个疑问的？在现有线程上(即UI线程上)运行一个耗时的操作时，
        // 为什么不会堵塞UI线程的呢？
        // 这个问题的答案就是 当编译器看到await关键字时，线程会
        private async Task<long> AccessWebAsync()
        {
            MemoryStream content = new MemoryStream();
            for (int i = 0; i < 10; i++)
            {
              
                HttpWebRequest webRequest = WebRequest.Create("http://www.baidu.com") as HttpWebRequest;
                if (webRequest != null)
                {
                    using (WebResponse response = await webRequest.GetResponseAsync())
                    {
                        Thread.Sleep(200);
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            await responseStream.CopyToAsync(content);

                        }
                    }
                }
            }
           
            txtAsynTid.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            return content.Length;
        }
        #endregion

    }
}

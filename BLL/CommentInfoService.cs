using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Web;
using System.Threading;
using System.Diagnostics;
using Microsoft.Samples.Debugging.MdbgEngine;
using System.IO;

namespace BLL
{
   public class CommentInfoService
    {
        DAL.ICommentInfoDal commentInfoDal = new CommentInfoSQLDal();
        public string result = string.Empty;
        public CommentInfoService()
        {
            int DataType = Convert.ToInt32(HttpContext.Current.Application["DataType"]);
            switch (DataType)
            {
                case 0:
                    commentInfoDal = new CommentInfoSQLDal();
                    break;
                case 2:
                    commentInfoDal = new CommentInfoESDal();
                    break;
                default:
                    commentInfoDal = new CommentInfoSQLDal();
                    break;
            }

        }
        public bool AddComment(long blogId,string userName,string comment)
        {
            if (blogId==0 || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(comment))
            {
                return false;
            }
            return commentInfoDal.AddCommentInfo(blogId, userName, comment) > 0;
        }

        //public async Task<string> DbgAdd()
        //{
        //    string result = await ExectueCmdForCDB();
            
        //    return result;

        //}

        
        public string DbgAdd()
        {
            
            Thread t = new Thread(ExectueCmdForCDB);
            t.Start();
            t.Join();
            //string result = ExectueCmdForCDB();
            return result;
        }


        public string MDbgAdd()
        {
            
            Thread t = new Thread(ExectueCmdForMdbgConsole);
            t.Start(Process.GetCurrentProcess().Id);

            

            t.Join();
            //string result = ExectueCmdForCDB();
            return result;
        }



        private string ExectueCmdForDbg()
        {

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            p.StandardInput.AutoFlush = true;
            //向cmd窗口发送输入信息
            //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe&&.loadby sos clr&&g&&~*e!clrstack&&qd&exit");
            //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv&&.loadby sos clr&&!runaway&&qd&exit");
            //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv&&!runaway&&qd&exit");
            //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv");
            //p.StandardInput.WriteLine(@"qd");

            p.StandardInput.WriteLine(@"ping www.baidu.com");
            p.StandardInput.WriteLine(@"exit");

            //p.StandardInput.WriteLine(@"ddd&exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();


            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return output.Replace(">", "::");

        }

        //private Task<string> ExectueCmdForCDB()
        //{
        //    return Task<string>.Run(() =>
        //    {
        //        System.Diagnostics.Process p = new System.Diagnostics.Process();
        //        p.StartInfo.FileName = "cmd.exe";
        //        //p.StartInfo.Arguments = "";
        //        p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
        //        p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
        //        p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
        //        p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
        //        p.StartInfo.CreateNoWindow = true;//不显示程序窗口
        //        p.Start();//启动程序
        //        p.StandardInput.AutoFlush = true;
        //        //向cmd窗口发送输入信息
        //        //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe&&.loadby sos clr&&g&&~*e!clrstack&&qd&exit");
        //        //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv&&.loadby sos clr&&!runaway&&qd&exit");
        //        //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv&&!runaway&&qd&exit");
        //        //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv");
        //        //p.StandardInput.WriteLine(@"qd");

        //        //p.StandardInput.WriteLine(@".loadby sos clr");

        //        //p.StandardInput.WriteLine("D:\\WinDBGx64New\\cdb.exe -pn w3wp.exe -pv -c \".loadby sos clr;!runaway;qd\"&exit");

        //        //p.StandardInput.WriteLine("D:\\WinDBGx64New\\cdb.exe -pn w3wp.exe -c \".loadby sos clr;!runaway;qd\"&exit");
        //        //p.StandardInput.WriteLine("D:\\aaa.bat&exit");

        //        //p.StandardInput.WriteLine(@"ddd&exit");
        //        //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
        //        //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

        //        p.StandardInput.WriteLine(@"ping www.baidu.com");
        //        p.StandardInput.WriteLine(@"exit");


        //        //获取cmd窗口的输出信息
        //        string output = p.StandardOutput.ReadToEnd();


        //        p.WaitForExit();//等待程序执行完退出进程
        //        p.Close();
        //        string message = output.Replace(">", "::");
        //        //Utility.Logger.Log(message);
        //        return message;
        //    });
           

        //}

        private void ExectueCmdForCDB()
        {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                //p.StartInfo.Arguments = "";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序
                p.StandardInput.AutoFlush = true;
                //向cmd窗口发送输入信息
                //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe&&.loadby sos clr&&g&&~*e!clrstack&&qd&exit");
                //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv&&.loadby sos clr&&!runaway&&qd&exit");
                //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv&&!runaway&&qd&exit");
                //p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe -pv");
                //p.StandardInput.WriteLine("D:\\WinDBGx64New\\cdb.exe -pn w3wp.exe -pv -c \".loadby sos clr;!runaway;qd\"&exit");

                //p.StandardInput.WriteLine("D:\\WinDBGx64New\\cdb.exe -pn w3wp.exe -pv -c \".loadby sos clr;!runaway;qd\"&exit");

                //p.StandardInput.WriteLine("D:\\WinDBGx64New\\cdb.exe -pn w3wp.exe -c \".loadby sos clr;!runaway;qd\"&exit");
                p.StandardInput.WriteLine("D:\\WinDBGx64New\\cdb.exe -pn w3wp.exe -c \"!runaway;qd\"&exit");


            //p.StandardInput.WriteLine("D:\\aaa.bat&exit");

            //p.StandardInput.WriteLine(@"ddd&exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();


                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                result = output.Replace(">", "::");
                //Utility.Logger.Log(message);
        }

        private void ExectueCmdForMdbgConsole(object spid )
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //p.StartInfo.FileName = @"D:\View\MYBLOG\CpuAnalyzer\bin\Debug\NETCpuAnalyzer.exe";
            p.StartInfo.FileName = @"D:\View\MYBLOG\cpuanalyzermaster\bin\Debug\cpuanalyzer.exe";

        

            p.StartInfo.Arguments = spid.ToString();
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            p.StandardInput.AutoFlush = true;
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine("exit");
            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();


            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            result = output.Replace(">", "::");
            using (var sr = new StreamReader(@"D:\View\MYBLOG\CpuAnalyzer\bin\Debug\w3wp.htm", Encoding.Default))
            {
                result += "<br />" + sr.ReadToEnd();
            }
            //Utility.Logger.Log(message);
        }

        private void ExectueCmdForMdbg()
        {
            int pid = Process.GetCurrentProcess().Id;

            result = "内部调用栈测试！";
            var sb = new StringBuilder();
            Process process = Process.GetProcessById(pid);
            var counters = new Dictionary<string, MyThreadCounterInfo>();
            var threadInfos = new Dictionary<string, MyThreadInfo>();
            //sb.AppendFormat(@"<html><head><title>{0}</title><style type=""text/css"">table, td{{border: 1px solid #000;border-collapse: collapse;}}</style></head><body>",process.ProcessName);
            
            //收集计数器
            var cate = new PerformanceCounterCategory("Thread");
            string[] instances = cate.GetInstanceNames();
            foreach (string instance in instances)
            {
                if (instance.StartsWith(process.ProcessName,StringComparison.CurrentCultureIgnoreCase))
                {
                    var counter1 = new PerformanceCounter("Thread", "ID Thread", instance, true);
                    var counter2 = new PerformanceCounter("Thread", "% Processor Time", instance,true);
                    counters.Add(instance,new MyThreadCounterInfo(counter1,counter2));
                }
            }
            foreach (var pair in counters)
            {
                try
                {
                    pair.Value.IdCounter.NextValue();
                    pair.Value.ProcessorTimeCounter.NextValue();
                }
                catch (Exception ex)
                {
                    //throw;
                    //todo 写到日志 zhanggangb
                }
            }

            Thread.Sleep(5000);
            foreach (var pair in counters)
            {
                try
                {
                    var info = new MyThreadInfo();
                    info.Id = pair.Value.IdCounter.NextValue().ToString();
                    info.ProcessorTimePercentage = pair.Value.ProcessorTimeCounter.NextValue().ToString("0.0");
                    threadInfos.Add(info.Id, info);
                }
                catch (Exception ex)
                {
                   // throw;
                }
            }

            //收到线程信息
            ProcessThreadCollection collection = process.Threads;
            foreach (ProcessThread thread in collection)
            {
                try
                {
                    MyThreadInfo info;
                    if (threadInfos.TryGetValue(thread.Id.ToString(), out info))
                    {
                        info.UserProcessorTime = thread.Id.ToString();
                        info.StartTime = thread.StartTime.ToString();
                    }
                }
                catch (Exception)
                {
                    //throw;
                }
            }


            var debugger = new MDbgEngine();
            debugger.Options.StopOnException = true;
            MDbgProcess proc = null;
            try
            {
                proc = debugger.Attach(pid);
                DrainAttach(debugger, proc);

                var tc = proc.Threads;
                //附加到进程获取调用栈
                foreach (MDbgThread t  in tc)
                {
                    var tempStrs = new StringBuilder();
                    foreach (MDbgFrame f in t.Frames)
                    {
                        tempStrs.AppendFormat("<br />" + f);
                    }
                    MyThreadInfo info;
                    if (threadInfos.TryGetValue(t.Id.ToString(),out info))
                    {
                        info.CallStack = tempStrs.Length == 0 ? "没有托管调用栈" : tempStrs.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
            finally
            {
                if (proc!=null)
                {
                    proc.Detach().WaitOne();
                }
            }

            //按照CPU占用率排序
            var threadresult = threadInfos.Values.ToArray().OrderByDescending(item => double.Parse(item.ProcessorTimePercentage));
            foreach (var info in threadresult)
            {
                sb.Append(info.ToString());
                sb.Append("<hr />");
            }
            //sb.Append("</body></html>")
            //Utility.Logger.Log(message);

            //生成报表
            //using (var sw = new StreamWriter(process.ProcessName+".html",false,Encoding.Default))
            //{
            //    sw.Write(sb.ToString());
            //}

            //process.Start(process.ProcessName + ".html");

            result = sb.ToString();

        }
        // Skip past fake attach events. 
        private static void DrainAttach(MDbgEngine debugger, MDbgProcess proc)
        {
            bool fOldStatus = debugger.Options.StopOnNewThread;
            debugger.Options.StopOnNewThread = false; // skip while waiting for AttachComplete

            proc.Go().WaitOne();
            Debug.Assert(proc.StopReason is AttachCompleteStopReason);

            debugger.Options.StopOnNewThread = true; // needed for attach= true; // needed for attach

            /*
            // Drain the rest of the thread create events.
            //proc
            while (proc.CorProcess.HasQueuedCallbacks(null))
            {
                proc.Go().WaitOne();
                Debug.Assert(proc.StopReason is ThreadCreatedStopReason);
            }*/

            debugger.Options.StopOnNewThread = fOldStatus;
        }

        public List<CommentInfo> GetCommentList(long blogId)
        {
            List<CommentInfo> list = commentInfoDal.GetCommentList(blogId);
            return list;
        }
        public int GetCommentCount(int pageSize)
        {
            int CommentCount = commentInfoDal.GetCommentCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)CommentCount / pageSize));
            return pageCount;
        }
        public List<BlogCommentInfo> GetCommentListByPage(int pageIndex,int pageSize)
        {
            int start = (pageIndex - 1) * pageSize+1;
            int end = pageIndex * pageSize;
            List<BlogCommentInfo> list = commentInfoDal.GetCommentListByPage(start,end);
            return list;
        }

        public bool DeletCommentInfo(long Commentid)
        {
            if (Commentid == 0)
            {
                return false;
            }
            return commentInfoDal.DeleteCommentInfo(Commentid) > 0;
        }

        internal class MyThreadInfo
        {
            public string CallStack = "null";
            public string Id;
            public string ProcessorTimePercentage;
            public string StartTime;
            public string UserProcessorTime;

            public override string ToString()
            {
                return
                   string.Format(
                       @"<table style=""width: 1000px;""><tr><td style=""width: 80px;"">ThreadId</td><td style=""width: 200px;"">{0}</td><td style=""width: 140px;"">% Processor Time</td><td>{1}</td></tr>
<tr><td style=""width: 80px;"">UserProcessorTime</td><td style=""width: 200px;"">{2}</td><td style=""width: 140px;"">StartTime</td><td>{3}</td></tr><tr><td colspan=""4"">{4}</td></tr></table>",
                       Id, ProcessorTimePercentage, UserProcessorTime, StartTime, CallStack);
            }
        }

        internal class MyThreadCounterInfo
        {
            public PerformanceCounter IdCounter;
            public PerformanceCounter ProcessorTimeCounter;
            public MyThreadCounterInfo(PerformanceCounter counter1,PerformanceCounter counter2)
            {
                IdCounter = counter1;
                ProcessorTimeCounter = counter2; 
            }
        }
    }
}

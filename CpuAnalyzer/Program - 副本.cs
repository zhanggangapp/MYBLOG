using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Samples.Debugging.MdbgEngine;
using System.Linq;

namespace NETCpuAnalyzer
{
    class Program
    {
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

        // Expects 1 arg, the pid as a decimal string
        private static void Main(string[] args)
        {
            try
            {
                int pid = int.Parse(args[0]);

                var sb = new StringBuilder();
                Process process = Process.GetProcessById(pid);

                var counters = new Dictionary<string, MyThreadCounterInfo>();
                var threadInfos = new Dictionary<string, MyThreadInfo>();


                //zhanggangb

                sb.AppendFormat(
                    @"<html><head><title>{0}</title><style type=""text/css"">table, td{{border: 1px solid #000;border-collapse: collapse;}}</style></head><body>",
                    process.ProcessName);

                Console.WriteLine("1、正在收集计数器");

                var cate = new PerformanceCounterCategory("Thread");
                string[] instances = cate.GetInstanceNames();
                foreach (string instance in instances)
                {
                    if (instance.StartsWith(process.ProcessName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var counter1 =
                            new PerformanceCounter("Thread", "ID Thread", instance, true);
                        var counter2 =
                            new PerformanceCounter("Thread", "% Processor Time", instance, true);
                        counters.Add(instance, new MyThreadCounterInfo(counter1, counter2));
                    }
                }

                
                foreach (var pair in counters)
                {
                    try
                    { 
                    pair.Value.IdCounter.NextValue();
                    pair.Value.ProcessorTimeCounter.NextValue();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Console.WriteLine("2、正在收集线程信息");
                ProcessThreadCollection collection = process.Threads;
                foreach (ProcessThread thread in collection)
                {
                    try
                    {
                        MyThreadInfo info;
                        if (threadInfos.TryGetValue(thread.Id.ToString(), out info))
                        {
                            info.UserProcessorTime = thread.UserProcessorTime.ToString();
                            info.StartTime = thread.StartTime.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
                    Console.WriteLine("3、正在附加到进程{0}获取调用栈", pid);
                    foreach (MDbgThread t in tc)
                    {
                        var tempStrs = new StringBuilder();
                        foreach (MDbgFrame f in t.Frames)
                        {
                            tempStrs.AppendFormat("<br />" + f);
                        }
                        MyThreadInfo info;
                        if (threadInfos.TryGetValue(t.Id.ToString(), out info))
                        {
                            info.CallStack = tempStrs.Length == 0 ? "no managment call stack" : tempStrs.ToString();
                        }
                    }
                }
                finally
                {
                    if (proc != null)
                    {
                        proc.Detach().WaitOne();
                    }
                }

                //按照cpu占用率排序 
                var result = threadInfos.Values.ToArray().OrderByDescending(item => double.Parse(item.ProcessorTimePercentage));


                foreach (var info in result)
                {
                    sb.Append(info.ToString());
                    sb.Append("<hr />");
                }
                sb.Append("</body></html>");

                Console.WriteLine("4、正在生成报表");
                using (var sw = new StreamWriter(@"D:\View\MYBLOG\CpuAnalyzer\bin\Debug\"+process.Id + ".htm", false,
                                                 Encoding.Default))
                {
                    sw.Write(sb.ToString());
                }

                Process.Start(@"D:\View\MYBLOG\CpuAnalyzer\bin\Debug\"+process.Id + ".htm" );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("分析结束");
            Console.ReadKey();
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

            public MyThreadCounterInfo(PerformanceCounter counter1, PerformanceCounter counter2)
            {
                IdCounter = counter1;
                ProcessorTimeCounter = counter2;
            }
        }


    }
}

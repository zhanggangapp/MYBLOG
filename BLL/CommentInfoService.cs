using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Web;

namespace BLL
{
   public class CommentInfoService
    {
        DAL.ICommentInfoDal commentInfoDal = new CommentInfoSQLDal();
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

        public string DbgAdd()
        {
            return ExectueCmdForDbg();
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

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(@"D:\WinDBGx64New\cdb.exe -pn w3wp.exe&&.loadby sos clr&&g&&~*e!clrstack&&qd&exit");

            //p.StandardInput.WriteLine(@"ddd&exit");

            p.StandardInput.AutoFlush = true;
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();


            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return output;

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
    }
}

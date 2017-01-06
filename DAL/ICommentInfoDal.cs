using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICommentInfoDal
    {
        //List<CommentInfo> GetCommentListByPage(int start, int end);
        List<BlogCommentInfo> GetCommentListByPage(int start, int end);
        List<CommentInfo> GetCommentList(long blogId);
        int AddCommentInfo(long blogId, string userName, string comment);
        int DeleteCommentInfo(long CommentId);
        int GetCommentCount();
        
    }
}

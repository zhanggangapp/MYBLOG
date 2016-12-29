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
        List<CommentInfo> GetCommentList(long blogId);
        void LoadEntity(DataRow row, CommentInfo commentInfo);
        int AddCommentInfo(long blogId, string userName, string comment);
        int DeleteCommentInfo(long CommentId);
        
    }
}

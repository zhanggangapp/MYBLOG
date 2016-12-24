using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace BLL
{
   public class CommentInfoService
    {
        DAL.CommentInfoDal commentInfoDal = new CommentInfoDal();
        public bool AddComment(int blogId,string userName,string comment)
        {
            if (blogId==0 || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(comment))
            {
                return false;
            }
            return commentInfoDal.AddCommentInfo(blogId, userName, comment) > 0;
        }
        public List<CommentInfo> GetCommentList(int blogId)
        {
            List<CommentInfo> list = commentInfoDal.GetCommentList(blogId);
            return list;
        }
    }
}

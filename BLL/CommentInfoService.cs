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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    class CommentInfoRedisDal : ICommentInfoDal
    {
        public int AddCommentInfo(long blogId, string userName, string comment)
        {
            throw new NotImplementedException();
        }

        public int DeleteCommentInfo(long CommentId)
        {
            throw new NotImplementedException();
        }

        public int GetCommentCount()
        {
            throw new NotImplementedException();
        }

        public List<CommentInfo> GetCommentList(long blogId)
        {
            throw new NotImplementedException();
        }

        public List<BlogCommentInfo> GetCommentListByPage(int start, int end)
        {
            throw new NotImplementedException();
        }
    }
}

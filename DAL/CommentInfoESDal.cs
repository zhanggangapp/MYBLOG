using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;
namespace DAL
{
    public class CommentInfoESDal : ICommentInfoDal
    {
        public int AddCommentInfo(int blogId, string userName, string comment)
        {

            var commentinfo = new CommentInfo
            {
                Id = Convert.ToInt64(System.DateTime.Now.ToString("yyyyMMddhhmmssffff")),
                BlogId = blogId,
                UserName = userName,
                Comment = comment,
                CreatedTime = System.DateTime.Now
            };
            //var response = ESHelper.client.Index(commentinfo, idx => idx.Index("myblog"));
            var response = ESHelper.client.Index(commentinfo);//不用指定索引名了
            if (response.Created == true)
                return 1;
            return 0;
        }

        public int DeleteCommentInfo(int CommentId)
        {

            return 0;
        }

        public List<CommentInfo> GetCommentList(int blogId)
        {
            return null;
        }

        public void LoadEntity(DataRow row, CommentInfo commentInfo)
        {
        }
    }
}

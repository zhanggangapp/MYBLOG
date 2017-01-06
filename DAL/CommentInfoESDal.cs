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
        public int AddCommentInfo(long blogId, string userName, string comment)
        {

            var commentinfo = new CommentInfo
            {
                CommentId = Convert.ToInt64(System.DateTime.Now.ToString("yyyyMMddhhmmss")),
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

        public int DeleteCommentInfo(long CommentId)
        {
            var response = ESHelper.client.Delete<CommentInfo>(CommentId);
            if (response.Result == Nest.Result.Deleted)
                return 1;
            return 0;
        }

        public int GetCommentCount()
        {
            var response = ESHelper.client.Count<CommentInfo>(c=>c.Query(q=>q.MatchAll()));
            return Convert.ToInt32(response.Count);
        }

        public List<CommentInfo> GetCommentList(long blogId)
        {
            var response = ESHelper.client.Search<CommentInfo>(s => s.Query(q => q.Term(t => t.BlogId, blogId)).Sort(x => x.Ascending(p => p.CommentId)));
            List<CommentInfo> list = null;
            if (response.Documents.Count > 0)
            {
                list = new List<CommentInfo>();
                foreach (CommentInfo document in response.Documents)
                    list.Add(document);
            }
            return list;
        }
        public List<BlogCommentInfo> GetCommentListByPage(int start, int end)
        {
            //todo 混合查询,一块查出blog的title-- nested  has-child  has-parent似乎做不到.后续再研究.
            //var response = ESHelper.client.Search<CommentInfo>(s=>s.Query(q=>q.MatchAll()).From(start-1).Size(end-start).Sort(ss=>ss.Descending(p=>p.CommentId)));
            var response = ESHelper.client.Search<CommentInfo>(s => s.Query(q => q.MatchAll()).From(start - 1).Size(end - start).Sort(ss => ss.Descending(p => p.CommentId)));

            List<BlogCommentInfo> list = null;
            if (response.Documents.Count>0)
            {
                list = new List<BlogCommentInfo>();
                BlogCommentInfo bcinfo;
                foreach (CommentInfo document in response.Documents)
                {
                    bcinfo = new BlogCommentInfo();
                    LoadEntity(document, bcinfo);
                   
                    list.Add(bcinfo);
                }
            }
            return list;
        }
        private void LoadEntity(CommentInfo document, BlogCommentInfo bcinfo)
        {
            bcinfo.BlogId = document.BlogId;
            bcinfo.Comment = document.Comment;
            bcinfo.CommentId = document.CommentId;
            bcinfo.CreatedTime = document.CreatedTime;
            bcinfo.UserName = document.UserName;
            //再查一次博客标题
            bcinfo.Title = GetBlogTitleById(document.BlogId);
        }

        public string GetBlogTitleById(long id)
        {
            //var response = ESHelper.client.Search<BlogInfo>(s => s.Query(q => q.Term(p => p.BlogId, id)));
            var response = ESHelper.client.Search<BlogInfo>(s=>s.Query(q=>q.Term(p=>p.BlogId,id)).Source(ss=>ss.Includes(i=>i.Field(f=>f.Title))));
            if (response.Documents.Count>0)
                return response.Documents.First().Title;
            return "此博客已删除";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;
using System.Text.RegularExpressions;

namespace DAL
{
    public class BlogInfoESDal : IBlogInfoDal
    {
        public int AddBlogInfo(string title,string content)
        {
            long blogId = Convert.ToInt64(System.DateTime.Now.ToString("yyyyMMddhhmmss"));
            var bloginfo = new BlogInfo
            {
                BlogId = blogId,
                Title=title,
                Content=content,
                CreatedTime=System.DateTime.Now
            };
            //var response = ESHelper.client.Index(bloginfo,idx=>idx.Index("myblog"));
            var response = ESHelper.client.Index(bloginfo);
            if (response.Created == true)
                return 1;
            return 0;
        }

        public int DeleteBlogInfo(long blogid)
        {
            //先删除掉博客的评论(集合）
            //var response0 = ESHelper.client.Delete<CommentInfo>(d => d.BlogId(blogid));
            //ESHelper.client.DeleteByQuery<CommentInfo>(dq => dq.Query(q => q.Term(tr => tr.Field(f => f.BlogId, blogid))));
            ESHelper.client.DeleteByQuery<CommentInfo>(dq => dq.Query(q => q.Term(tr => tr.Field(f=>f.BlogId).Value(blogid))));


            var response = ESHelper.client.Delete<BlogInfo>(blogid);
            if (response.Result == Nest.Result.Deleted)
                return 1;
            return 0;
        }

        public int EditBlogInfo(long blogid, string title, string content)
        {
            var bloginfo = new BlogInfo
            {
                BlogId = blogid,
                Title = title,
                Content = content,
                CreatedTime = System.DateTime.Now
            };

            var response = ESHelper.client.Index(bloginfo);
            if (response.Result == Nest.Result.Updated)
                return 1;            
            return 0;
        }

        public BlogInfo GetBlogById(long id)
        {
            //var response = ESHelper.client.Get<BlogInfo>(Id, idx => idx.Index("myblog"));
            var response = ESHelper.client.Search<BlogInfo>(s => s.Query(q=>q.Term(p=>p.BlogId, id)));
            return response.Documents.First();

            //var bloginfo = response.Documents.First();
            //return new BlogInfo()
            //{
            //    BlogId = bloginfo.BlogId,
            //    Title = bloginfo.Title,
            //    Content = bloginfo.Content,
            //    CreatedTime = bloginfo.CreatedTime
            //};
        }

        public int GetBlogCount()
        {
            var response = ESHelper.client.Count<BlogInfo>(c => c.Query(q=>q.MatchAll()));
            return Convert.ToInt32(response.Count);
        }

        public int GetBlogListByKeyWordCount(string keyword)
        {
            return 1;
        }

        public List<BlogInfo> GetBlogListByPage(int start, int end)
        {
            //var response = ESHelper.client.Search<BlogInfo>(s => s.From(start).Size(end-start).Query(q=>q.Term(t=>t.Title,"博客")).Sort(x=>x.Field("Id",Nest.SortOrder.Descending)));
            var response = ESHelper.client.Search<BlogInfo>(s => s.Query(q => q.MatchAll()).From(start-1).Size(end-start).Sort(ss=>ss.Descending(p=>p.BlogId)));
            List<BlogInfo> list = null;
            if (response.Documents.Count>0)
            {
                list = new List<BlogInfo>();
                Regex re = new Regex(@"<p[\w\W]*?>(?<content>[\w\W]*?)</p>");
                foreach (BlogInfo document in response.Documents)
                {
                    Match m = re.Match(document.Content);
                    if (m.Success)
                        document.Content = m.Groups["content"].Value;
                    list.Add(document);
                }
            }
            return list;
        }

        public List<BlogInfo> GetBlogTitleListByKeyWordPage(int start, int end, string keyword)
        {
            var key = string.Format("*{0}*",keyword);
            var response = ESHelper.client.Search<BlogInfo>(s=>s.Query(q=>q.QueryString(t=>t.Query(key).DefaultOperator(Nest.Operator.Or))).From(start-1).Size(end-start).Sort(ss=>ss.Descending(pp=>pp.BlogId)));
            List<BlogInfo> list = null;
            if (response.Documents.Count>0)
            {
                list = new List<BlogInfo>();
                foreach (BlogInfo documnet in response.Documents)
                {
                    list.Add(documnet);
                }
                return list;
            }
            return list;
        }

        public List<BlogInfo> GetBlogTitleListByPage(int start, int end)
        {
            var response = ESHelper.client.Search<BlogInfo>(s=>s.Query(q=>q.MatchAll()).From(start-1).Size(end-start).Sort(ss=>ss.Descending(p=>p.BlogId)));
            //todo:仅仅返回blodid\title\creattime(无content)时,如何写?  
            List<BlogInfo> list = null;
            if (response.Documents.Count>0)
            {
                list = new List<BlogInfo>();
                foreach (BlogInfo document in response.Documents)
                    list.Add(document);
            }
            return list;
        }
    }
}

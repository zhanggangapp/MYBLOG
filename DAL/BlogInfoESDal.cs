﻿using System;
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
            long blogId = Convert.ToInt64(System.DateTime.Now.ToString("yyyyMMddhhmmssffff"));
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
            var response = ESHelper.client.Delete<BlogInfo>(blogid);
            return 0;
        }

        public int EditBlogInfo(long blogid, string title, string content)
        {
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
            return 1;
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
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogTitleListByPage(int start, int end)
        {
            throw new NotImplementedException();
        }

        public void LoadTitleEntity(DataRow row, BlogInfo blogInfo)
        {
            throw new NotImplementedException();
        }
    }
}

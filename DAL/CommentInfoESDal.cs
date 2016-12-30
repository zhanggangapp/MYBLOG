﻿using System;
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

            return 0;
        }

        public List<CommentInfo> GetCommentList(long blogId)
        {
            var response = ESHelper.client.Search<CommentInfo>(s=>s.Query(q=>q.Term(t=>t.BlogId,blogId)).Sort(x=>x.Ascending(p=>p.CommentId)));
            List<CommentInfo> list = null;
            if (response.Documents.Count>0)
            {
                foreach (CommentInfo document in response.Documents)
                    list.Add(document);
            }
            return list;
        }

        public void LoadEntity(DataRow row, CommentInfo commentInfo)
        {
        }
    }
}
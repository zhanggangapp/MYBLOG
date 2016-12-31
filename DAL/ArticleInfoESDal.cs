using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;
namespace DAL
{
    public class ArticleInfoESDal : IArticleInfoDal
    {
        public List<ArticleEntity> GetArticleList(DateTime beginTime)
        {
            //string sql = "SELECT BlogId,Title,CreatedTime,Content FROM dbo.BlogInfo WHERE CreatedTime>@beginTime ORDER BY BlogId DESC";
            var response = ESHelper.client.Search<BlogInfo>(s=>s.Query(q=>q.Bool(b=>b.Should(sd=>sd.TermRange(tr=>tr.GreaterThan(System.DateTime.Now.AddDays
                (-30).ToString("yyyyMMddhhmmss")).Field(f=>f.BlogId))))).Sort(ss=>ss.Descending(p=>p.BlogId)));

            List<ArticleEntity> list = null;
            if (response.Documents.Count>0)
            {
                list = new List<ArticleEntity>();
                ArticleEntity articleInfo = null;
                foreach (BlogInfo documment in response.Documents)
                {
                    articleInfo = new ArticleEntity();
                    LoadArticleEntity(documment, articleInfo);
                    list.Add(articleInfo);
                }
            }
            return list;
        }
        private void LoadArticleEntity(BlogInfo bloginfo, ArticleEntity articleInfo)
        {
            articleInfo.BlogId = bloginfo.BlogId;
            articleInfo.Title = bloginfo.Title;
            articleInfo.PostTime = bloginfo.CreatedTime;
        }
    }
   

}

//ArticleEntity
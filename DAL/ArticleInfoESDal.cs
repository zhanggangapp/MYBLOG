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
            //var response = ESHelper.client.Search<BlogInfo>(s=>s.Query(q=>q.Range(r=>r.Field(f=>f.CreatedTime))).Sort(ss=>ss.Descending(p=>p.BlogId)));

            //SqlParameter[] pars = {
            //   new SqlParameter("@beginTime",System.Data.SqlDbType.DateTime)
            //};
            //pars[0].Value = beginTime;
            //DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pars);
            //List<ArticleEntity> list = null;
            //if (da.Rows.Count > 0)
            //{
            //    list = new List<ArticleEntity>();
            //    ArticleEntity articleInfo = null;
            //    foreach (DataRow row in da.Rows)
            //    {
            //        articleInfo = new ArticleEntity();
            //        LoadArticleEntity(row, articleInfo);
            //        list.Add(articleInfo);
            //    }
            //}
            return list;
        }
    }
}

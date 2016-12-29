using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
namespace DAL
{
    public class ArticleInfoDal
    {
        public List<ArticleEntity> GetArticleList(DateTime beginTime)
        {
            string sql = "SELECT BlogId,Title,CreatedTime,Content FROM dbo.BlogInfo WHERE CreatedTime>@beginTime ORDER BY BlogId DESC";
            SqlParameter[] pars = {
               new SqlParameter("@beginTime",System.Data.SqlDbType.DateTime)
            };
            pars[0].Value = beginTime;
            DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pars);
            List<ArticleEntity> list = null;
            if (da.Rows.Count>0)
            {
                list = new List<ArticleEntity>();
                ArticleEntity articleInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    articleInfo = new ArticleEntity();
                    LoadArticleEntity(row, articleInfo);
                    list.Add(articleInfo);
                }
            }
            return list;
        }
        private void LoadArticleEntity(DataRow row, ArticleEntity articleInfo)
        {
            articleInfo.BlogId = Convert.ToInt64(row["BlogId"]);
            articleInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            articleInfo.PostTime = Convert.ToDateTime(row["CreatedTime"]);
        }
    }
}

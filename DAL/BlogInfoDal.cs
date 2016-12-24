using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utility;
using Model;
using System.Text.RegularExpressions;

namespace DAL
{
    public class BlogInfoDal
    {
        public List<BlogInfo> GetBlogListByPage(int start, int end)
        {
            string sql = "SELECT * FROM ( SELECT ROW_NUMBER() OVER (ORDER BY id DESC ) AS num, * FROM dbo.BlogInfo ) AS t WHERE t.num>=@start AND t.num<=@end";
            SqlParameter[] pars = {
                //new SqlParameter("@start",start),
                //new SqlParameter("@end",end)
                new SqlParameter("@start",SqlDbType.Int),
                new SqlParameter("@end",SqlDbType.Int)
            };
            pars[0].Value = start;
            pars[1].Value = end;
            DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pars);
            List<BlogInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<BlogInfo>();
                BlogInfo blogInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    blogInfo = new BlogInfo();
                    LoadEntity(row, blogInfo);
                    list.Add(blogInfo);
                }
            }
            return list;
        }
        public List<BlogInfo> GetBlogTitleListByPage(int start, int end)
        {
            string sql = "SELECT Id, Title,CreatedTime FROM ( SELECT ROW_NUMBER() OVER (ORDER BY id DESC ) AS num, Id,Title,CreatedTime FROM dbo.BlogInfo ) AS t WHERE t.num>=@start AND t.num<=@end";
            SqlParameter[] pars = {
                //new SqlParameter("@start",start),
                //new SqlParameter("@end",end)
                new SqlParameter("@start",SqlDbType.Int),
                new SqlParameter("@end",SqlDbType.Int)
            };
            pars[0].Value = start;
            pars[1].Value = end;
            DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pars);
            List<BlogInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<BlogInfo>();
                BlogInfo blogInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    blogInfo = new BlogInfo();
                    LoadTitleEntity(row, blogInfo);
                    list.Add(blogInfo);
                }
            }
            return list;
        }

        public List<BlogInfo> GetBlogTitleListByKeyWordPage(int start, int end,string keyword)
        {
            string sql = "SELECT Id, Title,CreatedTime FROM ( SELECT ROW_NUMBER() OVER (ORDER BY id DESC ) AS num, Id,Title,CreatedTime FROM dbo.BlogInfo where Title like @Title ) AS t WHERE t.num>=@start AND t.num<=@end";
            SqlParameter[] pars = {
                //new SqlParameter("@start",start),
                //new SqlParameter("@end",end)
                new SqlParameter("@Title",SqlDbType.NVarChar),
                new SqlParameter("@start",SqlDbType.Int),
                new SqlParameter("@end",SqlDbType.Int)
            };
            pars[0].Value = '%'+keyword+'%';
            pars[1].Value = start;
            pars[2].Value = end;
            DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pars);
            List<BlogInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<BlogInfo>();
                BlogInfo blogInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    blogInfo = new BlogInfo();
                    LoadTitleEntity(row, blogInfo);
                    list.Add(blogInfo);
                }
            }
            return list;
        }
        public int GetBlogCount()
        {
            string sql = "select count(*) from BlogInfo";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        public int GetBlogListByKeyWordCount(string keyword)
        {
            string sql = "select count(*) from BlogInfo where Title like @title";
            SqlParameter[] pars = {
                new SqlParameter("@title",'%'+keyword+'%')
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql,CommandType.Text,pars));
        }
        
        public BlogInfo GetBlogById(int id)
        {
            string sql = "SELECT * FROM dbo.BlogInfo WHERE Id=@Id";
            SqlParameter[] pars = { new SqlParameter("@Id", id) };
            SqlDataReader dr = SqlHelper.ExecuteReader(sql, CommandType.Text, pars);
            BlogInfo blogInfo = new BlogInfo();
            if (dr.HasRows && dr.Read())
            {
                blogInfo.Id = id;
                blogInfo.Title = dr["Title"].ToString();
                blogInfo.Content = dr["Content"].ToString();
                blogInfo.CreatedTime = Convert.ToDateTime(dr["CreatedTime"]);
                dr.Close();
            }
            return blogInfo;

        }
        public int AddBlogInfo(string title,string content)
        {
            string sql = "INSERT INTO dbo.BlogInfo( Title, Content) VALUES  ( @title,@content)";
            SqlParameter[] pars = { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NVarChar) };
            pars[0].Value = title;
            pars[1].Value = content;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }
        public int EditBlogInfo(int blogid,string title, string content)
        {
            string sql = "UPDATE dbo.BlogInfo SET Title=@title,Content=@content WHERE id =@id";
            SqlParameter[] pars = { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NVarChar),new SqlParameter("@id",SqlDbType.Int) };
            pars[0].Value = title;
            pars[1].Value = content;
            pars[2].Value = blogid;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }
        public int DeleteBlogInfo(int blogid)
        {
            string sql = "delete dbo.BlogInfo WHERE id =@id";
            SqlParameter[] pars = { new SqlParameter("@id", SqlDbType.Int) };
            pars[0].Value = blogid;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }
        
        private void LoadEntity(DataRow row, BlogInfo blogInfo)
        {
            blogInfo.Id = Convert.ToInt32(row["Id"]);
            blogInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            //首页只显示第一个<p></p>内容即可.
            Regex re = new Regex("<p>(?<content>[\\w\\W]*?)</p>");
            Match m = re.Match(row["Content"].ToString());
            if (m.Success)
                blogInfo.Content = m.Groups["content"].Value.Substring(0, m.Groups["content"].Value.Length < 300 ? m.Groups["content"].Length : 300);
            else
                blogInfo.Content = row["Content"].ToString();
            blogInfo.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
        }
        private void LoadTitleEntity(DataRow row, BlogInfo blogInfo)
        {
            blogInfo.Id = Convert.ToInt32(row["Id"]);
            blogInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            blogInfo.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
        }
    }
}

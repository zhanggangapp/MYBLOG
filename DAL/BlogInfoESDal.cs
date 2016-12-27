using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class BlogInfoESDal : IBlogInfoDal
    {
        public int AddBlogInfo(string title,string content)
        {
            //string sql = "INSERT INTO dbo.BlogInfo( Title, Content) VALUES  ( @title,@content)";
            //SqlParameter[] pars = { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NVarChar) };
            //pars[0].Value = title;
            //    pars[1].Value = content;
            //    int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            //    return result;
            return 1;
        }

        public int DeleteBlogInfo(int blogid)
        {
            throw new NotImplementedException();
        }

        public int EditBlogInfo(int blogid, string title, string content)
        {
            throw new NotImplementedException();
        }

        public BlogInfo GetBlogById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetBlogCount()
        {
            throw new NotImplementedException();
        }

        public int GetBlogListByKeyWordCount(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogListByPage(int start, int end)
        {
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogTitleListByKeyWordPage(int start, int end, string keyword)
        {
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogTitleListByPage(int start, int end)
        {
            throw new NotImplementedException();
        }

        public void LoadEntity(DataRow row, BlogInfo blogInfo)
        {
            throw new NotImplementedException();
        }

        public void LoadTitleEntity(DataRow row, BlogInfo blogInfo)
        {
            throw new NotImplementedException();
        }
    }
}

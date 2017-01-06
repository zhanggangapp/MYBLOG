using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class CommentInfoSQLDal:ICommentInfoDal
    {
        public List<CommentInfo> GetCommentList(long blogId)
        {
            string sql = "SELECT * FROM dbo.CommentInfo WHERE BlogId =@BlogId order by Commentid";
            SqlParameter[] pars = {
                new SqlParameter("@BlogId",SqlDbType.BigInt)
            };
            pars[0].Value = blogId;
            DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pars);
            List<CommentInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<CommentInfo>();
                CommentInfo commentInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    commentInfo = new CommentInfo();
                    LoadEntity(row, commentInfo);
                    list.Add(commentInfo);
                }
            }
            return list;
        }
        public int GetCommentCount()
        {
            string sql = "SELECT count(*) FROM dbo.CommentInfo";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        //public List<CommentInfo> GetCommentListByPage(int start, int end)
        //{
        //    string sql = "SELECT * FROM dbo.CommentInfo order by Commentid desc";
        //    DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text,null);
        //    List<CommentInfo> list = null;
        //    if (da.Rows.Count > 0)
        //    {
        //        list = new List<CommentInfo>();
        //        CommentInfo commentInfo = null;
        //        foreach (DataRow row in da.Rows)
        //        {
        //            commentInfo = new CommentInfo();
        //            LoadEntity(row, commentInfo);
        //            list.Add(commentInfo);
        //        }
        //    }
        //    return list;
        //}
        private void LoadEntity(DataRow row, CommentInfo commentInfo)
        {
            commentInfo.CommentId = Convert.ToInt32(row["CommentId"]);
            commentInfo.UserName = row["UserName"].ToString();
            commentInfo.Comment = row["Comment"].ToString();
            commentInfo.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
        }
        private void LoadEntity(DataRow row, BlogCommentInfo commentInfo)
        {
            commentInfo.CommentId = Convert.ToInt32(row["CommentId"]);
            commentInfo.UserName = row["UserName"].ToString();
            commentInfo.Comment = row["Comment"].ToString();
            commentInfo.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
            commentInfo.Title = row["Title"].ToString();
        }
        public int AddCommentInfo(long blogId,string userName, string comment)
        {
            string sql = "INSERT INTO dbo.CommentInfo(BlogId, UserName, Comment) VALUES  (@blogId,@userName,@comment)";
            SqlParameter[] pars = { new SqlParameter("@blogId",SqlDbType.BigInt), new SqlParameter("@userName", SqlDbType.NVarChar), new SqlParameter("@comment", SqlDbType.NVarChar) };
            pars[0].Value = blogId;
            pars[1].Value = userName;
            pars[2].Value = comment;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }
        public int DeleteCommentInfo(long CommentId)
        {
            string sql = "delete dbo.CommentInfo where CommentId=@CommentId";
            SqlParameter[] pars = { new SqlParameter("@CommentId", SqlDbType.BigInt)};
            pars[0].Value = CommentId;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }

        List<BlogCommentInfo> ICommentInfoDal.GetCommentListByPage(int start, int end)
        {
            string sql = "SELECT c.*,b.Title FROM dbo.CommentInfo c,dbo.BlogInfo b where c.BlogId=b.BlogId order by Commentid desc";
            DataTable da = SqlHelper.ExecuteDataTable(sql, CommandType.Text, null);
            List<BlogCommentInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<BlogCommentInfo>();
                BlogCommentInfo commentInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    commentInfo = new BlogCommentInfo();
                    LoadEntity(row, commentInfo);
                    list.Add(commentInfo);
                }
            }
            return list;
        }
    }
}

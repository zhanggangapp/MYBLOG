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
   public class CommentInfoDal
    {
        public List<CommentInfo> GetCommentList(int blogId)
        {
            string sql = "SELECT * FROM dbo.CommentInfo WHERE BlogId =@BlogId order by id";
            SqlParameter[] pars = {
                new SqlParameter("@BlogId",SqlDbType.Int)
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
        private void LoadEntity(DataRow row,CommentInfo commentInfo)
        {
            commentInfo.Id = Convert.ToInt32(row["Id"]);
            commentInfo.UserName = row["UserName"].ToString();
            commentInfo.Comment = row["Comment"].ToString();
            commentInfo.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
        } 
        public int AddCommentInfo(int blogId,string userName, string comment)
        {
            string sql = "INSERT INTO dbo.CommentInfo(BlogId, UserName, Comment) VALUES  (@blogId,@userName,@comment)";
            SqlParameter[] pars = { new SqlParameter("@blogId",SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar), new SqlParameter("@comment", SqlDbType.NVarChar) };
            pars[0].Value = blogId;
            pars[1].Value = userName;
            pars[2].Value = comment;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }
        public int DeleteCommentInfo(int CommentId)
        {
            string sql = "delete dbo.CommentInfo where Id=@CommentId";
            SqlParameter[] pars = { new SqlParameter("@CommentId", SqlDbType.Int)};
            pars[0].Value = CommentId;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
            return result;
        }
    }
}

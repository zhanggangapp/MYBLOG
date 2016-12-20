using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class UserInfoDal
    {
        public bool IsExist(string email,string password)
        {
            string sql = "SELECT COUNT(*) FROM dbo.UserInfo WHERE Email=@email and Password=@password";
            SqlParameter[] pars = {
                new SqlParameter("@email",email),new SqlParameter("@password",password)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql,CommandType.Text,pars))>0;
        }

    }
}

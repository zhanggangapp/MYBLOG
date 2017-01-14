using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace BLL
{
    public class UserInfoService
    {
        DAL.UserInfoDal userInfoDal = new UserInfoDal();
        public bool IsExist(string email,string password)
        {
            return userInfoDal.IsExist(email,password);
        }
        //使用out参数将报错信息返回。
        public bool IsExist(string email,string password,out string msg)
        {
            bool isOk = userInfoDal.IsExist(email, password);
            if (isOk)
            {
                msg = "登陆成功";
                return isOk;
            }
            else
            {
                msg = "登陆失败";
                return false;
            }
        }
    }
}

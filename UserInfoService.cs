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
    }
}

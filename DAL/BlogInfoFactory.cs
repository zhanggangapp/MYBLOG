using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
   public class BlogInfoFactory
    {
        public static IBlogInfoDal Create()
        {
            IBlogInfoDal blogInfoDal;
            int DataType = Convert.ToInt32(HttpContext.Current.Application["DataType"]);
            switch (DataType)
            {
                case 0:
                    blogInfoDal = new BlogInfoSQLDal();
                    break;
                case 2:
                    blogInfoDal = new BlogInfoESDal();
                    break;
                default:
                    blogInfoDal = new BlogInfoSQLDal();
                    break;
            }
            return blogInfoDal;
        }
    }
}

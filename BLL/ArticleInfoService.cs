using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Web;

namespace BLL
{
    public class ArticleInfoService
    {

        DAL.IArticleInfoDal articleInfoDal = new ArticleInfoSQLDal();
        public List<ArticleEntity> GetArticleList(DateTime beginTime)
        {
            int DataType = Convert.ToInt32(HttpContext.Current.Application["DataType"]);
            switch (DataType)
            {
                case 0:
                    articleInfoDal = new ArticleInfoSQLDal();
                    break;
                case 2:
                    articleInfoDal = new ArticleInfoESDal();
                    break;
                default:
                    articleInfoDal = new ArticleInfoSQLDal();
                    break;
            }
            List<ArticleEntity> list = articleInfoDal.GetArticleList(beginTime);
            return list;
        }
    }
}

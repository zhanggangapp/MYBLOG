using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    public class ArticleInfoService
    {
        DAL.ArticleInfoDal articleInfoDal = new ArticleInfoDal();
        public List<ArticleEntity> GetArticleList(DateTime beginTime)
        {
            List<ArticleEntity> list = articleInfoDal.GetArticleList(beginTime);
            return list;
        }
    }
}

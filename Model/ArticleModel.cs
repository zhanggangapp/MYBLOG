using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class ArticleModel
    {
        public static IList<ArticleEntity> GetList()
        {
            IList<ArticleEntity> articleList = new List<ArticleEntity>();
            ArticleEntity article = null;
            for (int i = 0; i < 10; i++)
            {
                article = new ArticleEntity();
                article.ID = i;
                article.Title = string.Format("第{0}篇测试文章-标题加长加长再加长",i);
                article.Content = string.Format("第{0}篇测试文章--正文部分，内容页面内容页面",i);
                article.PostUser = "zhanggang" + i.ToString();
                articleList.Add(article);
            }
            return articleList;
        }
    }
}

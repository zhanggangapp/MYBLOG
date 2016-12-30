using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Web;

namespace BLL
{
    public class BlogInfoService
    {
        IBlogInfoDal blogInfoDal;

        public BlogInfoService()
        {
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
        }

        public List<BlogInfo> GetBlogListByPag(int pageIndex,int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<BlogInfo> list = blogInfoDal.GetBlogListByPage(start, end);
            return list;
        }
        public List<BlogInfo> GetBlogTitleListByPage(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<BlogInfo> list = blogInfoDal.GetBlogTitleListByPage(start, end);
            return list;
        }

        public List<BlogInfo> GetBlogTitleListByKeyWord(int pageIndex, int pageSize,string keyword)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<BlogInfo> list = blogInfoDal.GetBlogTitleListByKeyWordPage(start, end,keyword);
            return list;
        }

        public int GetBlogPageCount(int pageSize)
        {
            int blogCount = blogInfoDal.GetBlogCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)blogCount / pageSize));
            return pageCount;
        }
        public int GetBlogListCountByKeyWord(int pageSize,string keyword)
        {
            int blogCount = blogInfoDal.GetBlogListByKeyWordCount(keyword);
            int pageCount = Convert.ToInt32(Math.Ceiling((double)blogCount / pageSize));
            return pageCount;
        }
        public BlogInfo GetBlogById(long id)
        {
            return blogInfoDal.GetBlogById(id);
        }
        public bool AddBlogInfo(string title,string content)
        {
            if (string.IsNullOrEmpty(title)|| string.IsNullOrEmpty(content))
            {
                return false;
            }
            return blogInfoDal.AddBlogInfo(title,content) > 0;

        }
        public bool EditBlogInfo(long blogid, string title, string content)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || blogid==0)
            {
                return false;
            }
            return blogInfoDal.EditBlogInfo(blogid,title, content) > 0;
        }
        public bool DeletBlogInfo(long blogid)
        {
            if (blogid == 0)
            {
                return false;
            }
            return blogInfoDal.DeleteBlogInfo(blogid) > 0;
        }
       
        
    }
}

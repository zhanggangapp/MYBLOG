using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class BlogInfoXMLDal : IBlogInfoDal
    {
        public int AddBlogInfo(string title, string content)
        {
            throw new NotImplementedException();
        }

        public int DeleteBlogInfo(long blogid)
        {
            throw new NotImplementedException();
        }

        public int EditBlogInfo(long blogid, string title, string content)
        {
            throw new NotImplementedException();
        }

        public BlogInfo GetBlogById(long id)
        {
            throw new NotImplementedException();
        }

        public int GetBlogCount()
        {
            throw new NotImplementedException();
        }

        public int GetBlogListByKeyWordCount(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogListByPage(int start, int end)
        {
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogTitleListByKeyWordPage(int start, int end, string keyword)
        {
            throw new NotImplementedException();
        }

        public List<BlogInfo> GetBlogTitleListByPage(int start, int end)
        {
            throw new NotImplementedException();
        }

        public void LoadEntity(DataRow row, BlogInfo blogInfo)
        {
            throw new NotImplementedException();
        }

        public void LoadTitleEntity(DataRow row, BlogInfo blogInfo)
        {
            throw new NotImplementedException();
        }
    }
}

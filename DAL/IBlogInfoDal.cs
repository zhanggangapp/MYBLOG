using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace DAL
{
    public interface IBlogInfoDal
    {
        List<BlogInfo> GetBlogListByPage(int start, int end);
        List<BlogInfo> GetBlogTitleListByPage(int start, int end);
        List<BlogInfo> GetBlogTitleListByKeyWordPage(int start, int end, string keyword);
        int GetBlogCount();
        int GetBlogListByKeyWordCount(string keyword);
        BlogInfo GetBlogById(int id);
        int AddBlogInfo(string title, string content);
        int EditBlogInfo(int blogid, string title, string content);
        int DeleteBlogInfo(int blogid);
        void LoadTitleEntity(DataRow row, BlogInfo blogInfo);
    }
}

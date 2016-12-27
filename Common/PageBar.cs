using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PageBar
    {
        public static string GetPageBar(int pageIndex,int pageCount)
        {
            if (pageCount==1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;//起始位置,要求页面上显示10个页码
            if (start<1)
            {
                start = 1;
            }
            int end = start + 9;//终止位置
            if (end>pageCount)
            {
                end = pageCount;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <=end; i++)
            {
                if (i==pageIndex)
                {
                    sb.Append(string.Format("<li><a href='#' style='color:red;'>{0}</a></li>", i));
                }
                else
                {
                    sb.Append(string.Format("<li><a href='{0}'>{0}</a></li>", i));
                }
            }
            return sb.ToString();

        }
    }
}

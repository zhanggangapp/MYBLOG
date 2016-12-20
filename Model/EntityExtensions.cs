using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class EntityExtensions
    {
        public static RSSEntity ToDefaultRss(this IList<ArticleEntity> articleList)
        {
            
            RSSEntity rss = new RSSEntity()
            {
                Title = "张刚的rss test",
                Copyright = "Copyright 2016",
                Generator = "http://zhanggang.com.cn",
                Link = "http://zhanggang.com.cn",
                Description = "张刚的测试内容",
                WebMaster = "zhanggang",
                Image = new RSSImage()
                {
                    Link="http://zhangang/image/logo.jpg",
                    Title= "张刚的rss test",
                    Url= "http://zhanggang.com.cn",
                    Description= "张刚的测试内容img"
                }
            };
            if (articleList == null)
                return rss;
            foreach (ArticleEntity article in articleList)
            {
                rss.Items.Add(new RSSItem()
                {
                    Title = article.Title,
                    Author = article.PostUser,
                    Category = "默认分类",
                    Link = "http://www.zhang.com.cn",
                    Guid = "http://www.zgang",
                    PubDate=article.PostTime,
                    Description=article.Content
                });
            }
            return rss;
        }

        public static string ToXmlString(this RSSEntity rss)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<rss version=\"2.0\">");
            sb.AppendLine("<channel>");
            sb.AppendLine(ToXmlItem<RSSEntity>(rss));
            sb.AppendLine("</channel>");
            sb.AppendLine("</rss>");
            return sb.ToString();
        }

        public static string ToXmlString(this RSSImage image)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<image>");
            sb.Append(ToXmlItem<RSSImage>(image));
            sb.AppendLine("</image>");
            return sb.ToString();
        }
        public static string ToXmlString(this RSSItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<item>");
            sb.AppendLine(ToXmlItem<RSSItem>(item));
            sb.AppendLine("</item>");
            return sb.ToString();
        }
        private static string ToXmlItem<DType>(DType data) where DType : class
        {
            StringBuilder sb = new StringBuilder();
            Type type = data.GetType();
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo p in pis)
            {
                if (p.PropertyType == typeof(DateTime))
                {
                    sb.AppendFormat("<{0}>{1:R}</{0}>\r\n", p.Name.ToLower(), p.GetValue(data, null));
                }
                else if (p.PropertyType == typeof(RSSImage))
                {
                    sb.AppendLine(((RSSImage)p.GetValue(data, null)).ToXmlString());
                }
                else if (p.PropertyType == typeof(IList<RSSItem>))
                {
                    IList<RSSItem> rssItems = p.GetValue(data, null) as IList<RSSItem>;
                    foreach (RSSItem item in rssItems)
                    {
                        sb.AppendLine(item.ToXmlString());
                    }
                }
                else
                {
                    sb.AppendFormat("<{0}><![CDATA[{1}]]></{0}>\r\n", p.Name.ToLower(), p.GetValue(data, null));
                }
            }
            return sb.ToString();
        }

    }

   
   
}

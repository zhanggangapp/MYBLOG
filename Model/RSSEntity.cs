using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class RSSEntity
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string WebMaster { get; set; }
        public string Generator { get; set; }
        public RSSImage Image { get; set; }
        public IList<RSSItem> Items { get; set; }

        public RSSEntity()
        {
            Title = string.Empty;
            Link = string.Empty;
            Description = string.Empty;
            Language = "zh-cn";
            Copyright = string.Empty;
            WebMaster = string.Empty;
            Generator = string.Empty;
            Image = null;
            Items = new List<RSSItem>();
        }
    }

    public class RSSItem
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set;  }
        public DateTime PubDate { get; set; }
        public string Guid { get; set; }
        public string Description { get; set; }
        public RSSItem()
        {
            Link = string.Empty;
            Title = string.Empty;
            Author = string.Empty;
            Category = string.Empty;
            PubDate = DateTime.Now;
            Guid = string.Empty;
            Description = string.Empty;
        }
    }

    public class RSSImage
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public RSSImage()
        {
            Title = string.Empty;
            Url = string.Empty;
            Link = string.Empty;
            Description = string.Empty;
        }
    }
}

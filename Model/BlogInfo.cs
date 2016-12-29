using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
namespace Model
{
    [ElasticsearchType(Name ="bloginfo")]
    public class BlogInfo
    {
        public long BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

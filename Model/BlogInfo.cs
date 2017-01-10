using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
namespace Model
{
    [ElasticsearchType(IdProperty ="BlogId", Name ="bloginfo")]
    public class BlogInfo
    {
        [Number(NumberType.Long)]
        public long BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public int ClickCount { get; set; }
    }
}

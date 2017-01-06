using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BlogCommentInfo
    {
        public long BlogId { get; set; }
        public long CommentId { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

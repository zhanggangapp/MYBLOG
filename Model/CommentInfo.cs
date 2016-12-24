using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class CommentInfo
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

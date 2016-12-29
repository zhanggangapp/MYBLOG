using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
namespace Model
{
    //[ElasticsearchType(IdProperty ="Id",Name ="commentinfo")]  //这里的名字就是类型名字
    public class CommentInfo
    {
        [Number(NumberType.Long)]
        public long Id { get; set; }
        public int BlogId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

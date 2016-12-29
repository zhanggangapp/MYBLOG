using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Model
{
    [ElasticsearchType(IdProperty ="Id",Name ="userinfo")]
   public class UserInfo
    {
        [Number(NumberType.Integer)]
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

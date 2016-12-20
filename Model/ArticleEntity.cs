﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ArticleEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public string PostUser { get; set; }
        public ArticleEntity()
        {
            ID = -1;
            Title = string.Empty;
            Content = string.Empty;
            PostTime = DateTime.Now;
            PostUser = string.Empty;
        }

    }
}

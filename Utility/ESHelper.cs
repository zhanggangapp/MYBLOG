using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Utility
{
    public static class ESHelper
    {
        public static ElasticClient client;
        static ESHelper()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("myblog");
            client = new ElasticClient(settings);
           // CreateIndex("BlogInfo");
        }
        
        //通用的根据实体添加,需要根据属性值 来一一匹配添加 
        public static int AddDocument(BlogInfo blogInfo)
        {
            
            return 1;
        }

        /// <summary>
        /// 指定索引名字名字创建一个索引.
        /// </summary>
        /// <param name="indexName"></param>
        public static void CreateIndex(string indexName)
        {
            IIndexState indexState = new IndexState()
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = 1,//副本数
                    NumberOfShards = 5 //分片数
                }
            };

            client.CreateIndex(indexName, p => p.InitializeUsing(indexState).Mappings(m => m.Map<BlogInfo>(mp => mp.AutoMap())));

            //只有最后一个mapping会添加上(类型)
            //client.CreateIndex(indexName, p => p.InitializeUsing(indexState).Mappings(m => m.Map<BlogInfo>(mp => mp.AutoMap())).Mappings(m => m.Map<UserInfo>(mp => mp.AutoMap())));
            //client.CreateIndex(indexName,p=>p.InitializeUsing(indexState).Mappings(m=>m.Map<BlogInfo>(mp=>mp.AutoMap())).Mappings(m=>m.Map<UserInfo>(mp=>mp.AutoMap())).Mappings(m=>m.Map<CommentInfo>(mp=>mp.AutoMap())));

        }
    }
}

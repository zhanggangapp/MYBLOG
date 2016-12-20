using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Text;

namespace BLOG.Controllers
{
    public class RssResult:ActionResult
    {
        public System.Text.Encoding ContentEncoding { get; set; }
        public RSSEntity Data { get; set; }
        public RssResult()
        {

        }
        public RssResult(Encoding encode)
        {
            ContentEncoding = encode;
        }
        public RssResult(RSSEntity data, Encoding encode)
        {
            Data = data;
            ContentEncoding = encode;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context==null)
            {
                throw new AggregateException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            if (ContentEncoding!=null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data!=null)
            {
                response.Write(Data.ToXmlString());
            }
        }
    }
}

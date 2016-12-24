using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace Utility
{
   public static class util
    {
        public static string StrEncode(object obj)
        {
            return HttpContext.Current.Server.HtmlEncode(obj.ToString());
        }
        public static string UrlCode(object obj)
        {
            return "http://localhost/New.aspx?Id=" + obj.ToString();
        }


        //public static string TrimHtml(HttpServerUtility s, string strInput, bool allowHTML)
        //{
        //    string regStrCommon = @"((<\s*/?\s*((div)|(fieldset)|(legend)|(span)|(select)|(option)|(textarea)|(tbody)|(thead)|(span)|(table)|(tr)|(td)|(th)|(li)|(ul)|(ol)|(em)|(iframe)|(frame)|(form)|(input)|(link)|(meta)|(head)|(body)|(hr)|(h1)|(h2)|(h3)|(h4)|(h5)|(nobr)|(img)|(font)|(p)|(a)|(strong)|(object)|(embed)|(param)|(dl)|(dt)|(dd)|(script)|(o:p)|(v:shapetype)|(v:shape)|(v:stroke)|(v:formulas)|(v:imagedata)|(v:f)|(v:path)|(o:lock)|(b)|(br)|(wbr)|(param)|(color)|(backcolor)|(strike)|(u)|(i)|(size))((\s*/?>)|((\s+((\w+)\s*=\s*((""([^""\\]*(\\.[^""\\]*)*)"")|(\w+))))+\s*/?>))))";
        //    Regex rx = new Regex(@"(<!--[\w\W]*?-->)|(<\s*/?\s*((clk)|(nobr)|(center)|(founder)|(st1:[a-zA-Z0-9]+)|(marquee)|(form))((/?>)|(\s+[^<>]*?>)))" + (!allowHTML ? @"|" + regStrCommon : ""), RegexOptions.IgnoreCase);
        //    strInput = rx.Replace(strInput, "");
        //    rx = new Regex(@"((&middot;)|(&#\d{3,3};)|(&rdquo;)|&hellip;|(&ldquo;)|(&mdash;)|(&cap;)|(&omega;)|(&nbsp;)|(&lt;)|(&quot;)|(&lsquo;)|(&rsquo;)|(&gt;))" + (allowHTML ? @"|" + regStrCommon : ""), RegexOptions.IgnoreCase);
        //    string strOutput = "";
        //    int lastIndex = 0;
        //    Match m = rx.Match(strInput);
        //    while (m.Success)
        //    {
        //        strOutput += (lastIndex < m.Index ? s.HtmlEncode(strInput.Substring(lastIndex, m.Index - lastIndex)) : "") + strInput.Substring(m.Index, m.Length);
        //        lastIndex = m.Index + m.Length;
        //        m = m.NextMatch();
        //    }
        //    if (lastIndex < strInput.Length)
        //        strOutput += s.HtmlEncode(strInput.Substring(lastIndex));
        //    strOutput = strOutput.Replace("&quot;", "\"");
        //    if (allowHTML)
        //    {
        //        rx = new Regex("<object\\s*(?<attrs>[^>]*?classid\\s*=\\s*\"?clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"?[^>]*)>\\s*(?<params>(<param[^>]+>\\s*)*)</object>", RegexOptions.IgnoreCase);
        //        m = rx.Match(strOutput);
        //        if (m.Success)
        //        {
        //            string strNewOutput = strOutput.Substring(0, m.Index) + "<OBJECT " + m.Groups["attrs"].Value + ">" + m.Groups["params"].Value + "<EMBED TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/go/getflashplayer\"";
        //            rx = new Regex(@"(^|(\s+))(?<name>\w+)\s*=\s*""?(?<value>[^\s""]+)""?(?=((\s+)|>|$))", RegexOptions.IgnoreCase);
        //            Match m1 = rx.Match(m.Groups["attrs"].Value);
        //            while (m1.Success)
        //            {
        //                string nStr = m1.Groups["name"].Value.ToLower();
        //                if (nStr != "classid" && nStr != "codebase")
        //                    strNewOutput += " " + m1.Groups["name"].Value + "=\"" + m1.Groups["value"].Value + "\"";
        //                m1 = m1.NextMatch();
        //            }
        //            rx = new Regex(@"<param((\s+name\s*=\s*""?(?<name>[^\s""]+)""?)|(\s+value\s*=\s*""?(?<value>[^\s""]+)""?)){2}\s*/?>", RegexOptions.IgnoreCase);
        //            m1 = rx.Match(m.Groups["params"].Value);
        //            while (m1.Success)
        //            {
        //                string nStr = m1.Groups["name"].Value;
        //                if (nStr.ToLower() == "movie")
        //                    nStr = "SRC";
        //                strNewOutput += " " + nStr + "=\"" + m1.Groups["value"].Value + "\"";
        //                m1 = m1.NextMatch();
        //            }
        //            strNewOutput += "></EMBED></OBJECT>";
        //            strNewOutput += strOutput.Substring(m.Index + m.Length);
        //            strOutput = strNewOutput;
        //        }
        //    }
        //    return strOutput;
        //}

    }
}

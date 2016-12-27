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


        public static string TrimHtml(string strInput, bool allowHTML)
        {
            Regex rx = new Regex(@"((&middot;)|(&#\d{3,3};)|(&rdquo;)|&hellip;|(&ldquo;)|(&mdash;)|(&nbsp;)|(&lt;)|(&quot;)|(&lsquo;)|(&rsquo;)|(&gt;))" + (allowHTML ? @"|((<\s*/?\s*((div)|(fieldset)|(legend)|(span)|(select)|(option)|(textarea)|(tbody)|(thead)|(span)|(table)|(tr)|(td)|(th)|(li)|(ul)|(ol)|(em)|(iframe)|(frame)|(form)|(input)|(link)|(meta)|(head)|(body)|(hr)|(h1)|(h2)|(h3)|(h4)|(h5)|(nobr)|(img)|(font)|(p)|(a)|(strong)|(object)|(embed)|(param)|(dl)|(dt)|(dd)|(script)|(o:p)|(v:shapetype)|(v:shape)|(v:stroke)|(v:formulas)|(v:imagedata)|(v:f)|(v:path)|(o:lock)|(b)|(br)|(wbr)|(param))((/?>)|(\s+[^<>]*?>))))" : ""), RegexOptions.IgnoreCase);
            string strOutput = "";
            int lastIndex = 0;
            Match m = rx.Match(strInput);
            while (m.Success)
            {
                strOutput += (lastIndex < m.Index ? HttpUtility.HtmlEncode(strInput.Substring(lastIndex, m.Index - lastIndex)) : "") + strInput.Substring(m.Index, m.Length);
                lastIndex = m.Index + m.Length;
                m = m.NextMatch();
            }
            if (lastIndex < strInput.Length)
                strOutput += HttpUtility.HtmlEncode(strInput.Substring(lastIndex));
            strOutput = strOutput.Replace("&quot;", "\"");
            return strOutput;
        }

    }
}

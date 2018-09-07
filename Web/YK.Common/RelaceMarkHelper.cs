using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace YK.Common
{
    /// <summary>
    /// 置标置换帮助类
    /// </summary>
    /// 
    public class RelaceMarkHelper
    {      
        /// <summary>
        /// 获取指定的标签列表
        /// </summary>
        /// <param name="markName">标签名称</param>
        /// <param name="text">查找的文本</param>
        /// <returns></returns>
        public List<string> GetMarks(string markName, string text)
        {
            Regex reg = new Regex(@"(?is)<" + markName + ".*?>");
            MatchCollection match = reg.Matches(text);
            List<string> list = new List<string>();
            foreach (Match m in match)
            {
                list.Add(m.Groups[0].Value);
            }
            return list;
        }

        /// <summary>
        /// 获取列表标签列表
        /// </summary>
        /// <param name="markName">标签名称</param>
        /// <param name="text">查找的文本</param>
        /// <returns></returns>
        public List<string> GetListMarks(string markName, string text)
        {
            Regex reg = new Regex(@"<" + markName + @"\s*[^>]*>([\s\S]+?)</yk:" + markName + ">");
            MatchCollection match = reg.Matches(text);
            List<string> list = new List<string>();
            foreach (Match m in match)
            {
                list.Add(m.Groups[0].Value);
            }
            return list;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="markName">标签名称</param>
        /// <param name="text">查找的文本</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public string GetPropertyVal(string markName, string text, string propertyName)
        {
            List<string> list = GetPropertyVals(markName, text, propertyName);
            return list.Count > 0 ? list.First() : "";
        }

        /// <summary>
        /// 获取属性值列表
        /// </summary>
        /// <param name="markName">标签名称</param>
        /// <param name="text">查找的文本</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public List<string> GetPropertyVals(string markName, string text, string propertyName)
        {
            Regex reg = new Regex((@"(?is)<" + markName + @"[^>]*?$=(['""\s]?)(?<$>[^'""\s]*)\1[^>]*?>").Replace("$", propertyName));
            MatchCollection match = reg.Matches(text);
            List<string> list = new List<string>();
            foreach (Match m in match)
            {
                list.Add(m.Groups[propertyName].Value);
            }
            return list;
        }

        /// <summary>
        /// 获取标签的内容
        /// </summary>
        /// <param name="markName">标签名称</param>
        /// <param name="text">查找的文本</param>
        /// <returns></returns>
        public List<string> GetMarkInnerHtml(string markName, string text)
        {
            List<string> list = new List<string>();

            Regex reg = new Regex(@"<" + markName + @"\s*[^>]*>([\s\S]+?)</yk:" + markName + ">", RegexOptions.IgnoreCase);
            MatchCollection matchColl = reg.Matches(text);

            foreach (Match enity in matchColl)
            {
                list.Add(enity.Result("$1"));
            }

            return list;
        }


    }
}

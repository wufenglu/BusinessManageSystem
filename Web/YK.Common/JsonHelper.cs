using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YK.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 发送Get请求接口
        /// </summary>
        /// <param name="uri">uri地址</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            ser.MaxJsonLength = Int32.MaxValue;
            return ser.Serialize(obj);
        }

        /// <summary>
        /// 发送Get请求接口
        /// </summary>
        /// <param name="uri">uri地址</param>
        /// <returns></returns>
        public static T Deserialize<T>(string res)
        {
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            ser.MaxJsonLength = Int32.MaxValue;
            return ser.Deserialize<T>(res);
        }
    }
}

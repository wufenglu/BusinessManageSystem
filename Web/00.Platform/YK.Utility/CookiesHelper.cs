using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace YK.Utility
{
    /// <summary>
    /// Cookie处理类
    /// </summary>
    public class CookiesHelper
    {
        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="cookieParams">参数集合</param>
        /// <param name="Values">值列表</param>
        /// <returns></returns>
        public static void AddCookie(string cookieName, Dictionary<string,string> cookieParams, int? hours)
        {
            if (cookieParams == null) {
                return;
            }
            HttpCookie cookie = new HttpCookie(cookieName);
            int i = 0;
            foreach (var item in cookieParams)
            {
                cookie[item.Key] = item.Value;
                i++;
            }
            if (hours.HasValue)
            {
                cookie.Expires = DateTime.Now.AddHours(hours.Value);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 移除指定Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns></returns>
        public static void RemoveCookie(string CookieName)
        {
            HttpContext.Current.Response.Cookies.Remove(CookieName);
        }

        /// <summary>
        /// 移除所有Cookie
        /// </summary>
        /// <returns></returns>
        public static void RemoveCookie()
        {
            HttpContext.Current.Response.Cookies.Clear();
        }
    }
}
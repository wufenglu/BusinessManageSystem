using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace YK.Cache
{
    /// <summary>
    /// 缓存处理类
    /// </summary>
    public class CachesHelper
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        public static object GetCache(string cacheName)
        {
            return HttpContext.Current.Cache[cacheName];
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="value">缓存值</param>
        /// <param name="hours">有效期（小时）</param>
        /// <returns></returns>
        public static void AddCache(string cacheName, object value, int? hours = null)
        {
            if (hours.HasValue)
            {
                HttpContext.Current.Cache.Insert(cacheName, value, null, DateTime.Now.AddHours(hours.Value), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Low, null);
            }
            else
            {
                HttpContext.Current.Cache.Insert(cacheName, value);
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="value">缓存值</param>
        /// <param name="filePath">缓存关联文件（全路径）</param>
        /// <returns></returns>
        public static void AddCacheByFile(string cacheName, object value, string filePath)
        {
            if (File.Exists(filePath))
            {
                HttpContext.Current.Cache.Insert(cacheName, value, new System.Web.Caching.CacheDependency(filePath));
            }
        }

        /// <summary>
        /// 移除指定缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <returns></returns>
        public static void RemoveCache(string cacheName)
        {
            HttpContext.Current.Cache.Remove(cacheName);
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllCache()
        {
            System.Collections.IDictionaryEnumerator enume = HttpContext.Current.Cache.GetEnumerator();
            while (enume.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enume.Key.ToString());
            }
        }

        /// <summary>
        /// 获取所有缓存的名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllCacheNames()
        {
            List<string> names = new List<string>();
            System.Collections.IDictionaryEnumerator enume = HttpContext.Current.Cache.GetEnumerator();
            while (enume.MoveNext())
            {
                names.Add(enume.Key.ToString());
            }
            return names;
        }
    }
}
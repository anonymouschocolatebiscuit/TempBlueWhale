using System;
using System.Web;
using System.Web.Caching;

namespace BlueWhale.Common
{
    public class CacheHelper
    {
        /// <summary>
        /// Create cache file
        /// </summary>
        /// <param name="key">CacheKey</param>
        /// <param name="obj">objectTarget</param>
        public static void Insert(string key, object obj)
        {
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj);
        }
        /// <summary>
        /// Remove cache file
        /// </summary>
        /// <param name="key">CacheKey</param>
        public static void Remove(string key)
        {
            //创建缓存
            HttpContext.Current.Cache.Remove(key);
        }
        /// <summary>
        /// Create cache file dependancy
        /// </summary>
        /// <param name="key">CacheKey</param>
        /// <param name="obj">objectTarget</param>
        /// <param name="fileName">pathAbsolutePath</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //Create cache dependancy
            CacheDependency dep = new CacheDependency(fileName);
            //Create cache
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        /// <summary>
        /// Create cache expiry
        /// </summary>
        /// <param name="key">CacheKey</param>
        /// <param name="obj">objectTarget</param>
        /// <param name="expires">expire time (minute)</param>
        public static void Insert(string key, object obj, int expires)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
        }

        /// <summary>
        /// get target cache
        /// </summary>
        /// <param name="key">CacheKey</param>
        /// <returns>objectTarget</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return HttpContext.Current.Cache.Get(key);
        }

        /// <summary>
        /// get target cache
        /// </summary>
        /// <typeparam name="T">T obejct</typeparam>
        /// <param name="key">CacheKey</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }

    }
}

using System;
using System.Web;
using System.Web.Caching;

namespace BlueWhale.Common
{
    public class CacheHelper
    {
        /// <summary>
        /// Inserts an object into the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="obj">Object to cache</param>
        public static void Insert(string key, object obj)
        {
            HttpContext.Current.Cache.Insert(key, obj);
        }

        /// <summary>
        /// Removes an object from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// Inserts an object into the cache with file dependency
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="obj">Object to cache</param>
        /// <param name="fileName">Absolute path of the dependent file</param>
        public static void Insert(string key, object obj, string fileName)
        {
            CacheDependency dep = new CacheDependency(fileName);
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        /// <summary>
        /// Inserts an object into the cache with a time-based expiration
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="obj">Object to cache</param>
        /// <param name="expires">Expiration time in minutes</param>
        public static void Insert(string key, object obj, int expires)
        {
            HttpContext.Current.Cache.Insert(
                key,
                obj,
                null,
                Cache.NoAbsoluteExpiration,
                new TimeSpan(0, expires, 0)
            );
        }

        /// <summary>
        /// Retrieves an object from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Cached object or null if not found</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            return HttpContext.Current.Cache.Get(key);
        }

        /// <summary>
        /// Retrieves a typed object from the cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Cached object cast to type T, or default(T) if not found</returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default : (T)obj;
        }
    }
}

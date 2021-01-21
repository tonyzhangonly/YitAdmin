/****************************************************************
* 名称：RedisCacheImp
* 创建人：张思友
* 创建时间：2021/1/21 15:58:49
* 修改人：张思友
* 修改时间：2021/1/21 15:58:49
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yit.Cache.Cache.Interface;
using Yit.Util;

namespace Yit.Cache.Cache.Redis
{
    public class RedisCacheImp : ICache
    {
        private IDatabase cache;
        private ConnectionMultiplexer connection;
        private readonly object lockHelper = new object();
        private bool isRetrieveOverdue = false;
        /// <summary>
        /// 用于保存Redis中滑动过期的key,与时间间隔
        /// </summary>
        public static Dictionary<string, TimeSpan?> slidingKey = new Dictionary<string, TimeSpan?>();

        public RedisCacheImp()
        {
            connection = ConnectionMultiplexer.Connect(GlobalContextUtil.SystemConfig.RedisConnectionString);
            cache = connection.GetDatabase();
            if (!isRetrieveOverdue)
            {
                isRetrieveOverdue = true;
                Task.Run(() =>
                {
                    RetrieveOverdue();
                });
            }
        }
        public bool SetCache<T>(string key, T value, DateTime? expireTime = null)
        {
            try
            {
                var jsonOption = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                string strValue = JsonConvert.SerializeObject(value, jsonOption);
                return SetCache(key, strValue, expireTime);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return false;
        }

        public bool SetCache(string key, string strValue, DateTime? expireTime = null)
        {
            try
            {
                if (string.IsNullOrEmpty(strValue))
                {
                    return false;
                }
                if (expireTime == null)
                {
                    return cache.StringSet(key, strValue);
                }
                else
                {
                    return cache.StringSet(key, strValue, (expireTime.Value - DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return false;
        }
        public bool RemoveCache(string key)
        {
            return cache.KeyDelete(key);
        }

        public T GetCache<T>(string key)
        {
            var t = default(T);
            try
            {
                SlidingIsOverdue(key);
                var value = cache.StringGet(key);
                if (string.IsNullOrEmpty(value))
                {
                    return t;
                }
                t = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return t;
        }
        public object GetCache(string key)
        {
            SlidingIsOverdue(key);
            var value = cache.StringGet(key);
            return value;
        }

        #region Hash
        public int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue)
        {
            return SetHashFieldCache<T>(key, new Dictionary<string, T> { { fieldKey, fieldValue } });
        }

        public int SetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            int count = 0;
            var jsonOption = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            foreach (string fieldKey in dict.Keys)
            {
                string fieldValue = JsonConvert.SerializeObject(dict[fieldKey], jsonOption);
                count += cache.HashSet(key, fieldKey, fieldValue) ? 1 : 0;
            }
            return count;
        }

        public T GetHashFieldCache<T>(string key, string fieldKey) where T : class, new()
        {
            var dict = GetHashFieldCache<T>(key, new Dictionary<string, T> { { fieldKey, default(T) } });
            return dict[fieldKey];
        }

        public Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict) where T : class, new()
        {
            foreach (string fieldKey in dict.Keys)
            {
                string fieldValue = cache.HashGet(key, fieldKey);
                dict[fieldKey] = JsonConvert.DeserializeObject<T>(fieldValue);
            }
            return dict;
        }

        public Dictionary<string, T> GetHashCache<T>(string key)
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();
            var hashFields = cache.HashGetAll(key);
            foreach (HashEntry field in hashFields)
            {
                dict[field.Name] = JsonConvert.DeserializeObject<T>(field.Value);
            }
            return dict;
        }

        public List<T> GetHashToListCache<T>(string key)
        {
            List<T> list = new List<T>();
            var hashFields = cache.HashGetAll(key);
            foreach (HashEntry field in hashFields)
            {
                list.Add(JsonConvert.DeserializeObject<T>(field.Value));
            }
            return list;
        }

        public bool RemoveHashFieldCache(string key, string fieldKey)
        {
            Dictionary<string, bool> dict = new Dictionary<string, bool> { { fieldKey, false } };
            dict = RemoveHashFieldCache(key, dict);
            return dict[fieldKey];
        }

        public Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict)
        {
            foreach (string fieldKey in dict.Keys)
            {
                dict[fieldKey] = cache.HashDelete(key, fieldKey);
            }
            return dict;
        }
        #endregion

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
            }
            GC.SuppressFinalize(this);
        }

        public string GetHashFieldCache(string key, string fieldKey)
        {
            var dict = GetHashFieldCache(key, new Dictionary<string, string> { { fieldKey, "" } });
            return dict[fieldKey];
        }

        public Dictionary<string, string> GetHashFieldCache(string key, Dictionary<string, string> dict)
        {
            foreach (string fieldKey in dict.Keys)
            {
                string fieldValue = cache.HashGet(key, fieldKey);
                dict[fieldKey] = fieldValue;
            }
            return dict;
        }

        public bool SetCacheSliding<T>(string key, T value, TimeSpan? expireTime = null)
        {
            lock (lockHelper)
            {
                slidingKey.Add(key, expireTime);
            }

            return SetCache(key, value, DateTime.Now + expireTime);
        }

        public bool SetCacheSliding(string key, string value, TimeSpan? expireTime = null)
        {
            lock (lockHelper)
            {
                slidingKey.Add(key, expireTime);
            }
            return SetCache(key, value, DateTime.Now + expireTime);
        }

        public bool IsKeyExists(string key)
        {
            return cache.KeyExists(key);
        }
        /// <summary>
        /// 滑动是否过期,没过期就更换过期时间
        /// </summary>
        /// <returns></returns>
        private void SlidingIsOverdue(string key)
        {
            try
            {
                var keyExists = IsKeyExists(key);
                if (keyExists)
                {
                    TimeSpan? timespan = null;//
                    bool isSliding = false;
                    lock (lockHelper)
                    {
                        if (slidingKey.ContainsKey(key))
                        {
                            timespan = slidingKey[key];
                            isSliding = true;
                        }
                    }
                    if (isSliding)
                    {
                        KeyExpire(key, timespan);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Info("相对过期写入错误", ex);
            }

        }
        /// <summary>
        /// 写入过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        private bool KeyExpire(string key, TimeSpan? expireTime = null)
        {
            return cache.KeyExpire(key, expireTime);
        }
        private async Task RetrieveOverdue()
        {
            while (true)
            {
                int lengthKey = slidingKey.Count;
                Dictionary<string, TimeSpan?> dis = JsonConvert.DeserializeObject<Dictionary<string, TimeSpan?>>(JsonConvert.SerializeObject(slidingKey));
                foreach (var item in dis.Keys)
                {
                    if (!IsKeyExists(item))
                    {
                        dis.Remove(item);
                    }
                }
                lock (lockHelper)
                {
                    for (int i = lengthKey; i < slidingKey.Count; i++)
                    {
                        dis.Add(slidingKey.ToList()[i].Key, slidingKey.ToList()[i].Value);
                    }
                    slidingKey = dis;
                }
                await Task.Delay(1000 * 60 * 10);
            }
        }
    }
}

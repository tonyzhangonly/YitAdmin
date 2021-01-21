/****************************************************************
* 名称：ICache抽象类
* 创建人：张思友
* 创建时间：2021/1/21 15:45:51
* 修改人：张思友
* 修改时间：2021/1/21 15:45:51
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Yit.Cache.Cache.Interface
{
    public interface ICache
    {
        /// <summary>
        /// 写入缓存信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        bool SetCache<T>(string key, T value, DateTime? expireTime = null);
        /// <summary>
        /// 写入缓存信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        bool SetCache(string key, string value, DateTime? expireTime = null);
        /// <summary>
        /// 检测是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsKeyExists(string key);
        /// <summary>
        /// 写入滑动过期缓存,Hash未操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        bool SetCacheSliding<T>(string key, T value, TimeSpan? expireTime=null);

        bool SetCacheSliding(string key, string value, TimeSpan? expireTime = null);
        /// <summary>
        /// 获取缓存信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key);
        /// <summary>
        /// 获取缓存信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetCache(string key);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveCache(string key);

        #region Hash
        /// <summary>
        /// 数据写入Hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fieldKey"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue);
        /// <summary>
        /// 数据写入Hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        int SetHashFieldCache<T>(string key, Dictionary<string, T> dict);
        /// <summary>
        /// 获取Hash信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fieldKey"></param>
        /// <returns></returns>
        T GetHashFieldCache<T>(string key, string fieldKey) where T : class, new();
        string GetHashFieldCache(string key, string fieldKey);
        /// <summary>
        /// 获取hash表中数据Dictionary集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict) where T:class,new();
        Dictionary<string, string> GetHashFieldCache(string key, Dictionary<string, string> dict);
        Dictionary<string, T> GetHashCache<T>(string key);
        List<T> GetHashToListCache<T>(string key);
        bool RemoveHashFieldCache(string key, string fieldKey);
        Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict);
        #endregion
    }
}

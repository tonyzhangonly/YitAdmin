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
        bool SetCache<T>(string key, T value, DateTime? expireTime = null);
        T GetCache<T>(string key);
        bool RemoveCache(string key);

        #region Hash
        int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue);
        int SetHashFieldCache<T>(string key, Dictionary<string, T> dict);
        T GetHashFieldCache<T>(string key, string fieldKey);
        Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict);
        Dictionary<string, T> GetHashCache<T>(string key);
        List<T> GetHashToListCache<T>(string key);
        bool RemoveHashFieldCache(string key, string fieldKey);
        Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict);
        #endregion
    }
}

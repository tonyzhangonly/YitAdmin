/****************************************************************
* 名称：MemoryCacheImp
* 创建人：张思友
* 创建时间：2021/1/21 15:53:41
* 修改人：张思友
* 修改时间：2021/1/21 15:53:41
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yit.Cache.Cache.Interface;
using Microsoft.Extensions.DependencyInjection;
using Yit.Util;

namespace Yit.Cache.Cache.Memory
{
    public class MemoryCacheImp:ICache
    {
        private IMemoryCache cache = GlobalContextUtil.ServiceProvider.GetService<IMemoryCache>();

        public T GetCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, T> GetHashCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T GetHashFieldCache<T>(string key, string fieldKey)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            throw new NotImplementedException();
        }

        public List<T> GetHashToListCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCache(string key)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHashFieldCache(string key, string fieldKey)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict)
        {
            throw new NotImplementedException();
        }

        public bool SetCache<T>(string key, T value, DateTime? expireTime = null)
        {
            throw new NotImplementedException();
        }

        public int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue)
        {
            throw new NotImplementedException();
        }

        public int SetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            throw new NotImplementedException();
        }
    }
}

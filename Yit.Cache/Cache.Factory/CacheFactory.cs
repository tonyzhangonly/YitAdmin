using System;
using Yit.Cache.Cache.Interface;
using Yit.Cache.Cache.Memory;
using Yit.Cache.Cache.Redis;
using Yit.Util;

namespace Yit.Cache
{
    public class CacheFactory
    {
        private static ICache cache = null;
        private static readonly object lockHelper = new object();
        public CacheFactory()
        {
            if (cache == null)
            {
                lock (lockHelper)
                {
                    if (cache == null)
                    {
                        switch (GlobalContextUtil.SystemConfig.CacheProvider)
                        {
                            case "Redis": cache = new RedisCacheImp(); break;

                            default:
                            case "Memory": cache = new MemoryCacheImp(); break;
                        }
                    }
                }
            }
        }
    }
}

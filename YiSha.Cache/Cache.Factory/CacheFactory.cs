using System;
using YiSha.Cache.Cache.Interface;
using YiSha.Cache.Cache.Memory;
using YiSha.Cache.Cache.Redis;
using Yit.Util;

namespace YiSha.Cache
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

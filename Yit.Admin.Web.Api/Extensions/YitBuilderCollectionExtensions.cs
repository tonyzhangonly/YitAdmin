using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yit.Admin.Web.Api.CoreBuilder;

namespace Yit.Admin.Web.Api.Extensions
{
    public static class YitBuilderCollectionExtensions
    {
        /// <summary>
        /// 添加中间件
        /// </summary>
        public static void AddYitServiceProvider(this IServiceCollection services, IConfiguration configuration)
        {
            ICoreServiceBuilder serviceBuilder = new YitCoreServiceBuilder(services, configuration);
            serviceBuilder.AddMvcExtensions();
            serviceBuilder.AddCache();
            serviceBuilder.AddLog();
            serviceBuilder.AddAutoMapper();
            serviceBuilder.AddCors();
            serviceBuilder.AddSwaggerGenerator();
            serviceBuilder.AddJwtAuth();
            serviceBuilder.AddHttpContext();
        }
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddYitConfigureProvider(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}

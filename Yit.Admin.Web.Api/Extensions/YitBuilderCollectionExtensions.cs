using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            serviceBuilder.SetConfige();
            serviceBuilder.AddMvcExtensions();
            serviceBuilder.AddCache();
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
        public static void AddYitConfigureProvider(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            ICoreConfigurationBuilder configurationBuilder = new YitCoreConfigureBuilder(app, env);
            configurationBuilder.UseRazorEngine();
            configurationBuilder.UseSwagger();
            configurationBuilder.UseAuth();
            configurationBuilder.UseCors();
            configurationBuilder.UseOther();
            configurationBuilder.AddSignalR();
        }
    }
}

using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yit.Admin.Web.Api.MappingLayer;
using Yit.Util;

namespace Yit.Admin.Web.Api.CoreBuilder
{
    public class YitCoreServiceBuilder : ICoreServiceBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        public YitCoreServiceBuilder(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }
        public void AddAutoMapper()
        {
            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                //cfg.ReplaceMemberName("NU_", "");
                //cfg.ReplaceMemberName("_", "");//替换
                //cfg.RecognizePrefixes("NU");///忽略前缀
                cfg.SourceMemberNamingConvention = new PascalCaseNamingConvention();
                cfg.DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.AddProfile<AutoCustomProfile>();//上面的要放这个之前
            });
            _services.AddSingleton(config);
            _services.AddScoped<IMapper, Mapper>();
        }

        /// <summary>
        /// 开启MemoryCache缓存
        /// </summary>
        public void AddCache()
        {
            _services.AddMemoryCache();
        }

        public void AddCors()
        {
            _services.AddCors(options =>
            {
                options.AddPolicy("myAllowSpecificOrigins", corsbuilder =>
                {
                    ///跨域信息与IP填写无关，与浏览器地址相同。浏览器URl是什么填写就应该是什么
                    var corsPath = _configuration.GetSection("CorsPaths").GetChildren().Select(p => p.Value).ToArray();
                    corsbuilder.WithOrigins(corsPath)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
        }

        public void AddHttpContext()
        {
            throw new NotImplementedException();
        }

        public IServiceProvider AddIocContainer()
        {
            throw new NotImplementedException();
        }

        public void AddJwtAuth()
        {
            throw new NotImplementedException();
        }

        public void AddMvcExtensions()
        {
            throw new NotImplementedException();
        }

        public void AddSwaggerGenerator()
        {
            throw new NotImplementedException();
        }
    }
}

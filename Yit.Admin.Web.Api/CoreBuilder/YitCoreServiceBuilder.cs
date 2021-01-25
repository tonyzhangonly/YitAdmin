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
using Yit.Admin.Web.Api.Filter;
using Yit.Admin.Web.Api.MappingLayer;
using Yit.Util;
using Yit.Util.Model;

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
        public void UseErrorHanle()
        {
            _services.AddMvc(o => o.Filters.Add(typeof(GlobalExceptions)));
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
            _services.AddControllers()
            //原生Json服务中文、变量、注释支持差，这里切换为Newtonsoft提供的Json服务，必须引用Microsoft.AspNetCore.Mvc.NewtonsoftJson，引用Newtonsoft.Json此处无效
            .AddNewtonsoftJson(options =>
            {
              //保持Json属性/变量大小写
              options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
              // 忽略循环引用
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
              // 设置时间格式
              options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
              // 如字段为null值，该字段不会返回到前端
              // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; 
          });
        }

        public void AddSwaggerGenerator()
        {
            throw new NotImplementedException();
        }

        public void SetConfige()
        {
            GlobalContextUtil.SystemConfig = _configuration.GetSection("SystemConfig").Get<SystemConfig>();
        }

        public void AddSignalR()
        {
            _services.AddSignalR(options =>
            {
                //客户端发保持连接请求到服务端最长间隔，默认30秒，改成4分钟，网页需跟着设置connection.keepAliveIntervalInMilliseconds = 12e4;即2分钟
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
                //服务端发保持连接请求到客户端间隔，默认15秒，改成2分钟，网页需跟着设置connection.serverTimeoutInMilliseconds = 24e4;即4分钟
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });
        }
    }
}

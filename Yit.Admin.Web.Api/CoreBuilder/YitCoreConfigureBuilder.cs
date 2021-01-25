using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yit.SignalRChat.Hubs;
using Yit.Util;

namespace Yit.Admin.Web.Api.CoreBuilder
{
    public class YitCoreConfigureBuilder : ICoreConfigurationBuilder
    {
        private readonly IApplicationBuilder _app;
        private readonly IWebHostEnvironment _env;
        public YitCoreConfigureBuilder(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _app = app;
            _env = env;
        }

        public void AddSignalR()
        {
            _app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ServerHub>("/serverHub");
            });
        }

        public void UseAuth()
        {
            
        }

        public void UseCors()
        {
            _app.UseCors("myAllowSpecificOrigins");
            _app.UseAuthorization();
            _app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("myAllowSpecificOrigins");
                endpoints.MapControllers();
            });
        }
        /// <summary>
        /// 
        /// </summary>
        public void UseOther()
        {
            GlobalContextUtil.ServiceProvider = _app.ApplicationServices;
        }

        public void UseRazorEngine()
        {
            if (_env.IsDevelopment())
            {
                _app.UseDeveloperExceptionPage();
            }
        }

        public void UseSwagger()
        {
            
        }
    }
}

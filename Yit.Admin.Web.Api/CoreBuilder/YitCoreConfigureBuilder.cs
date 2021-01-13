using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void UseAuth()
        {
            throw new NotImplementedException();
        }

        public void UseCors()
        {
            _app.UseCors("myAllowSpecificOrigins");
        }

        public void UseErrorHanle()
        {
            throw new NotImplementedException();
        }

        public void UseOther()
        {
            throw new NotImplementedException();
        }

        public void UseRazorEngine()
        {
            throw new NotImplementedException();
        }

        public void UseSwagger()
        {
            throw new NotImplementedException();
        }
    }
}

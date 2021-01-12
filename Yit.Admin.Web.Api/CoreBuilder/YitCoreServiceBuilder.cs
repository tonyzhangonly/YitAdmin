using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void AddCache()
        {
            throw new NotImplementedException();
        }

        public void AddCors()
        {
            throw new NotImplementedException();
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

        public void AddLog()
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

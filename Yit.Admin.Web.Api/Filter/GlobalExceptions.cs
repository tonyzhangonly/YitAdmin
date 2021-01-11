using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yit.Entity;
using Yit.Util;
using Yit.Util.Extension;

namespace Yit.Admin.Web.Api.Filter
{
    /// <summary>
    /// 全局错误检测
    /// </summary>
    public class GlobalExceptions: ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _env;
        public GlobalExceptions(IWebHostEnvironment env)
        {
            _env = env;
        }
        public override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            LogHelper log = new LogHelper();
            LogMessage logMessage = new LogMessage()
            {
                Url = context.HttpContext.Request.Path,
                Host = context.HttpContext.Request.Host.ToString(),
                Browser = context.HttpContext.Request.Protocol,
                Parameter = context.HttpContext.Request.GetUrlParameter()
            };
            if (_env.IsDevelopment())//开发者模式显示具体错误信息
            {
                context.Result = new BadRequestObjectResult(new ExecResult<string>() { Flag = false, Message = context.Exception.Message });//返回异常数据
            }
            else
            {
                context.Result = new BadRequestObjectResult(new ExecResult<string>() { Flag = false, Message = "系统内部错误" });//返回异常数据
            }
            log.Error(context.Exception, logMessage);
            context.ExceptionHandled = true;
        }
    }
}

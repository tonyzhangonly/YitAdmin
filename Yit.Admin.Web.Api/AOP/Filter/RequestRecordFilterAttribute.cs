using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yit.Admin.Web.Api.AOP.Filter
{
    /// <summary>
    /// 筛选器，用于控制tokenid验证，以及页面的缓存记录
    /// </summary>
    public class RequestRecordFilterAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 用于验证token信息，以及缓存的读取
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        /// <summary>
        /// 异步缓存保存
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}

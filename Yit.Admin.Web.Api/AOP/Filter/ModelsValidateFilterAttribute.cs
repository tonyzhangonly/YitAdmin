using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yit.Entity.CommonModels;

namespace Yit.Admin.Web.Api.AOP.Filter
{
    /// <summary>
    /// 用于数据模型验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,AllowMultiple = true,Inherited = true)]
    public class ModelsValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)//是否通过数据验证
            {
                ExecResult<object> result = new ExecResult<object>() { Flag = false };
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        result.Message += error.ErrorMessage + "|";
                    }
                }
                if (result.Message.Length > 0)
                    result.Message = result.Message.Substring(0, result.Message.Length - 1);
                context.Result = new JsonResult(result);
                return;
            }
        }
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    if (!context.ModelState.IsValid)//是否通过数据验证
        //    {
        //        ExecResult<object> result = new ExecResult<object>() { Flag = false };
        //        foreach (var item in context.ModelState.Values)
        //        {
        //            foreach (var error in item.Errors)
        //            {
        //                result.Message += error.ErrorMessage + "|";
        //            }
        //        }
        //        if (result.Message.Length > 0)
        //            result.Message = result.Message.Substring(0, result.Message.Length - 1);
        //        context.Result = new JsonResult(result);
        //    }
        //}

    }
}

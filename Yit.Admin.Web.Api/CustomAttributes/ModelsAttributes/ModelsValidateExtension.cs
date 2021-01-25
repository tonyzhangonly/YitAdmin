using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yit.Admin.Web.Api.CustomAttributes.ModelsAttributes
{
    /// <summary>
    /// 条件正则判断
    /// </summary>
    public class CompareConditionsAttribute : ValidationAttribute
    {
        /// <summary>
        /// 比较值属性名称
        /// </summary>
        public string OtherProperty { get; set; }
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Regx { get; set; }
        /// <summary>
        /// 判断条件值
        /// OtherProperty值等于NewValue 才验证正则表达式
        /// </summary>
        public object NewValue { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == (PropertyInfo)null)
            {
                return null;
            }
            object value2 = property.GetValue(validationContext.ObjectInstance, null);//获取对应属性值
            if (object.Equals(NewValue, value2))//属性值等于当前值
            {
                Regex regEx = new Regex(Regx);
                bool ismatch = regEx.IsMatch(value.ToString());
                if (!ismatch)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return null;
        }
    }
    /// <summary>
    /// 条件非空验证,可多值判断 NewValue用,分割
    /// </summary>
    public class NoNullConditionsAttribute : ValidationAttribute
    {
        /// <summary>
        /// 比较值属性名称
        /// </summary>
        public string OtherProperty { get; set; }
        /// <summary>
        /// 判断条件值
        /// OtherProperty值等于NewValue才验证非空，数据需要为基本类型
        /// </summary>
        public string NewValue { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == (PropertyInfo)null)
            {
                return null;
            }
            object value2 = property.GetValue(validationContext.ObjectInstance, null);//获取对应属性值
            if (value2 == null)
            {
                return null;
            }
            if (NewValue.Split(',').Contains(value2.ToString()))//属性值等于当前值 且当前值为null
            {
                if (string.IsNullOrEmpty(value?.ToString()))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return null;
        }
    }
    /// <summary>
    /// 判断是否为重复用户
    /// 未找见模型验证前的 验证处理 模型验证每次进入都会处理   不合适
    /// </summary>
    public class CustomRemoteAttribute : Microsoft.AspNetCore.Mvc.RemoteAttribute
    {
        //    /// <summary>
        //    /// 获取继承IValidationAction的所有类
        //    /// </summary>
        //    /// <returns></returns>
        //    private static IEnumerable<Type> GetControllerNames()
        //    {
        //        var controllerNames = new List<Type>();
        //        GetSubClasses<IValidationAction>().ForEach(controllerNames.Add);
        //        return controllerNames;
        //    }
        //    /// <summary>
        //    /// 匹配继承T的所有类
        //    /// </summary>
        //    /// <typeparam name="T">接口</typeparam>
        //    /// <returns></returns>
        //    private static List<Type> GetSubClasses<T>()
        //    {
        //        Type[] types = Assembly.GetCallingAssembly().GetTypes();
        //        return types.Where(t => t.GetInterfaces().Contains(typeof(T))).ToList();
        //    }
        //    /// <summary>
        //    /// 远程验证调用
        //    /// </summary>
        //    /// <param name="action">方法名称</param>
        //    /// <param name="controller">类名</param>
        //    public CustomRemoteAttribute(string action, string controller)
        //        : base(action, controller)
        //    {
        //        Action = action;
        //        Controller = controller;
        //    }
        //    public string Action { get; set; }
        //    public string Controller { get; set; }
        //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    {
        //        var fields = AdditionalFields.Split(',');//拆分字段列 用,分割
        //        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
        //        foreach (var item in fields)
        //        {
        //            var property = validationContext.ObjectType.GetProperty(item);
        //            if (property == null)
        //            {
        //                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "{0} 不存在", item));
        //            }
        //            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
        //            keyValuePairs.Add(property.Name, otherValue);
        //        }
        //        #region 反射调用匹配的方法
        //        var allControllers = GetControllerNames();
        //        var controllerType = allControllers.FirstOrDefault(x => x.Name == Controller);
        //        if (controllerType != null)
        //        {
        //            var methodInfo = controllerType.GetMethod(Action);
        //            object result = null;
        //            if (methodInfo != null)
        //            {
        //                var parameters = methodInfo.GetParameters();
        //                var controller = Activator.CreateInstance(controllerType);//实例化方法，实例对象为无参的对象
        //                if (parameters.Length == 0)//调用方法
        //                {
        //                    result = methodInfo.Invoke(controller, null);
        //                }
        //                else
        //                {
        //                    object[] parametersArray = new object[keyValuePairs.Keys.Count];
        //                    int i = 0;
        //                    foreach (var item in keyValuePairs)
        //                    {
        //                        parametersArray[i] = item.Value;
        //                        ++i;
        //                    }

        //                    result = methodInfo.Invoke(controller, parametersArray);
        //                }
        //            }
        //            bool isValid = result is bool;
        //            if (isValid)
        //            {
        //                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        //            }
        //        }
        //        #endregion
        //        return null;
        //    }
    }
}

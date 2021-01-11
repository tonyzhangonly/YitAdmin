/****************************************************************
* 名称：LogFormat
* 创建人：张思友
* 创建时间：2021/1/11 18:23:31
* 修改人：张思友
* 修改时间：2021/1/11 18:23:31
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yit.Entity;

namespace Yit.Util
{
    public class LogFormat
    {
        /// <summary>
        /// 生成错误
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string ErrorFormat(LogMessage logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 错误: >> 操作时间: " + logMessage.OperationTime + "   操作人: " + logMessage.UserName + " \r\n");
            strInfo.Append("2. 地址: " + logMessage.Url + "    \r\n");
            strInfo.Append("3. 类名: " + logMessage.Class + " \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成警告
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string WarnFormat(LogMessage logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 警告: >> 操作时间: " + logMessage.OperationTime + "   操作人: " + logMessage.UserName + " \r\n");
            strInfo.Append("2. 地址: " + logMessage.Url + "    \r\n");
            strInfo.Append("3. 类名: " + logMessage.Class + " \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成信息
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string InfoFormat(LogMessage logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 信息: >> 操作时间: " + logMessage.OperationTime + "   操作人: " + logMessage.UserName + " \r\n");
            strInfo.Append("2. 地址: " + logMessage.Url + "    \r\n");
            strInfo.Append("3. 类名: " + logMessage.Class + " \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成调试
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string DebugFormat(LogMessage logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 调试: >> 操作时间: " + logMessage.OperationTime + "   操作人: " + logMessage.UserName + " \r\n");
            strInfo.Append("2. 地址: " + logMessage.Url + "    \r\n");
            strInfo.Append("3. 类名: " + logMessage.Class + " \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成异常信息
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string ExceptionFormat(LogMessage logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 异常: >> 操作时间: " + logMessage.OperationTime + "   操作人: " + logMessage.UserName + " \r\n");
            strInfo.Append("2. 地址: " + logMessage.Url + "    \r\n");
            strInfo.Append("3. 类名: " + logMessage.Class + " \r\n");
            strInfo.Append("4. 主机: " + logMessage.Host + "   Ip  : " + logMessage.Ip + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 请求参数: " + logMessage.Parameter + "    \r\n");
            strInfo.Append("6. 异常: " + logMessage.ExceptionInfo + "\r\n");
            strInfo.Append("7. 来源: " + logMessage.ExceptionSource + "\r\n");
            strInfo.Append("8. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("9. StackTrace: " + logMessage.ExceptionRemark + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
    }
}

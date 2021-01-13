using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yit.Entity;
using Yit.Util.Extension;

namespace Yit.Util
{
    public class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger("NETCoreRepository", "logerror");
        private LogFormat mLogFormat = new LogFormat();
        /// <summary>
        /// 调试错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                log.Debug(msg.ParseToString());
            }
            else
            {
                log.Debug(msg + GetExceptionMessage(ex));
            }
        }
        /// <summary>
        /// 信息写入
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                log.Info(msg.ParseToString());
            }
            else
            {
                log.Info(msg + GetExceptionMessage(ex));
            }
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Warn(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                log.Warn(msg.ParseToString());
            }
            else
            {
                log.Warn(msg + GetExceptionMessage(ex));
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                log.Error(msg.ParseToString());
            }
            else
            {
                log.Error(msg + GetExceptionMessage(ex));
            }
        }
        public void Error(Exception ex, LogMessage msg)
        {
            msg.ExceptionSource = ex.Source;
            msg.Content = ex.InnerException == null ? ex.Message : ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message;
            msg.ExceptionInfo = ex.Message;
            msg.ExceptionRemark = ex.StackTrace;
            msg.OperationTime = DateTime.Now;
            msg.Class = ex.GetType().Name;
            string errMsg = mLogFormat.ExceptionFormat(msg);
            log.Error(errMsg);
        }

        public static void Error(Exception ex)
        {
            if (ex != null)
            {
                log.Error(GetExceptionMessage(ex));
            }
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                log.Fatal(msg.ParseToString());
            }
            else
            {
                log.Fatal(msg + GetExceptionMessage(ex));
            }
        }

        public static void Fatal(Exception ex)
        {
            if (ex != null)
            {
                log.Fatal(GetExceptionMessage(ex));
            }
        }
        public static void Warn(Exception ex)
        {
            log.Warn(ex);
        }
        /// <summary>
        /// 错误信息规格化
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string GetExceptionMessage(Exception ex)
        {
            StringBuilder message = new StringBuilder();
            if (ex != null)
            {
                message.AppendLine(ex.Message);
                Exception originalException = ex.GetOriginalException();
                if (originalException != null)
                {
                    if (originalException.Message != ex.Message)
                    {
                        message.AppendLine(originalException.Message);
                    }
                }
                message.AppendLine(ex.StackTrace);
            }
            return message.ToString();
        }
    }
}

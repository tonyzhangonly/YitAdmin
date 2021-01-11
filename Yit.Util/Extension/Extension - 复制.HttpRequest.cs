/****************************************************************
* 名称：Extentsion
* 创建人：张思友
* 创建时间：2021/1/11 18:16:34
* 修改人：张思友
* 修改时间：2021/1/11 18:16:34
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Yit.Util.Extension
{
	public static partial class Extensions
	{
        /// <summary>
        /// 判断是否为Ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
        public static string GetUrlParameter(this HttpRequest request)
        {
            string parameter = string.Empty;
            try
            {
                if (request.Method == "GET")
                {
                    parameter = request.QueryString.ToString();
                }
                else
                {
                    Stream stream = request.Body;
                    byte[] buffer = new byte[request.ContentLength.Value];
                    stream.ReadAsync(buffer, 0, buffer.Length);
                    string querystring = Encoding.UTF8.GetString(buffer);
                    return "";

                    Encoding encoding = Encoding.UTF8;
                    using (StreamReader reader = new StreamReader(request.Body, encoding))
                    {
                        parameter = reader.ReadToEndAsync().ToString();
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return parameter;
        }
    }
}

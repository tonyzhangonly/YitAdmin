﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    return querystring;
                    //Encoding encoding = Encoding.UTF8;
                    //using (StreamReader reader = new StreamReader(request.Body, encoding))
                    //{
                    //    parameter = reader.ReadToEndAsync().ToString();
                    //}

                }
            }
            catch (Exception ex)
            {

            }
            return parameter;
        }
    }
}

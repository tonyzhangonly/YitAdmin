/****************************************************************
* 名称：ExecResult
* 创建人：张思友
* 创建时间：2021/1/11 18:12:40
* 修改人：张思友
* 修改时间：2021/1/11 18:12:40
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Yit.Entity
{
    public class ExecResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Flag { get; set; } = true;
        /// <summary>
        /// 回调信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回实体
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 数据量
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 页面返回状态码
        /// </summary>
        public int StatusCode { get; set; }
    }
}

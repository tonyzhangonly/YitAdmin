/****************************************************************
* 名称：IServerHub抽象类
* 创建人：张思友
* 创建时间：2021/1/25 16:07:28
* 修改人：张思友
* 修改时间：2021/1/25 16:07:28
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yit.SignalRChat.Hubs
{
    public interface IServerHub
    {
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessage(string user, string message);
        /// <summary>
        /// 分组发送信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task GroupSendMessage(string groupId, string message);
        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RegisterUserMessage(string key);
        /// <summary>
        /// 指定用户发送信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendByUserKeyMessage(string key, string message);
        /// <summary>
        /// 所有人发送信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendByAllMessage(string message);
    }
}

/****************************************************************
* 名称：ChatHub
* 创建人：张思友
* 创建时间：2021/1/25 15:07:55
* 修改人：张思友
* 修改时间：2021/1/25 15:07:55
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Yit.SignalRChat.Hubs
{
    public class ServerHub : Hub, IServerHub
    {
        internal static List<UserModel> signalrUser = new List<UserModel>();
        private readonly string lockHelper = string.Empty;
        private readonly IServiceProvider _serviceProvider;
        public ServerHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);///接受信息的方法是ReceiveMessage
        }
        /// <summary>
        /// 分组发送消息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task GroupSendMessage(string groupId, string message)
        {
            UserModel userModel = new UserModel();//通过key查询到用户信息
            string connecId = Context.ConnectionId;
            await Groups.AddToGroupAsync(connecId, groupId);
            await Clients.Group(userModel.GroupName).SendAsync("GetUsersResponse", message);
        }
        public async Task RegisterUserMessage(string key)
        {
            string connecId = Context.ConnectionId;
            UserModel userModel = new UserModel();//通过key查询到用户信息
            bool isAdd = signalrUser.Where(a => a.SignalRKey == connecId).ToList().Count == 0;
            UserModel oldUser = new UserModel();
            lock (lockHelper)
            {
                ///没有用户就添加用户，否则修改用户中绑定值
                if (isAdd)
                {
                    userModel.SignalRKey = connecId;
                    signalrUser.Add(userModel);
                }
                else
                {
                    oldUser = signalrUser.Where(a => a.SignalRKey == connecId).FirstOrDefault();
                    oldUser.LoginName = userModel.LoginName;
                }
            }
            if (isAdd)
            {
                await Groups.AddToGroupAsync(connecId, userModel.GroupName);
            }
            else
            {

                await Groups.RemoveFromGroupAsync(connecId, oldUser.GroupName);
                await Groups.AddToGroupAsync(connecId, userModel.GroupName);
            }
        }
        /// <summary>
        /// Signalr连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// Signalr断开连接
        /// 当连接断开时执行，关闭、刷新 浏览器或标签页都会执行
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connecId = Context.ConnectionId;
            lock (lockHelper)
            {
                if (signalrUser.Where(a => a.SignalRKey == connecId).ToList().Count > 0)
                {
                    UserModel removeUser = signalrUser.Where(a => a.SignalRKey == connecId).FirstOrDefault();
                    signalrUser.Remove(removeUser);
                    Groups.RemoveFromGroupAsync(connecId, removeUser.GroupName);
                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendByUserKeyMessage(string key, string message)
        {
            UserModel userModel = new UserModel();//通过key查询到用户信息
            await Clients.Client(userModel.SignalRKey).SendAsync("", message);
        }

        public async Task SendByAllMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveAllMessage", message);///接受信息的方法是ReceiveMessage
        }
    }
}

/****************************************************************
* 名称：UserModel
* 创建人：张思友
* 创建时间：2021/1/25 15:34:45
* 修改人：张思友
* 修改时间：2021/1/25 15:34:45
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yit.SignalRChat
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public string LoginName { get; set; }
        public string SignalRKey { get; set; }
        public string GroupName { get; set; }
    }
}

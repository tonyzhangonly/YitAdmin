/****************************************************************
* 名称：SysUserDto
* 创建人：张思友
* 创建时间：2021/1/21 14:55:50
* 修改人：张思友
* 修改时间：2021/1/21 14:55:50
* CLR版本：V1.0.0.0
* 描述说明：
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Yit.Entity.DtoModels
{
    public partial class SysUserDto
    {
        public Guid SysUserId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public int? UserType { get; set; }
        public int? UserStatus { get; set; }
        public string Telphone { get; set; }
    }
}

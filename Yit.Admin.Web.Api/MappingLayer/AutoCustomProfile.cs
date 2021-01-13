using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yit.Admin.Web.Api.MappingLayer
{
    public class AutoCustomProfile:Profile
    {
        public AutoCustomProfile()
        {
            //CreateMap<SYS_ROLE_LINK, SysRoleLink>()
            //   .ForMember(d => d.ROLEID, o => o.MapFrom(s => s.RoleID))
            //   .ForMember(d => d.LINKNO, o => o.MapFrom(s => s.LinkNO))
            //   .ForMember(d => d.MENUID, o => o.MapFrom(s => s.MenuID))
            //   .ForMember(d => d.USERID, o => o.MapFrom(s => s.UserID));
            //CreateMap<SYS_DEPARTMENT, SysDepartment>();
        }
    }
}

using KMHC.SLTC.Business.Entity;
using SC.Business.Entity.Filter;
using SC.Business.Entity.Models;
using SC.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.Business.Implement
{
    public class RoleService : BaseService, IRoleService
    {
        public BaseResponse<IList<Role>> QueryRole(BaseRequest<RoleFilter> request)
        {
            BaseResponse<IList<Role>> response = new BaseResponse<IList<Role>>();
            response.Data = new List<Role>();
            var role = new Role
            {
                RoleId = "1",
                RoleName = "角色1",
                RoleType = "1",
                Description = "管理员角色",
                Status = true,
            };
            response.Data.Add(role);
            response.PagesCount = 1;
            response.RecordsCount = 1;
            return response;
        }

        public BaseResponse<Role> GetRole(string roleID)
        {
            BaseResponse<Role> response = new BaseResponse<Role>();
            response.Data = new Role
            {
                RoleId = "1",
                RoleName = "角色1",
                RoleType = "1",
                Description = "管理员角色",
                Status = true,
            };
            return response;
        }

    }
}

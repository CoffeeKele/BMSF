using KMHC.SLTC.Business.Entity;
using SC.Business.Entity.Filter;
using SC.Business.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.Business.Interface
{
    public interface IRoleService
    {
        BaseResponse<IList<Role>> QueryRole(BaseRequest<RoleFilter> request);
        BaseResponse<Role> GetRole(string roleID);
    }
}

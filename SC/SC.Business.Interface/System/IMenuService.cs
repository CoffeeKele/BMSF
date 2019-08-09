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
    public interface IMenuService
    {
        IList<Module> GetInitializeModule();

        BaseResponse<IList<TreeNode>> GetInitializeModuleByRole();
    }
}

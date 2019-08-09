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
    public interface IUserService
    {
        BaseResponse<IList<User>> QueryUser(BaseRequest<UserFilter> request);

        bool IsExistUser(string name, string pwd);

        BaseResponse<User> GetUser(string name, string pwd);
    }
}

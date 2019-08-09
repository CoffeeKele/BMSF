using KMHC.SLTC.Business.Entity;
using SC.Business.Entity.Filter;
using SC.Business.Entity.Models;
using SC.Business.Interface;
using SC.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SC.Business.Implement
{
    public class UserService:BaseService,IUserService
    {
        public BaseResponse<IList<User>> QueryUser(BaseRequest<UserFilter> request)
        {
            var response = base.Query<sys_User, User>(request, (q) =>
            {
                q = q.OrderByDescending(m => m.CreateDate);
                return q;
            });
            //response.Data = new List<User>();
            //var user = new User
            //{
            //    UserId = 1,
            //    EmpName = "用户1",
            //    LogonName = "管理员",
            //    PwdExpDate = DateTime.Now.AddDays(30),
            //    Status = true,
            //};
            //response.Data.Add(user);
            response.PagesCount = 1;
            response.RecordsCount = 1;

            return response;
        }

        public bool IsExistUser(string name,string pwd)
        {
            var result = false;
            var response = base.Query<sys_User, User>(null, (q) =>
            {
                q = q.Where(m => m.Name == name && m.PassWard == pwd);
                q = q.OrderByDescending(m => m.CreateDate);
                return q;
            });

            if(response.RecordsCount>0)
            {
                result = true;
            }
            return result;
        }
        public BaseResponse<User> GetUser(string name, string pwd)
        {
          
            return base.Get<sys_User, User>(m=>m.Name==name && m.PassWard==pwd);
        }
    }
}

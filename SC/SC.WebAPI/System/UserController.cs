using KMHC.SLTC.Business.Entity;
using SC.Business.Entity.Filter;
using SC.Business.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace SC.WebAPI.System
{

    [RoutePrefix("api/user")]
    public class UserController:BaseController
    {
        IUserService userService = IOCContainer.Instance.Resolve<IUserService>();

        [Route(""), HttpGet]
        public IHttpActionResult Query()
        {
            var request = new BaseRequest<UserFilter>() {

            };
            var response = userService.QueryUser(request);
            return Ok(response);
        }


    }
}
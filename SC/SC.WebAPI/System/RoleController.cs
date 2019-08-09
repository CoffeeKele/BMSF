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

    [RoutePrefix("api/role")]
    public class RoleController:BaseController
    {
        IRoleService roleService = IOCContainer.Instance.Resolve<IRoleService>();

        [Route(""), HttpGet]
        public IHttpActionResult Query()
        {
            var request = new BaseRequest<RoleFilter>() {

            };
            var response = roleService.QueryRole(request);
            return Ok(response);
        }

        [Route("{RoleId}")]
        public IHttpActionResult Get(string RoleId)
        {
            var response = roleService.GetRole("1");
            return Ok(response);
        }
    }
}
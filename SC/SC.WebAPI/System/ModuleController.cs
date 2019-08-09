
using SC.Business.Entity.Filter;
using SC.Business.Entity.Models;
using SC.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SC.WebAPI.System
{
    [RoutePrefix("api/module")]
    public class ModuleController : BaseController
    {

        IMenuService menuService = IOCContainer.Instance.Resolve<IMenuService>();
        [Route(""), HttpGet]
        public IHttpActionResult Query()
        {
            var response = menuService.GetInitializeModule();
            return Ok<IList<Module>>(response);
        }


        [Route("{roleId}")]
        public IHttpActionResult Get(string roleId, string type = "", bool loadTreeByRole = false)
        {

            if (type == "tree")
            {
                var response = menuService.GetInitializeModuleByRole();
                return Ok(response);
            }
            else
            {
                return Ok("type參數不正確");
            }
        }


    }
}

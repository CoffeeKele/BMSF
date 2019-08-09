
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SC.Business.Interface;

namespace SC.WebAPI.Demo
{
    [RoutePrefix("api/demo")]
    public class DemoController : BaseController
    {

        IDemoService demoService = IOCContainer.Instance.Resolve<IDemoService>();
        [Route(""), HttpGet]
        public IHttpActionResult GetDemoData()
        {
            var result = demoService.GetData();
            return Ok<string>(result);
        }
    }
}

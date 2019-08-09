using SC.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SC.Infrastructure
{
    public class SecurityHelper
    {
        public static ICustomPrincipal CurrentPrincipal
        {
            get
            {
                return HttpContext.Current.User as ICustomPrincipal;
            }
        }
    }
}

using SC.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.Business.Implement
{
    public class DemoService : IDemoService
    {
        public string GetData()
        {
            return "Hello World";
        }
    }
}

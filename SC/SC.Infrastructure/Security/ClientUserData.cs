using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.Infrastructure.Security
{
    public class ClientUserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public DateTime? PwdExpDate { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string ImgUrl { get; set; }
        public string Email { get; set; }
        public string EmpNo { get; set; }
        public string RoleId { get; set; }
        public string RoleType { get; set; }

    }
}

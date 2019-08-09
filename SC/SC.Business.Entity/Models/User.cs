using System;

namespace SC.Business.Entity.Models
{
    public class User
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
        public DateTime? LastLogonDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public bool? Status { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SC.Business.Entity.Models
{
    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public bool Status { get; set; }
        public List<TreeNode> CheckModuleList { get; set; }
        public string SysType { get; set; }
    }
    public partial class TreeNode
    {
        public string moduleId { get; set; }
        public string text { get; set; }
        public string href { get; set; }
        public State state { get; set; }
        public List<TreeNode> nodes { get; set; }

        public TreeNode()
        {
            //this.nodes = new List<TreeNode>();
        }
    }

    public partial class State
    {
        public bool @checked { get; set; }
        //public bool disabled { get; set; }
        //public bool expanded { get; set; }
        //public bool selected { get; set; }
    }
}
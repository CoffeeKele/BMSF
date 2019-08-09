using KMHC.SLTC.Business.Entity;
using SC.Business.Entity.Filter;
using SC.Business.Entity.Models;
using SC.Business.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SC.Business.Implement
{
    public class MenuService : BaseService, IMenuService
    {
        public IList<Module> GetInitializeModule()
        {
            var response = new List<Module>();
            var module = new Module
            {
                ModuleId = "01",
                ModuleName = "系统管理",
                SuperModuleId = "00",
            };
            response.Add(module);
            module = new Module
            {
                ModuleId = "01001",
                ModuleName = "用户管理",
                SuperModuleId = "01",
                Url = "/angular/UserList",
                Target = "2",
            };
            response.Add(module);
            module = new Module
            {
                ModuleId = "01002",
                ModuleName = "角色管理",
                SuperModuleId = "01",
                Url = "/angular/RoleList",
                Target = "2",
            };
            response.Add(module);
            module = new Module
            {
                ModuleId = "02",
                ModuleName = "Demo功能演示",
                SuperModuleId = "00",
            };
            response.Add(module);
            module = new Module
            {
                ModuleId = "02001",
                ModuleName = "Demo",
                SuperModuleId = "02",
                Url = "/angular/Demo",
                Target = "2",
            };

            response.Add(module);
            return response;
        }

        private void LoadTree(TreeNode parentNode, IList<Module> modules, IEnumerable<Module> modulesByRole, string moduleId)
        {
            var nodes = modules.Where(m => m.SuperModuleId == moduleId).ToList();
            if (nodes.Count > 0 && parentNode.nodes == null)
            {
                parentNode.nodes = new List<TreeNode>();
            }
            foreach (var item in nodes)
            {
                var newNode = new TreeNode();
                newNode.moduleId = item.ModuleId;
                newNode.text = item.ModuleName;
                newNode.href = item.Url;
                newNode.state = new State { @checked = modulesByRole.Any(m => m.ModuleId == item.ModuleId) };
                parentNode.nodes.Add(newNode);
                LoadTree(newNode, modules, modulesByRole, item.ModuleId);
            }
        }

        public BaseResponse<IList<TreeNode>> GetInitializeModuleByRole()
        {
            BaseResponse<IList<TreeNode>> response = new BaseResponse<IList<TreeNode>>();
            var moduleList = this.GetInitializeModule();
            var moduleListByRole = this.GetInitializeModule();
            TreeNode rootNode = new TreeNode();
            LoadTree(rootNode, moduleList, moduleListByRole, "00");
            response.Data = rootNode.nodes;
            return response;
        }
    }

}

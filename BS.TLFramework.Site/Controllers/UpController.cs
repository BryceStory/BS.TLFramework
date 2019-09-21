using BS.TLFramework.IService;
using BS.TLFramework.Model;
using Framework.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BS.TLFramework.Site.Controllers
{
    [AllowAnonymous]
    public class UpController : BaseController
    {
        private IMenuService iMenuService;
        private IMenuActionService iMenuActionService;
        private DbContext dbContext;

        public UpController(IMenuService iMenuService,
           IMenuActionService iMenuActionService,
           DbContext dbContext)
        {
            this.iMenuService = iMenuService;
            this.iMenuActionService = iMenuActionService;
            this.dbContext = dbContext;
        }
        public string Index()
        {
            Init();

            this.iMenuService.Commit();

            return "升级完成";
        }

        private void Init()
        {
            InitMenu();
            InitMenuAction();
        }

        private void InitMenu()
        {
            IList<Menu> list = new List<Menu>();
            list.Add(new Menu() { Code = "001", Name = "系统资料", Sort = 9999 });
            list.Add(new Menu() { Code = "001001", Name = "基础设置", Sort = 9999 });
            list.Add(new Menu() { Code = "001001001", Name = "员工资料", Controller = "User", Action = "Index,Create,Query", Url = "/User/Index", Sort = 1 });
            list.Add(new Menu() { Code = "001001002", Name = "角色配置", Controller = "Role", Action = "Index,Create,Query", Url = "/Role/Index", Sort = 2 });
            list.Add(new Menu() { Code = "001001003", Name = "部门配置", Controller = "Department", Action = "Index,Create,Query", Url = "/Department/Index", Sort = 3 });
          
            foreach (var m in list)
            {
                Menu menu = iMenuService.Get(t => t.Code == m.Code) ?? new Menu();
                menu.Code = m.Code;
                menu.ParentCode = !string.IsNullOrWhiteSpace(m.Code.Substring(0, m.Code.Length - 3)) ? m.Code.Substring(0, m.Code.Length - 3) : null;
                menu.Name = m.Name;
                menu.Url = m.Url;
                menu.Sort = m.Sort;
                menu.Controller = m.Controller;
                menu.Action = m.Action;
                menu.Status = 1;

                iMenuService.Save(menu);
            }
        }


        private void InitMenuAction()
        {
            List<MenuAction> list = new List<MenuAction>();
            list.Add(new MenuAction() { MenuCode = "001001001", Code = ActionCode.Index.ToString(), Name = ActionCode.Index.ToDescription(), Sort = 1 });
            list.Add(new MenuAction() { MenuCode = "001001001", Code = ActionCode.Add.ToString(), Name = ActionCode.Add.ToDescription(), Sort = 2 });
            list.Add(new MenuAction() { MenuCode = "001001001", Code = ActionCode.Modify.ToString(), Name = ActionCode.Modify.ToDescription(), Sort = 3 });
            list.Add(new MenuAction() { MenuCode = "001001001", Code = ActionCode.Delete.ToString(), Name = ActionCode.Delete.ToDescription(), Sort = 4 });

            list.Add(new MenuAction() { MenuCode = "001001002", Code = ActionCode.Index.ToString(), Name = ActionCode.Index.ToDescription(), Sort = 1 });
            list.Add(new MenuAction() { MenuCode = "001001002", Code = ActionCode.Add.ToString(), Name = ActionCode.Add.ToDescription(), Sort = 2 });
            list.Add(new MenuAction() { MenuCode = "001001002", Code = ActionCode.Modify.ToString(), Name = ActionCode.Modify.ToDescription(), Sort = 3 });
            list.Add(new MenuAction() { MenuCode = "001001002", Code = ActionCode.Delete.ToString(), Name = ActionCode.Delete.ToDescription(), Sort = 4 });

            list.Add(new MenuAction() { MenuCode = "001001003", Code = ActionCode.Index.ToString(), Name = ActionCode.Index.ToDescription(), Sort = 1 });
            list.Add(new MenuAction() { MenuCode = "001001003", Code = ActionCode.Add.ToString(), Name = ActionCode.Add.ToDescription(), Sort = 2 });
            list.Add(new MenuAction() { MenuCode = "001001003", Code = ActionCode.Modify.ToString(), Name = ActionCode.Modify.ToDescription(), Sort = 3 });
            list.Add(new MenuAction() { MenuCode = "001001003", Code = ActionCode.Delete.ToString(), Name = ActionCode.Delete.ToDescription(), Sort = 4 });


            foreach (var m in list)
            {
                MenuAction menu = iMenuActionService.Get(t => t.Code == m.Code && t.MenuCode == m.MenuCode) ?? new MenuAction();
                menu.MenuCode = m.MenuCode;
                menu.Code = m.Code;
                menu.Name = m.Name;
                menu.Sort = m.Sort;
                menu.Status = 1;
                iMenuActionService.Save(menu);
            }
        }
    }
}
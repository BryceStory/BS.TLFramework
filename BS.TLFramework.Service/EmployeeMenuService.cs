using BS.TLFramework.IService;
using BS.TLFramework.Model;
using Framework.Cache;
using Framework.Common;
using Framework.Expression;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BS.TLFramework.Service
{
    public class EmployeeMenuService : BusinessService<EmployeeMenu>, IEmployeeMenuService
    {
        public override Expression<Func<EmployeeMenu, bool>> GetExpress<QM>(QM model)
        {
            Expression<Func<EmployeeMenu, bool>> express = PredicateBuilder.True<EmployeeMenu>();

            if (model.Status == null)
            {
                express = express.And(t => t.Status == 1);
            }
            else
            {
                express = express.And(t => t.Status == model.Status);
            }

            return express;
        }

        public IList<EmployeeMenu> GetEmployeeMenuList()
        {
            var list = new List<EmployeeMenu>();
            var iUserService = DIFactory.GetContainer().Resolve<IEmployeeService>();
            var iMenuService = DIFactory.GetContainer().Resolve<IEmployeeMenuService>();
            var iMenuActionService = DIFactory.GetContainer().Resolve<IEmployeeMenuActionService>();

            var user = iUserService.Get(t => t.ID == currentUser.ID);
            if (user != null)
            {
                if (user.IsAdmin > 0)
                {
                    return GetList(new EmployeeMenuQM() { PageSize = Int32.MaxValue });
                }
                else if (user.EmployeeRole != null && !string.IsNullOrWhiteSpace(user.EmployeeRole.MenuActions))
                {
                    var actionArray = user.EmployeeRole.MenuActions.ToLongList();
                    var actionList = iMenuActionService.Gets(t => actionArray.Contains(t.ID) && t.Code == "Index").ToList();
                    foreach (var m in actionList)
                    {
                        list.Add(m.EmployeeMenu);
                        if (list.FirstOrDefault(t => t.Code == m.EmployeeMenu.Code.Substring(0, 6)) == null)
                        {
                            list.Add(this.Get(t => t.Code == m.EmployeeMenu.Code.Substring(0, 6)));
                        }
                        if (list.FirstOrDefault(t => t.Code == m.EmployeeMenu.Code.Substring(0, 3)) == null)
                        {
                            list.Add(this.Get(t => t.Code == m.EmployeeMenu.Code.Substring(0, 3)));
                        }
                    }
                }
            }
            return list;
        }



        public void SetCache()
        {
            var list = GetAllListModel<EmployeeMenuLM, EmployeeMenuQM>();

            CacheHelper.SetCache(CacheKey.Menu.ToString(), list, DateTime.Now.AddYears(100));
        }
    }
}

using BS.TLFramework.IService;
using BS.TLFramework.Model;
using BS.TLFramework.Model.Admin;
using Framework.Common;
using Framework.Expression;
using Framework.Models;
using Framework.Security;
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
    public class EmployeeService : BusinessService<Employee>, IEmployeeService
    {
        public override Expression<Func<Employee, bool>> GetExpress<QM>(QM model)
        {
            Expression<Func<Employee, bool>> express = PredicateBuilder.True<Employee>();

            var m = model as EmployeeQM;

            if (currentUser.IsAdmin)
            {
                if (!string.IsNullOrWhiteSpace(m.Code))
                {
                    express = express.And(t => t.Merchant.Code == m.Code.Trim());
                }
                if (!string.IsNullOrWhiteSpace(m.MerchantName))
                {
                    express = express.And(t => t.Merchant.Name.Contains(m.MerchantName.Trim()));
                }

                if (!string.IsNullOrWhiteSpace(m.Name))
                {
                    express = express.And(t => t.Name.Contains(m.Name.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(m.Mobile))
                {
                    express = express.And(t => t.Mobile == m.Mobile.Trim());
                }
                if (currentUser.MerchantID != 0)
                {
                    express = express.And(t => t.MerchantID == currentUser.MerchantID);
                }
            }
            else
            {
                if (m.MerchantID != null)    //客户端用户查询时，默认根据当前用户的商户ID来查询（MerchantID）
                {
                    express = express.And(t => t.MerchantID == m.MerchantID);
                }

                if (!string.IsNullOrWhiteSpace(m.Code))
                {
                    express = express.And(t => t.Code == m.Code.Trim() && t.MerchantID == m.MerchantID);
                }
                if (!string.IsNullOrWhiteSpace(m.MerchantName))
                {
                    express = express.And(t => t.Merchant.Name == m.MerchantName.Trim() && t.MerchantID == m.MerchantID);
                }

                if (!string.IsNullOrWhiteSpace(m.Name))
                {
                    express = express.And(t => t.Name.Contains(m.Name.Trim()) && t.MerchantID == m.MerchantID);
                }
            }

            if (m.Status != null)
            {
                express = express.And(t => t.Status == m.Status);
            }

            return express;
        }

        public LoginStatus Login(LoginModel model)
        {
            LoginStatus loginResult = Verify(model);

            if (loginResult == LoginStatus.Success)
            {
                var user = Get(t => t.Code == model.UserCode && t.Merchant.Code == model.MerchantCode);

                CacheCurrnetUser(user);
            }

            return loginResult;
        }

        private LoginStatus Verify(LoginModel model)
        {
            var user = Get(t => t.Code == model.UserCode && t.Merchant.Code == model.MerchantCode);

            if (user == null)
                return LoginStatus.NotExists;

            if (user.Merchant.Code != model.MerchantCode)
                return LoginStatus.MerchantNotExists;

            if (!PasswordHasher.VerifyHashedPassword(user.Password, model.Password))
                return LoginStatus.PasswordError;

            if (user.Status == LoginStatus.Lock.ToInt())
                return LoginStatus.Lock;

            if (user.Status == LoginStatus.Disable.ToInt())
                return LoginStatus.Disable;

            if (user.Status == LoginStatus.NotActive.ToInt())
                return LoginStatus.NotActive;

            if (!PasswordHasher.VerifyHashedPassword(user.Password, model.Password))
                return LoginStatus.Success;

            return LoginStatus.Unknown;
        }

        //获取拥有的权限
        private List<ActionModel> GetAction(Employee user)
        {
            IEmployeeMenuActionService iMenuActionService = DIFactory.GetContainer().Resolve<IEmployeeMenuActionService>();

            List<EmployeeMenuAction> menuActionlist = new List<EmployeeMenuAction>();

            List<ActionModel> actionList = new List<ActionModel>();

            if (user.IsAdmin == 0)
            {
                menuActionlist = iMenuActionService.Gets(t => user.MenuActionList.Contains(t.ID)).ToList();
            }
            else
            {
                menuActionlist = iMenuActionService.Gets().ToList();
            }

            foreach (var m in menuActionlist)
            {
                actionList.Add(new ActionModel()
                {
                    Controller = m.EmployeeMenu.Controller.ToLower(),
                    Action = m.Code.ToLower(),
                });
            }

            return actionList;
        }

        public CurrentUser ConvertCurrentUser(Employee user)
        {
            CurrentUser userModel = new CurrentUser()
            {
                ID = user.ID,
                UserCode = user.Code,
                UserName = user.Name,
                MerchantID = user.MerchantID,
                // AccountBank = user.Merchant.AccountBank,
                IsAdmin = user.IsAdmin > 0 ? true : false,
                AllAction = GetAction(user),
            };

            return userModel;
        }

        public void CacheCurrnetUser(Employee user)
        {
            UserHelper.SetCurrentUser(ConvertCurrentUser(user));
        }

        public List<Employee> GetAuthList()
        {
            Expression<Func<Employee, bool>> express = PredicateBuilder.True<Employee>();

            if (!currentUser.IsAdmin)
            {
                var users = this.GetDepartmentUsers(currentUser.ID);
                express = express.And(t => users.Contains(t.ID));
            }

            return this.Gets(express, 1, Int32.MaxValue, new OrderByExpression<Employee, string>(t => t.Name)).ToList();
        }

    }
}

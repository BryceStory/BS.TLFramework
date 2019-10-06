using BS.TLFramework.IService;
using BS.TLFramework.Model;
using Framework.Common;
using Framework.Expression;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Framework.Security;
using Framework.Web.IOC;
using Unity;
using System.Linq.Dynamic;
using BS.TLFramework.Model.Admin;

namespace BS.TLFramework.Service
{
    public class UserService : BusinessService<User>, IUserService
    {
        public new void Save(User e)
        {
            if (string.IsNullOrWhiteSpace(e.Code))
            {
                throw new Exception(ErrorCode.CodeIsNotNull.ToDescription());
            }

            if (Get(t => t.Code == e.Code && t.ID != e.ID) != null)
            {
                throw new Exception(ErrorCode.CodeIsNotNull.ToDescription());
            }
            base.Save(e);
        }

        public override Expression<Func<User, bool>> GetExpress<M>(M model)
        {
            Expression<Func<User, bool>> express = PredicateBuilder.True<User>();

            var m = model as UserQM;

            express = express.And(t => t.Code != "admin");

            if (!string.IsNullOrWhiteSpace(m.UserCode))
            {
                express = express.And(t => t.Code.Contains(m.UserCode.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(m.UserName))
            {
                express = express.And(t => t.Name.Contains(m.UserName.Trim()));
            }

            if (m.DepartmentID != null)
            {
                if (m.DepartmentID > 0)
                {
                    express = express.And(t => t.DepartmentID == m.DepartmentID);
                }
                else
                {
                    express = express.And(t => t.DepartmentID == null);
                }
            }

            if (m.RoleID != null)
            {
                if (m.RoleID > 0)
                {
                    express = express.And(t => t.RoleID == m.RoleID);
                }
                else
                {
                    express = express.And(t => t.RoleID == null);
                }
            }

            if (m.Status != null)
            {
                express = express.And(t => t.Status == m.Status);
            }

            if (!currentUser.IsAdmin)
            {
                var users = this.GetDepartmentUsers(currentUser.ID);

                express = express.And(t => users.Contains(t.ID));
            }

            return express;
        }


        public LoginStatus Login(LoginModel model)
        {
            LoginStatus loginResult = Verify(model);

            if (loginResult == LoginStatus.Success)
            {
                var user = Get(t => t.Name == model.UserCode);

                CacheCurrnetUser(user);
            }

            return loginResult;
        }

        private LoginStatus Verify(LoginModel model)
        {
            var user = Get(t => t.Name == model.UserCode && t.Merchant.Code == model.MerchantCode);

            var verifyResult = PasswordHasher.VerifyHashedPassword(PasswordHasher.HashPassword(model.Password), model.Password);

            if (user == null)
                return LoginStatus.NotExists;

            if (!verifyResult)
                return LoginStatus.PasswordError;

            if (user.Status == LoginStatus.Lock.ToInt())
                return LoginStatus.Lock;

            if (user.Status == LoginStatus.Disable.ToInt())
                return LoginStatus.Disable;

            if (user.Status == LoginStatus.NotActive.ToInt())
                return LoginStatus.NotActive;

            if (verifyResult)
                return LoginStatus.Success;

            return LoginStatus.Unknown;
        }

        public void CacheCurrnetUser(User user)
        {
            UserHelper.SetCurrentUser(ConvertCurrentUser(user));
        }

        public CurrentUser ConvertCurrentUser(User user)
        {
            CurrentUser userModel = new CurrentUser()
            {
                ID = user.ID,
                UserCode = user.Code,
                UserName = user.Name,
                IsAdmin = user.IsAdmin,
                AllAction = GetAction(user),
            };

            return userModel;
        }

        private List<ActionModel> GetAction(User user)
        {
            IMenuActionService iMenuActionService = DIFactory.GetContainer().Resolve<IMenuActionService>();

            List<MenuAction> menuActionlist = new List<MenuAction>();
            List<ActionModel> actionList = new List<ActionModel>();

            if (!user.IsAdmin)
            {
                menuActionlist = iMenuActionService.Gets(t => user.MenuActionList.Contains(t.ID)).ToList();
            }
            else
            {
                menuActionlist = iMenuActionService.Gets().ToList();
            }

            foreach (var m in menuActionlist)
            {
                actionList.Add(new ActionModel
                {
                    Controller = m.Menu.Controller.ToLower(),
                    Action = m.Code.ToLower()
                });
            }

            return actionList;
        }

        public List<User> GetAuthList()
        {
            Expression<Func<User, bool>> express = PredicateBuilder.True<User>();

            if (!currentUser.IsAdmin)
            {
                var users = this.GetDepartmentUsers(currentUser.ID);
                express = express.And(t => users.Contains(t.ID));
            }

            return this.Gets(express, 1, Int32.MaxValue, new OrderByExpression<User, string>(t => t.Name)).ToList();
        }
    }
}

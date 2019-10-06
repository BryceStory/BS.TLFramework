using BS.TLFramework.IService;
using BS.TLFramework.Model;
using BS.TLFramework.Model.Admin;
using Framework.Common;
using Framework.Expression;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace BS.TLFramework.Site.Controllers
{
    //[AllowAnonymous]
    public class HomeController : BaseController
    {
        private IUserService iUserService;
        private IMenuService iMenuService;
        private IMerchantService iMerchantService;

        public HomeController(IUserService iUserService,
                              IMenuService iMenuService,
                              IMerchantService iMerchantService)
        {
            this.iUserService = iUserService;
            this.iMenuService = iMenuService;
            this.iMerchantService = iMerchantService;
        }

        public ActionResult Index()
        {
            ViewBag.UserMenuList = iMenuService.GetUserMenuList();

            return View();
        }

        public void RegisterAdmin()
        {
            //Merchant merchant = new Merchant()
            //{
            //    Code = "10000",
            //    Name = "AdminMerchant",
            //    Status = 1,
            //    Mobile = "15273738880"
            //};

            //iMerchantService.Save(merchant);
            //iMerchantService.Commit();


            //User user = new User()
            //{
            //    MerchantID = 1,
            //    Code = "1000",
            //    Name = "admin",
            //    Email = "448174650@qq.com",
            //    Mobile = "15273738880",
            //    Password = Framework.Security.PasswordHasher.HashPassword("123456"),
            //    IsMore = 0,
            //    LastLoginDate = DateTime.Now,
            //    RoleID = null,
            //    DepartmentID = null,
            //    AuthDepartmentIDs = "0,1"

            //};

            //Menu menu = new Menu()
            //{
            //    Code = "001"
            //};

            //iUserService.Save(user);
            //iUserService.Commit();

            //var res = iUserService.Get(t => t.ID == 10010);
            //var res4 = iUserService.GetList<UserQM>();

            //using (TLFrameworkDb dbcontext = new TLFrameworkDb())
            //{
            //    var express = PredicateBuilder.True<Menu>();
            //    express = express.And(t => t.Code == menu.Code);
            //    var ress = dbcontext.Set<Menu>().Where<Menu>(express.Compile()).ToList();
            //}

            //var res1 = iMenuService.Get(t => t.Code == menu.Code);
            //var res2 = iMenuService.GetAllList<MenuQM>();
            //var a = "";
        }

        public ActionResult Logout()
        {
            UserHelper.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}
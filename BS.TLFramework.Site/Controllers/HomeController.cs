using BS.TLFramework.IService;
using BS.TLFramework.Model;
using BS.TLFramework.Model.Admin;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace BS.TLFramework.Site.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private IUserService iUserService;
        private IMenuService iMenuService;

        public HomeController(IUserService iUserService,
                              IMenuService iMenuService)
        {
            this.iUserService = iUserService;
            this.iMenuService = iMenuService;
        }

        public ActionResult Index()
        {
            //User user = new User()
            //{
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

            Menu menu = new Menu()
            {
                Code = "001"
            };

            //iUserService.Save(user);
            //iUserService.Commit();

            // var res = iUserService.Get(t => t.ID == 10010);
            //var res4 = iUserService.GetList<UserQM>();
            var res1 = iMenuService.Get(t => t.Code == menu.Code);
            var res2 = iMenuService.GetAllList<MenuQM>();
            var a = "";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
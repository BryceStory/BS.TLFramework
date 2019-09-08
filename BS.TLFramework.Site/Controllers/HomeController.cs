using BS.TLFramework.IService;
using BS.TLFramework.Model;
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
    public class HomeController : BaseController
    {
        private IUserService iUserService;

        public HomeController(IUserService iUserService)
        {
            this.iUserService = iUserService;
        }

        public ActionResult Index()
        {
            //User user = new User()
            //{
            //    Code = "1000",
            //    Name = "admin",
            //    Email = "448174650@qq.com",
            //    Mobile = "15273738880",
            //    Password = "123456",
            //    IsMore=0,
            //    LastLoginDate=DateTime.Now,
            //    RoleID=0,
            //    DepartmentID=0,
            //    AuthDepartmentIDs="0,1"
            //};

            //iUserService.Save(user);
            //iUserService.Commit();

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
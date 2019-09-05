using BS.TLFramework.IService;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BS.TLFramework.Site.Controllers
{
    public class HomeController : Controller
    {
        private IUserService iUserService;

        public HomeController(IUserService iUserService)
        {
            this.iUserService = iUserService;
        }

        public ActionResult Index()
        {
            var a = this.iUserService.Get(1);
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
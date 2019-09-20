using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BS.TLFramework.Site.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult Error()
        {
            return PartialView();
        }
    }
}
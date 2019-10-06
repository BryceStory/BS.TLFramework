using BS.TLFramework.IService;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace BS.TLFramework.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//控制器的实例化走UnityControllerFactory
        }

        public override void Init()
        {
            var iMenuActionService = DIFactory.GetContainer().Resolve<IMenuActionService>();
            var iMenuService = DIFactory.GetContainer().Resolve<IMenuService>();

            iMenuActionService.SetCache();
            iMenuService.SetCache();
        }
    }
}

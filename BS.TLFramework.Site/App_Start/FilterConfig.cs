using BS.TLFramework.Site.Filter;
using System.Web;
using System.Web.Mvc;

namespace BS.TLFramework.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilter());
            filters.Add(new AuthorityFilter());
        }
    }
}

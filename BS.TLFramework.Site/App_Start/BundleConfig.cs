using System.Web;
using System.Web.Optimization;

namespace BS.TLFramework.Site
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/modernizr-*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Plugins").Include(
                //"~/Scripts/jquery-3.3.1.min.js",
                 "~/Plugins/My97DatePicker/WdatePicker.js",
                 "~/Plugins/layer/2.1/layer.js",
                 "~/Plugins/layer/2.1/extend/layer.ext.js",
                 "~/Plugins/icheck/jquery.icheck.min.js",
                 "~/Plugins/h-ui/js/H-ui.js",
                 "~/Plugins/h-ui.admin/js/H-ui.admin.js",
                 "~/Plugins/Viewer/js/viewer.min.js",
                 "~/Plugins/Viewer/js/viewer-jquery.min.js",
                 "~/Plugins/Viewer/js/viewer.min.js",
                 "~/Plugins/Viewer/js/viewer-jquery.min.js",
                 "~/Scripts/Common.js"


                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Plugins/h-ui/css/H-ui.css",
                      "~/Plugins/h-ui.admin/css/H-ui.admin.css",
                      "~/Plugins/Hui-iconfont/1.0.7/iconfont.css",
                      "~/Plugins/icheck/icheck.css",
                      "~/Plugins/h-ui.admin/skin/default/skin.css",
                      "~/Plugins/h-ui.admin/css/style.css",
                      "~/Content/Pager.css",
                      "~/Content/font-awesome.min.css",
                      "~/Plugins/bootstrap-multiselect/bootstrap-multiselect.css",
                      "~/Plugins/Viewer/css/viewer.min.css"
                      ));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace Przychodnia
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/sb-admin-2.js",
                      "~/vendor/metisMenu/metisMenu.min.js",
                      "~/vendor/raphael/raphael.min.js",
                      "~/vendor/morrisjs/morris.min.js",
                      "~/Scripts/data.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/sb-admin-2.css",
                      "~/vendor/metisMenu/metisMenu.min.css",
                      "~/vendor/morrisjs/morris.css",
                      "~/vendor/font-awesome/css/font-awesome.min.css"));
        }
    }
}

﻿using System.Web;
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
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

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
                      "~/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                      "~/vendor/datatables-responsive/dataTables.responsive.js",
                      "~/vendor/datatables/js/jquery.dataTables.min.js",
                      "~/Scripts/data.js",
                      "~/Scripts/przychodnia.hidener.js",
                      "~/Scripts/jquery-ui.js",

                      "~/vendor/flot/excanvas.min.js",
                      "~/vendor/flot/jquery.flot.js",
                      "~/vendor/flot/jquery.flot.pie.js",
                      "~/vendor/flot/jquery.flot.resize.js",
                      "~/vendor/flot/jquery.flot.time.js",
                      "~/vendor/flot-tooltip/jquery.flot.tooltip.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/sb-admin-2.css",
                      "~/vendor/metisMenu/metisMenu.min.css",
                      "~/vendor/morrisjs/morris.css",
                      "~/vendor/font-awesome/css/font-awesome.min.css",
                      "~/vendor/datatables/css/dataTables.uikit.css",
                      "~/vendor/datatables/css/jquery.dataTables.css",
                      "~/vendor/datatables/css/dataTables.material.css",
                      "~/vendor/datatables-plugins/dataTables.bootstrap.css",
                      "~/vendor/datatables-responsive/dataTables.responsive.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.structure.css",
                      "~/Content/jquery-ui.theme.css"));
        }
    }
}

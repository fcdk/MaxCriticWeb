using System.Web;
using System.Web.Optimization;

namespace CriticWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/tooltipstercss").Include(
                      "~/Content/tooltipster.css"));

            bundles.Add(new ScriptBundle("~/bundles/tooltipsterjs").Include(
                      "~/Scripts/jquery.tooltipster.min.js"));

            bundles.Add(new StyleBundle("~/Content/star-rating").Include(
                      "~/Content/star-rating.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/star-ratingjs").Include(
                      "~/Scripts/star-rating.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery-bootpag").Include(
                      "~/Scripts/jquery.bootpag.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScripts").Include(
                      "~/Scripts/myScripts.js"));
        }
    }
}

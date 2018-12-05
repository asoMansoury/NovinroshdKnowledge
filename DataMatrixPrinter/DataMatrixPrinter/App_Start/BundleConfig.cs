using System.Web.Optimization;

namespace DataMatrixPrinter
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            #region Stylesheets

            bundles.Add(new StyleBundle("~/Styles/Global").Include(
                "~/Assets/css/icons/icomoon/styles.css",
                "~/Assets/css/bootstrap.css",
                "~/Assets/css/core.css",
                "~/Assets/css/components.css",
                "~/Assets/css/colors.css",
                "~/Assets/css/custom.css"));

            bundles.Add(new StyleBundle("~/Styles/PersianDateTimePicker").Include(
                "~/Assets/css/jquery.Bootstrap-PersianDateTimePicker.css"));

            #endregion

            #region Scripts

            bundles.Add(new ScriptBundle("~/Scripts/Core").Include(
                "~/Assets/js/plugins/loaders/pace.min.js",
                "~/Assets/js/core/libraries/jQuery-2.1.4.min.js",
                "~/Assets/js/core/libraries/jquery_validate/jquery.validate.min.js",
                "~/Assets/js/core/libraries/bootstrap.min.js",
                "~/Assets/js/plugins/notifications/jgrowl.min.js",
                "~/Assets/js/core/Utils.js",
                "~/Assets/js/plugins/loaders/blockui.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Datatables").Include(
                "~/Assets/js/plugins/tables/datatables/datatables.min.js",
                "~/Assets/js/plugins/tables/datatables/extensions/responsive.min.js",
                "~/Assets/js/pages/datatables_responsive.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Select2").Include(
                "~/Assets/js/plugins/forms/selects/select2.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/App").Include(
                "~/Assets/js/core/app.js",
                "~/Assets/js/plugins/ui/ripple.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/UniForm").Include(
                "~/Assets/js/plugins/forms/styling/uniform.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/FormLayout").Include(
                "~/Assets/js/pages/form_layouts.js"));

            bundles.Add(new ScriptBundle("~/Scripts/PersianDateTimePicker").Include(
                "~/Assets/js/jalaali.js",
                "~/Assets/js/jquery.Bootstrap-PersianDateTimePicker.js"));

            bundles.Add(new ScriptBundle("~/Scripts/JqueryPriceFormat").Include(
                "~/Assets/js/plugins/Price-Format/jquery.priceformat.js"));

            #endregion
        }
    }
}

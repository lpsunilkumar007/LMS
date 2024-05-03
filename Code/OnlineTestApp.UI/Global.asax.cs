using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineTestApp.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
             
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(string), new Filters.TrimModelBinder());
            BundleTable.EnableOptimizations = Utilities.AppSettings.GetBoolValue("EnableOptimizations");
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var y = Server.GetLastError();
        }
    }
}

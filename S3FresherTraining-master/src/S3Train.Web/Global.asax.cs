using S3Train.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace S3Train
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.All;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyConfig.RegisterDependencyResolvers();
        }
    }
}

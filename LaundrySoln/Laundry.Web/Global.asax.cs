using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Laundry.DAL;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Laundry.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //  Database.SetInitializer(
            //new MigrateDatabaseToLatestVersion<LaundryContext, Laundry.Web.Migrations.Configuration>());
         //   Database.SetInitializer(
         //new MigrateDatabaseToLatestVersion<LaundryContext, Configuration>());
        }
    }
}

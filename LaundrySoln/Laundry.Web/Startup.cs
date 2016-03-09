using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Laundry.Web.Startup))]
namespace Laundry.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

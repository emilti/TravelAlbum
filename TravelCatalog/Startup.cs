using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelCatalog.Startup))]
namespace TravelCatalog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

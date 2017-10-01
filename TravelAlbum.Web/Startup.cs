using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelAlbum.Web.Startup))]
namespace TravelAlbum.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

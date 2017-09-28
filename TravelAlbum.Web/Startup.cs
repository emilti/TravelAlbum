using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelAlbum.Startup))]
namespace TravelAlbum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

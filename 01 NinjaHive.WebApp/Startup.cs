using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NinjaHive.WebApp.Startup))]
namespace NinjaHive.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

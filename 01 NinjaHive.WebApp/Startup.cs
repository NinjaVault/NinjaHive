using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NinjaHive.UI.Startup))]
namespace NinjaHive.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

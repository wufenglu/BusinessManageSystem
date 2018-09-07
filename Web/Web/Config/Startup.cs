using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Config.Startup))]
namespace Config
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

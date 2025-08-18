using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reports.net.Sample.WebSite.Startup))]
namespace Reports.net.Sample.WebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

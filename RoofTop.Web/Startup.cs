using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoofTop.Web.Startup))]
namespace RoofTop.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

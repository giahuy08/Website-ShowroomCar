using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoanWeb.Startup))]
namespace DoanWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

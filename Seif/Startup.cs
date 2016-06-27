using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Seif.Startup))]
namespace Seif
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.AspNet.Identity.Owin;
using Owin;
using Seif.Models;

namespace Seif
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
            
        } 
    }
}
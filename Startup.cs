using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatingSite.Startup))]
namespace DatingSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

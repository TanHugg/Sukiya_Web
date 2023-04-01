using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sukiya.Startup))]
namespace Sukiya
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

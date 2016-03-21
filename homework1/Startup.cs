using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(homework1.Startup))]
namespace homework1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

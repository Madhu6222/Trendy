using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trendy.Startup))]
namespace Trendy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

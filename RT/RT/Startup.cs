using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RT.Startup))]
namespace RT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

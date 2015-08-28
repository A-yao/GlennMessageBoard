using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GlennMessageBoard.Startup))]
namespace GlennMessageBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

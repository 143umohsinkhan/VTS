using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VST.Startup))]
namespace VST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

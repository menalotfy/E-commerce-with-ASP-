using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Souq.Startup))]
namespace Souq
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

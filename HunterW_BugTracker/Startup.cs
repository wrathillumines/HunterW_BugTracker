using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HunterW_BugTracker.Startup))]
namespace HunterW_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

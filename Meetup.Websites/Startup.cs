using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Meetup.Websites.Startup))]
namespace Meetup.Websites
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

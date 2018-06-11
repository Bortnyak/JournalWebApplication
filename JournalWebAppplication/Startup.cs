using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JournalWebAppplication.Startup))]
namespace JournalWebAppplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

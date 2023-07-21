using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduZone.Startup))]
namespace EduZone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}

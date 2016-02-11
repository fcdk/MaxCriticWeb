using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CriticWeb.Startup))]
namespace CriticWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
        }
    }
}

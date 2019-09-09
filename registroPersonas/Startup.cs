using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(registroPersonas.Startup))]
namespace registroPersonas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

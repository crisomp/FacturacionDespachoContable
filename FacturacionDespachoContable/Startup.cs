using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FacturacionDespachoContable.Startup))]
namespace FacturacionDespachoContable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

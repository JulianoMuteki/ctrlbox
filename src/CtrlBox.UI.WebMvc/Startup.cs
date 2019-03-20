using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CtrlBox.UI.WebMvc.Startup))]
namespace CtrlBox.UI.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

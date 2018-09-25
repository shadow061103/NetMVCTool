using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetMVCTool.Startup))]
namespace NetMVCTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

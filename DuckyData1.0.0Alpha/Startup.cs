using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DuckyData1._0._0Alpha.Startup))]
namespace DuckyData1._0._0Alpha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

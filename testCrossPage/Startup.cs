using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testCrossPage.Startup))]
namespace testCrossPage
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

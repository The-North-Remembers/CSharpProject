using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectCSharp.Startup))]
namespace ProjectCSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

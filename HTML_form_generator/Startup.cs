using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTML_form_generator.Startup))]
namespace HTML_form_generator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

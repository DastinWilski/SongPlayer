using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SongPlayer.Startup))]
namespace SongPlayer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

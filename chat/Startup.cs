using Microsoft.Owin;
using Owin;
using chat;

namespace chat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using chezzles.api.App_Start;

[assembly: OwinStartup(typeof(chezzles.api.Startup))]

namespace chezzles.api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            NinjectConfig.RegisterNinject(app);
        }
    }
}

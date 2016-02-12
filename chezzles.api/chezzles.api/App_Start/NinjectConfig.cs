using Ninject;
using Owin;

using Ninject.Web.WebApi;
using System.Reflection;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System;
using System.Web.Http;

namespace chezzles.api.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterNinject(IAppBuilder app)
        {
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(new HttpConfiguration());
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
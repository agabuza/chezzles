using Owin;
using System.Reflection;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System.Web.Http;
using Ninject;

namespace chezzles.api.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterNinject(IAppBuilder app)
        {
            app.UseNinjectMiddleware(CreateKernel)
               .UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
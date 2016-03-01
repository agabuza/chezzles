using Nancy;
using Nancy.TinyIoc;
using chezzles.data.Model;
using chezzles.data;
using chezzles.data.EF;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

namespace chezzles.nancy.api.Bootstrapper
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IRepository<GameDTO>, data.EF.Repository<GameDTO>>().AsSingleton();
            container.Register<IDbDataProvider, GamesStoreContext>().AsSingleton();
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                var processors = new[] { typeof(JsonProcessor) };
                return NancyInternalConfiguration.WithOverrides(c => c.ResponseProcessors = processors);
            }
        }
    }
}
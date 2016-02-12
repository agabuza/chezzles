using chezzles.data;
using chezzles.data.EF;
using chezzles.data.Model;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chezzles.api.Configuration
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            Kernel.Bind<IDbDataProvider>().To<GamesStoreContext>();
            Kernel.Bind<IRepository<GameDTO>>().To<data.EF.Repository<GameDTO>>();
        }
    }
}
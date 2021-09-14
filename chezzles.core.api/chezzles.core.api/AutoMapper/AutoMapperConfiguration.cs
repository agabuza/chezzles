using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace chezzles.core.api.AutoMapper
{
    /// <summary>
    /// Class used to initialize AutoMapper mapping profiles.
    /// </summary>
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            var config = new MapperConfiguration(cfg => {
                // cfg.AddProfile<PgnGameMappingProfile>();
            });

            services.AddSingleton<IMapper>(sp => config.CreateMapper());
        }
    }
}

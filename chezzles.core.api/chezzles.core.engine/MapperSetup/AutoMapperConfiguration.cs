using AutoMapper;

namespace chezzles.core.engine.MapperSetup
{
    /// <summary>
    /// Class used to initialize AutoMapper mapping profiles.
    /// </summary>
    public class AutoMapperConfiguration
    {
        static AutoMapperConfiguration()
        {
            Mapper.Initialize(s =>
            {
                s.AddProfile(new PgnGameMappingProfile());
            });
        }
    }
}

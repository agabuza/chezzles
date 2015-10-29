using System.Collections.Generic;
using ilf.pgn;
using AutoMapper;
using chezzles.engine.MapperSetup;

namespace chezzles.engine.Core.Game
{
    public class GameParser
    {
        private AutoMapperConfiguration config = new AutoMapperConfiguration();
        public IEnumerable<Game> Parse(string png)
        {
            var parser = new PgnReader();
            var db = parser.ReadFromString(png);
            return Mapper.Map<IEnumerable<Game>>(db);
        }
    }
}

using System.Collections.Generic;
using ilf.pgn;
using AutoMapper;
using chezzles.engine.MapperSetup;
using System;
using System.Threading.Tasks;
using System.IO;

namespace chezzles.engine.Core.Game
{
    public class GameParser
    {
        private AutoMapperConfiguration config = new AutoMapperConfiguration();
        public IEnumerable<Game> Parse(string png)
        {
            var parser = new PgnReader();
            var db = parser.ReadFromString(png);
            foreach (var game in db.Games)
            {
                yield return Mapper.Map<Game>(game);
            }
        }

        public IEnumerable<Game> ParseStream(Stream stream)
        {
            var parser = new PgnReader();
            var db = parser.ReadFromStream(stream);
            foreach (var game in db.Games)
            {
                yield return Mapper.Map<Game>(game);
            }
        }

        public async Task<IEnumerable<Game>> ParseAsync(string text)
        {
            var parser = new PgnReader();
            var db = await Task.Run(() => { return parser.ReadFromString(text); });
            return Mapper.Map<IEnumerable<Game>>(db);
        }
    }
}

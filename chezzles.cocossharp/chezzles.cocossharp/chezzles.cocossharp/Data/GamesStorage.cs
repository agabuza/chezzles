using chezzles.engine.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Reflection;

namespace chezzles.engine.Data
{
    public class GamesStorage : IGameStorage
    {
        private List<Game> games;

        public GamesStorage()
        {
            this.Init();
        }

        private void Init()
        {
            var assembly = typeof(GamesStorage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("chezzles.cocossharp.Content.puzzles.pgn");
            string text = string.Empty;
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var parser = new GameParser();
            this.games = parser.Parse(text).ToList();
        }

        public IEnumerable<Game> All()
        {
            return this.games;
        }

        public Game Get(int index)
        {
            return this.games[index];
        }

    }
}

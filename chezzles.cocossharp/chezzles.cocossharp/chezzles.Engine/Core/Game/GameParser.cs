using chezzles.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ilf.pgn;
using PngGame = ilf.pgn.Data.Game;
using ilf.pgn.Data;

namespace chezzles.engine.Core.Game
{
    public class GameParser
    {
        public Database Parse(string png)
        {
            var parser = new PgnReader();
            return parser.ReadFromString(png);
        }
    }
}

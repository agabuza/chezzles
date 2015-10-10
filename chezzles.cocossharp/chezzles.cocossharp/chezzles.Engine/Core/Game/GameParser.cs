using chezzles.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ilf.pgn;
using PngGame = ilf.pgn.Data.Game;

namespace chezzles.engine.Core.Game
{
    public class GameParser
    {
        public GameParser()
        {

        }

        public List<PngGame> Parse(string png)
        {
            var parser = new PgnReader();
            var a = parser.ReadFromString(png);

            return a.Games;
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ilf.pgn.Data;
//using ilf.pgn;
using PgnGame = ilf.pgn.Data.Game;
using chezzles.engine.Pieces.Builder;

namespace chezzles.engine.Core.Game
{
    public class Game 
    {
        private Board board;

        public Board Board
        {
            get
            {
                return board;
            }
        }

        public Game(PgnGame game)
        {
            this.board = new Board(game);
        }
    }
}

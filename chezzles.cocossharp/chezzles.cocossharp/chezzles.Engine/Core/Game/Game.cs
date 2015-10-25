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
    public sealed class Game
    {
        private Board board;
        private IPieceBuilder builder = new PieceBuilder();

        public Board Board
        {
            get
            {
                return board;
            }
        }

        internal Game(PgnGame game)
        {
            this.board = new Board();
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    var piece = this.builder.BuildPiece(game.BoardSetup[i, j]);
                    if (piece != null)
                    {
                        piece.Board = this.board;
                        this.board.PutPiece(new Square(i + 1, j + 1), piece);
                    }
                }
        }
    }
}

using chezzles.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PgnPiece = ilf.pgn.Data.Piece;

namespace chezzles.engine.Pieces.Builder
{
    public interface IPieceBuilder
    {
        Piece Build(string piece);

        Piece BuildPiece(PgnPiece piece);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chezzles.engine.Core;
using ilf.pgn.Data;

namespace chezzles.engine.Pieces.Builder
{
    class PieceBuilder : IPieceBuilder
    {
        public Core.Piece BuildPiece(ilf.pgn.Data.Piece piece)
        {
            if (piece == null)
            {
                return null;
            }

            var color = piece.Color == Color.White ? PieceColor.White : PieceColor.Black;
            switch (piece.PieceType)
            {
                case ilf.pgn.Data.PieceType.King:
                    return new King(color);
                case ilf.pgn.Data.PieceType.Queen:
                    return new Queen(color);
                case ilf.pgn.Data.PieceType.Rook:
                    return new Rook(color);
                case ilf.pgn.Data.PieceType.Bishop:
                    return new Bishop(color);
                case ilf.pgn.Data.PieceType.Knight:
                    return new Knight(color);
                case ilf.pgn.Data.PieceType.Pawn:
                    return new Pawn(color);
            }

            return null;
        }

        public Core.Piece Build(string piece)
        {
            throw new NotImplementedException();
        }
    }
}

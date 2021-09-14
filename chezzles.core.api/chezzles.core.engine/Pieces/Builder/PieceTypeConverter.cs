using System;
using AutoMapper;
using chezzles.core.engine.Core;
using Pgn = ilf.pgn.Data;
namespace chezzles.core.engine.Pieces.Builder
{
    public class PieceTypeConverter : ITypeConverter <Pgn.Piece, Core.Piece>
    {
        private static IPieceBuilder builder = new PieceBuilder();
        public Piece Convert(Pgn.Piece source, Core.Piece destination, ResolutionContext context)
        {
            return builder.BuildPiece(source);
        }
    }
}

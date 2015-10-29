using System;
using AutoMapper;
using chezzles.engine.Core;
using Pgn = ilf.pgn.Data;
namespace chezzles.engine.Pieces.Builder
{
    public class PieceTypeConverter : ITypeConverter<Pgn.Piece, Core.Piece>
    {
        private static IPieceBuilder builder = new PieceBuilder();
        public Piece Convert(ResolutionContext context)
        {
            return builder.BuildPiece(context.SourceValue as Pgn.Piece);
        }
    }
}

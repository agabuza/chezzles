using chezzles.engine.Core;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.cocossharp.Pieces
{
    public class CocoPieceBuilder
    {
        private CCSpriteFrameCache cache;

        public CocoPieceBuilder()
        {
            this.cache = CCSpriteFrameCache.SharedSpriteFrameCache;
            this.cache.AddSpriteFrames("hd/sprites.plist");
        }

        public CocoPiece Build(Piece piece)
        {
            var strColor = piece.Color == PieceColor.White ? "w" : "b";
            var spriteName = strColor + "_" + Enum.GetName(typeof(PieceType), piece.Type).ToLower() + ".png";
            var cocoPiece = new CocoPiece(piece, cache[spriteName]);
            return cocoPiece;
        }

    }
}

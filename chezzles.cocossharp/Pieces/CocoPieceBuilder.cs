using chezzles.cocossharp.Common;
using chezzles.cocossharp.Pieces.Model;
using chezzles.engine.Core;
using CocosSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace chezzles.cocossharp.Pieces
{
    public class CocoPieceBuilder
    {
        private CCSpriteFrameCache cache;
        private ISetttingsProvider settings;

        public CocoPieceBuilder()
        {
            this.settings = DependencyService.Get<ISetttingsProvider>(DependencyFetchTarget.GlobalInstance);
            this.cache = CCSpriteFrameCache.SharedSpriteFrameCache;
            var set = settings["chess-set"];
            var chessSet = JsonConvert.DeserializeObject<ChessSet>(set);
            if (chessSet != null && !string.IsNullOrEmpty(chessSet.FilePath))
            {
                this.cache.AddSpriteFrames(chessSet.FilePath);
            }
            else
            {
                // default value
                this.cache.AddSpriteFrames("hd/sprites.plist");
            }
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

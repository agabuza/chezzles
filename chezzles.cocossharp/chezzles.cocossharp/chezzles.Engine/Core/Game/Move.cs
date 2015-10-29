using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.engine.Core.Game
{
    public class Move
    {
        public Square OriginalSquare { get; set; }
        public Square TargetSquare { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
        public PieceType PromotedPiece { get; set; }
    }
}

using chezzles.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Square square, Board board, PieceColor color)
            : base(square, board, color)
        {
        }

        public override PieceType Type
        {
            get
            {
                return PieceType.Bishop;
            }
        }

        protected override IEnumerable<Tuple<int, int>> GetOffsets()
        {
            var offsets = new List<Tuple<int, int>>();
            offsets.Add(new Tuple<int, int>(1, 1));
            offsets.Add(new Tuple<int, int>(-1, 1));
            offsets.Add(new Tuple<int, int>(1, -1));
            offsets.Add(new Tuple<int, int>(-1, -1));

            return offsets;
        }
    }
}

using chezzles.core.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.core.engine.Pieces
{
    public class Rook : Piece
    {
        public Rook(PieceColor color)
        {
            this.color = color;
        }

        public Rook(Square position, Board board, PieceColor color)
            : base(position, board, color)
        {
        }

        public override PieceType Type
        {
            get
            {
                return PieceType.Rook;
            }
        }

        protected override IEnumerable<Tuple<int, int>> GetOffsets()
        {
            var offsets = new List<Tuple<int, int>>();
            offsets.Add(new Tuple<int, int>(0, 1));
            offsets.Add(new Tuple<int, int>(1, 0));
            offsets.Add(new Tuple<int, int>(0, -1));
            offsets.Add(new Tuple<int, int>(-1, 0));

            return offsets;
        }
    }
}

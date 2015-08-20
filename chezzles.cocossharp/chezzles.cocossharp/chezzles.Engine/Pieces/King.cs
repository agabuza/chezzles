using chezzles.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Pieces
{
    public class King : Piece
    {
        public King(Square square, Board board, PieceColor color)
            : base(square, board, color)
        {
        }

        public override PieceType Type
        {
            get
            {
                return PieceType.King;
            }
        }

        protected override IEnumerable<Tuple<int, int>> GetOffsets()
        {
            var offsets = new List<Tuple<int, int>>();
            offsets.Add(new Tuple<int, int>(0, 1));
            offsets.Add(new Tuple<int, int>(1, 0));
            offsets.Add(new Tuple<int, int>(1, 1));
            offsets.Add(new Tuple<int, int>(-1, -1));
            offsets.Add(new Tuple<int, int>(0, -1));
            offsets.Add(new Tuple<int, int>(-1, 0));
            offsets.Add(new Tuple<int, int>(-1, 1));
            offsets.Add(new Tuple<int, int>(1, -1));

            return offsets;
        }

        protected override int MaxRange
        {
            get
            {
                return 1;
            }
        }
    }
}

using chezzles.core.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.core.engine.Pieces
{
    public class King : Piece
    {
        public King(PieceColor color)
        {
            this.color = color;
        }

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

        public override bool CanMoveTo(Square square)
        {
            var moves = base.PossibleMoves().ToList();
            var beatenSquares = this.board.Pieces.Where(x => x.Color != this.Color)
                                    .SelectMany(p => p.BeatenSquares()).ToList();

            return moves.Except(beatenSquares).Any(x => x.Equals(square));
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

using chezzles.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor color)
        {
            this.color = color;
        }

        public Pawn(Square square, Board board, PieceColor color)
            : base(square, board, color)
        {
        }

        public override PieceType Type
        {
            get
            {
                return PieceType.Pawn;
            }
        }

        protected override int MaxRange
        {
            get
            {
                var secondRank = this.board.IsBottomUpDirection ? 2 : 7;
                var sevensRank = this.board.IsBottomUpDirection ? 7 : 2;
                switch (this.Color)
                {
                    case PieceColor.White:
                        return this.position.YPosition == secondRank ? 2 : 1;
                    case PieceColor.Black:
                        return this.position.YPosition == sevensRank ? 2 : 1;
                    default:
                        return 1;
                }
            }
        }

        protected override IEnumerable<Tuple<int, int>> GetOffsets()
        {
            var offsets = new List<Tuple<int, int>>();
            var whiteVerticalOffset = this.board.IsBottomUpDirection ? 1 : -1;
            var blackVerticalOffset = this.board.IsBottomUpDirection ? -1 : 1;

            switch (this.Color)
            {
                case PieceColor.White:
                    offsets.Add(new Tuple<int, int>(0, whiteVerticalOffset));
                    break;
                case PieceColor.Black:
                    offsets.Add(new Tuple<int, int>(0, blackVerticalOffset));
                    break;
                default:
                    break;
            }

            return offsets;
        }
    }
}

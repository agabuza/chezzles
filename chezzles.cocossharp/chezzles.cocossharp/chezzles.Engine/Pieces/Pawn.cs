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
                switch (this.Color)
                {
                    case PieceColor.White:
                        return this.position.YPosition == 2 ? 2 : 1;
                    case PieceColor.Black:
                        return this.position.YPosition == 7 ? 2 : 1;
                    default:
                        return 1;
                }
            }
        }

        protected override IEnumerable<Tuple<int, int>> GetOffsets()
        {
            var offsets = new List<Tuple<int, int>>();

            switch (this.Color)
            {
                case PieceColor.White:
                    offsets.Add(new Tuple<int, int>(0, 1));
                    break;
                case PieceColor.Black:
                    offsets.Add(new Tuple<int, int>(0, -1));
                    break;
                default:
                    break;
            }

            return offsets;
        }
    }
}

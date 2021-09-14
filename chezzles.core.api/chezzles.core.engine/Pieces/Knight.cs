using chezzles.core.engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.core.engine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Square square, Board board, PieceColor color)
            : base(square, board, color)
        {
        }

        public Knight(PieceColor color)
        {
            this.color = color;
        }

        public override PieceType Type
        {
            get
            {
                return PieceType.Knight;
            }
        }

        protected override IEnumerable<Tuple<int, int>> GetOffsets()
        {
            var offset = new List<Tuple<int, int>>();
            offset.Add(new Tuple<int, int>(2, -1));
            offset.Add(new Tuple<int, int>(2, 1));
            offset.Add(new Tuple<int, int>(-2, 1));
            offset.Add(new Tuple<int, int>(-2, -1));
            offset.Add(new Tuple<int, int>(1, -2));
            offset.Add(new Tuple<int, int>(-1, -2));
            offset.Add(new Tuple<int, int>(1, 2));
            offset.Add(new Tuple<int, int>(-1, 2));

            return offset;
        }

        public override IEnumerable<Square> PossibleMoves()
        {
            var squares = GetOffsets().Select(x => new Square(this.position.XPosition + x.Item1, this.position.YPosition + x.Item2));

            return squares.Where(sq => this.board.Squares.ContainsKey(sq) &&
                    (this.board.Squares[sq] == null || this.board.Squares[sq].Color != this.Color));
        }


    }
}

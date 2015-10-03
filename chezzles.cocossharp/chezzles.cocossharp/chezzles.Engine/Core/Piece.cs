using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Core
{
    public abstract class Piece
    {
        protected Square position;
        protected Board board;
        private PieceColor color;

        public Piece(Square position, Board board, PieceColor color)
        {
            this.position = position;
            this.board = board;
            this.color = color;

            this.board.Squares[position] = this;
        }

        public abstract PieceType Type { get; }

        public PieceColor Color
        {
            get { return color; }
        }

        public Board Board
        {
            get { return board; }
        }

        protected virtual int MaxRange
        {
            get { return 8; }
        }

        protected abstract IEnumerable<Tuple<int, int>> GetOffsets();

        public virtual IEnumerable<Square> PossibleMoves()
        {
            foreach (var offset in GetOffsets())
            {
                for (int i = 1; i <= this.MaxRange; i++)
                {
                    var square = new Square(this.position.XPosition + offset.Item1 * i, this.position.YPosition + offset.Item2 * i);
                    if (this.IsEmptySquare(square))
                    {
                        yield return square;
                        if (this.IsOpponentsPiece(square)) break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public Square GetPosition()
        {
            return this.position;
        }

        public bool MoveTo(Square square)
        {
            if (this.CanMoveTo(square))
            {
                var oldPosition = this.position;
                this.position = square;

                this.board.Squares[oldPosition] = null;
                this.board.Squares[this.position] = this;
                return true;
            }

            return false;
        }

        public virtual bool CanMoveTo(Square square)
        {
            return this.PossibleMoves().Any(x => x.Equals(square));
        }
        
        protected bool IsEmptySquare(Square sq)
        {
            return this.board.Squares.ContainsKey(sq) &&
                    (this.board.Squares[sq] == null || this.board.Squares[sq].Color != this.Color);
        }

        protected bool IsOpponentsPiece(Square sq)
        {
            return this.board.Squares.ContainsKey(sq) &&
                   (this.board.Squares[sq] != null && this.board.Squares[sq].Color != this.Color);
        }
    }
}

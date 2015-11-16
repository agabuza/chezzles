using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Core
{
    public delegate void PieceTakenEventHandler (object sender);

    public abstract class Piece
    {
        protected Square position;
        protected Board board;
        protected PieceColor color;

        public Piece(Square position, Board board, PieceColor color)
        {
            this.position = position;
            this.board = board;
            this.color = color;

            this.board.Squares[position] = this;
        }        

        public Piece()
        {
        }

        public abstract PieceType Type { get; }

        public PieceColor Color
        {
            get { return color; }
        }

        public Square Position
        {
            get { return this.position; }
            internal set { this.position = value; }
        }

        public Board Board
        {
            get { return board; }
            internal set { this.board = value; }
        }        

        protected virtual int MaxRange
        {
            get { return 8; }
        }

        public event PieceTakenEventHandler PieceTaken;

        protected abstract IEnumerable<Tuple<int, int>> GetOffsets();

        internal void TakePiece()
        {
            this.board.Squares[this.position] = null;
            if (this.PieceTaken != null)
            {
                PieceTaken(this);
            }
        }

        public virtual IEnumerable<Square> PossibleMoves()
        {
            foreach (var offset in GetOffsets())
            {
                for (int i = 1; i <= this.MaxRange; i++)
                {
                    var square = new Square(this.position.XPosition + offset.Item1 * i, this.position.YPosition + offset.Item2 * i);
                    if (this.IsEmptySquare(square) || this.IsOpponentsPiece(square))
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

        public bool MoveTo(Square square)
        {
            if (this.CanMoveTo(square) && this.CanColorMove())
            {
                this.board.PutPiece(square, this);
                return true;
            }

            return false;
        }

        internal bool CanColorMove()
        {
            return this.Color == PieceColor.White ? this.board.IsWhiteMove : !this.board.IsWhiteMove;
        }

        public virtual bool CanMoveTo(Square square)
        {
            return this.PossibleMoves().Any(x => x.Equals(square));
        }
        
        protected bool IsEmptySquare(Square sq)
        {
            return this.board.Squares.ContainsKey(sq) && this.board.Squares[sq] == null;
        }

        protected bool IsOpponentsPiece(Square sq)
        {
            return this.board.Squares.ContainsKey(sq) &&
                   (this.board.Squares[sq] != null && this.board.Squares[sq].Color != this.Color);
        }
    }
}

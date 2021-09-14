using chezzles.core.engine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Core
{
    public delegate void PieceTakenEventHandler (object sender);
    public delegate void PositionUpatedEventHandler(object sender, Square position);

    public abstract class Piece
    {
        protected Square position;
        protected Board board;
        protected PieceColor color;

        public event PieceTakenEventHandler PieceTaken;
        public event PositionUpatedEventHandler PositionUpdated;

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
            internal set
            {
                this.position = value;
                if (this.PositionUpdated != null)
                {
                    this.PositionUpdated(this, this.position);
                }
            }
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

        protected abstract IEnumerable<Tuple<int, int>> GetOffsets();

        internal void TakePiece()
        {
            this.board.Squares[this.position] = null;
            if (this.PieceTaken != null)
            {
                PieceTaken(this);
            }
        }

        public virtual IEnumerable<Square> BeatenSquares()
        {
            foreach (var offset in GetOffsets())
            {
                for (int i = 1; i <= this.MaxRange; i++)
                {
                    var square = new Square(this.position.XPosition + offset.Item1 * i, this.position.YPosition + offset.Item2 * i);

                    yield return square;
                    if (!this.IsEmptySquare(square) &&
                        !(this.board.Squares.ContainsKey(square) && this.board.Squares[square] is King))
                    {
                        break;
                    }
                }
            }
        }

        public virtual IEnumerable<Square> PossibleMoves()
        {
            return BeatenSquares().Where(x => IsEmptySquare(x) || IsOpponentsPiece(x));
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

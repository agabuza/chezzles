using chezzles.engine.Core.Game;
using chezzles.engine.Pieces.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PgnGame = ilf.pgn.Data.Game;

namespace chezzles.engine.Core
{
    public delegate void PieceMovedEventHandler(Board board, Move move);

    public sealed class Board
    {
        private Dictionary<Square, Piece> squares;
        private float size;

        public bool IsWhiteMove { get; set; }
        public bool IsBottomUpDirection { get; internal set; }

        public event PieceMovedEventHandler PieceMoved;

        public Board()
        {
            InitProperties();
            PopulateSquares(400);
        }

        public Board(float size)
        {
            InitProperties();
            PopulateSquares(size);
        }
        public Board(Dictionary<Square, Piece> squares)
        {
            InitProperties();
            this.squares = squares;
            this.size = 400;
        }

        internal void MakeMove(Move nextMove)
        {
            var pieceToMove = this.Pieces.FirstOrDefault(x =>
                    x.Type == nextMove.TargetPiece &&
                    x.Color == (this.IsWhiteMove ? PieceColor.White : PieceColor.Black) &&
                    x.PossibleMoves().Contains(nextMove.TargetSquare));

            this.PutPiece(nextMove.TargetSquare, pieceToMove, false);
        }

        private void InitProperties()
        {
            this.IsBottomUpDirection = true;
            this.IsWhiteMove = true;
        }

        private void PopulateSquares(float size)
        {
            this.size = size;
            this.squares = new Dictionary<Square, Piece>();
            for (int i = 1; i <= 8; i++)
                for (int j = 1; j <= 8; j++)
                {
                    this.squares.Add(new Square(i, j), null);
                }
        }

        public float Size
        {
            get { return this.size; }
        }

        public Dictionary<Square, Piece> Squares
        {
            get { return squares; }
            set { squares = value; }
        }

        public IEnumerable<Piece> Pieces
        {
            get
            {
                return this.squares
                    .Where(x => x.Value != null)
                    .Select(x => x.Value);
            }
        }

        public void PutPiece(Square square, Piece piece, bool notifyPieceMoved = true)
        {
            if (this.squares[square] != null)
            {
                this.squares[square].TakePiece();
            }

            // Put piece in a new position
            var oldPosition = piece.Position;
            this.squares[oldPosition] = null;
            this.squares[square] = piece;
            piece.Position = square;

            // Now it's opposite color to move
            this.IsWhiteMove = !this.IsWhiteMove;

            if (notifyPieceMoved)
            {
                this.FirePieceMoved(oldPosition, piece);
            }
        }

        private void FirePieceMoved(Square oldPosition, Piece piece)
        {
            if (this.PieceMoved != null)
            {
                var move = new Move()
                {
                    OriginalSquare = oldPosition,
                    TargetSquare = piece.Position,
                    TargetPiece = piece.Type
                };

                this.PieceMoved(this, move);
            }
        }

        public bool GameFinished()
        {
            return false;
        }

        public bool IsEmpty(Square square)
        {
            return this.Squares.ContainsKey(square) && this.Squares[square] == null;
        }

        public void SetSize(float width)
        {
            this.size = width;
        }
    }
}

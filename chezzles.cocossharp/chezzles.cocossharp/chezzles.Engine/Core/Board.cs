using chezzles.engine.Pieces.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PgnGame = ilf.pgn.Data.Game;

namespace chezzles.engine.Core
{
    public sealed class Board
    {
        private Dictionary<Square, Piece> squares;
        private float size;

        public bool IsWhiteMove { get; set; }
        public bool IsBottomUpDirection { get; internal set; }

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

        public void PutPiece(Square square, Piece piece)
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

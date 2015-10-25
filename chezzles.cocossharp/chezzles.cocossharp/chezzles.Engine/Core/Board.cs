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

        public Board()
            : this(400)
        {
        }

        public Board(float size)
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

        public Board(Dictionary<Square, Piece> squares)
        {
            this.squares = squares;
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

            var oldPosition = piece.Position;
            this.squares[oldPosition] = null;
            this.squares[square] = piece;
            piece.Position = square;
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

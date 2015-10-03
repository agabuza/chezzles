using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Core
{
    public class Board
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

        public void PutPiece(Square square, Piece piece)
        {
            this.squares[square] = piece;
        }

        public bool GameFinished()
        {
            return false;
        }

        public bool IsEmpty(Square square)
        {
            return this.Squares.ContainsKey(square) && this.Squares[square] == null;
        }
    }
}

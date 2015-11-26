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
            var whiteVerticalOffset = 1;
            var blackVerticalOffset = -1;

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

        public override IEnumerable<Square> BeatenSquares()
        {
            var moves = new List<Square>();

            var whiteVerticalOffset = 1;
            var blackVerticalOffset = -1;

            switch (this.Color)
            {
                case PieceColor.White:
                    AddTakes(moves, whiteVerticalOffset);
                    break;
                case PieceColor.Black:
                    AddTakes(moves, blackVerticalOffset);
                    break;
                default:
                    break;
            }

            return moves;
        }

        public override IEnumerable<Square> PossibleMoves()
        {
            // WARNING: CRAZY... It's pawn, baby...
            var moves = base.BeatenSquares().ToList();
            moves.RemoveAll(x => !IsEmptySquare(x));
            moves.AddRange(this.BeatenSquares().Where(IsOpponentsPiece));

            return moves;
        }

        /// <summary>
        /// Method to add max two possible takes of pawn (left&right)
        /// </summary>
        private void AddTakes(List<Square> moves, int verticalOffset)
        {
            var right = new Square(this.position.XPosition + 1, this.position.YPosition + verticalOffset);
            moves.Add(right);

            var left = new Square(this.position.XPosition - 1, this.position.YPosition + verticalOffset);
            moves.Add(left);
        }
    }
}

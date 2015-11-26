using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.engine.Core.Game
{
    public class Move
    {
        public Square OriginalSquare { get; set; }
        public Square TargetSquare { get; set; }
        public bool? IsCheck { get; set; }
        public bool? IsCheckMate { get; set; }
        public PieceType? PromotedPiece { get; set; }
        public PieceType? TargetPiece { get; set; }
        public PieceType Piece { get; set; }
        public PieceColor Color { get; internal set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Move p = obj as Move;
            if ((object)p == null)
            {
                return false;
            }

            return (TargetSquare == p.TargetSquare) && (Piece == p.Piece);
        }

        public bool Equals(Move p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (TargetSquare == p.TargetSquare) && (Piece == p.Piece);
        }

        public override int GetHashCode()
        {
            return TargetSquare.XPosition ^ TargetSquare.YPosition;
        }

        public static bool operator ==(Move a, Move b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.TargetSquare == b.TargetSquare && a.Piece == b.Piece;
        }

        public static bool operator !=(Move a, Move b)
        {
            return !(a == b);
        }
    }
}

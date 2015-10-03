using chezzles.engine.Core;
using chezzles.engine.Pieces;
using NUnit.Framework;
using System.Linq;

namespace chezzles.Engine.Tests.Pieces
{
    [TestFixture]
    public class RookTests
    {
        [Test]
        public void Whether_Rook_ReturnsCorrectMoves_On_PossibleMoves()
        {
            var board = new Board();
            var rook = new Rook(new Square(4, 4), board, PieceColor.White);

            var possibleMoves = rook.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 14);
        }

        [Test]
        public void Whether_Rook_CantMoveToIncorrectSquare_On_CanMoveTo()
        {
            var board = new Board();
            var rook = new Rook(new Square(4, 4), board, PieceColor.White);

            Assert.That(rook.CanMoveTo(new Square(5, 5)), Is.False);
        }

        [TestCase(1, 0)]
        [TestCase(3, 0)]
        [TestCase(0, 2)]
        [TestCase(0, -3)]
        [TestCase(-1, 0)]
        public void Whether_Rook_CanMoveToPossibleSquare_On_CanMoveTo(int offsetX, int offsetY)
        {
            var board = new Board();
            var rook = new Rook(new Square(4, 4), board, PieceColor.White);

            Assert.That(rook.CanMoveTo(
                new Square(rook.GetPosition().XPosition + offsetX,
                           rook.GetPosition().YPosition + offsetY)));
        }

        [Test]
        public void Whether_Rook_CantMoveToOccupiedSquare_On_CanMoveTo()
        {
            var board = new Board();
            var rook = new Rook(new Square(1, 1), board, PieceColor.White);
            var anotherKnight = new Knight(new Square(1, 6), board, PieceColor.White);

            Assert.That(rook.CanMoveTo(new Square(1, 6)), Is.False);
        }

        [Test]
        public void Whether_Rook_CantMoveOutsideTheBoard_On_CanMoveTo()
        {
            var board = new Board();
            var rook = new Rook(new Square(1, 1), board, PieceColor.White);

            Assert.That(rook.PossibleMoves().Count() == 14);
            Assert.That(rook.CanMoveTo(new Square(-1, 1)), Is.False);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void Whether_Rook_CantJumpOverAnotherPiece_On_CanMoveTo(PieceColor pieceColor)
        {
            var board = new Board();
            var rook = new Rook(new Square(4, 4), board, PieceColor.White);
            var knight = new Knight(new Square(4, 5), board, pieceColor);

            Assert.That(rook.CanMoveTo(new Square(4, 6)), Is.False);
        }
    }
}

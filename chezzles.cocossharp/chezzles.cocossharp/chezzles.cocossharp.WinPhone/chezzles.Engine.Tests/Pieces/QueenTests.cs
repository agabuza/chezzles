using chezzles.engine.Core;
using chezzles.engine.Pieces;
using NUnit.Framework;
using System.Linq;

namespace chezzles.Engine.Tests.Pieces
{
    [TestFixture]
    public class QueenTests
    {
        [Test]
        public void Whether_Queen_ReturnsCorrectMoves_On_PossibleMoves()
        {
            var board = new Board();
            var queen = new Queen(new Square(4, 4), board, PieceColor.White);

            var possibleMoves = queen.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 27);
        }

        [Test]
        public void Whether_Queen_CantMoveToIncorrectSquare_On_CanMoveTo()
        {
            var board = new Board();
            var queen = new Queen(new Square(4, 4), board, PieceColor.White);

            Assert.That(queen.CanMoveTo(new Square(6, 3)), Is.False);
        }

        [TestCase(1, 1)]
        [TestCase(4, 1)]
        [TestCase(3, 4)]
        [TestCase(8, 8)]
        public void Whether_Queen_CanMoveToPossibleSquare_On_CanMoveTo(int offsetX, int offsetY)
        {
            var board = new Board();
            var queen = new Queen(new Square(4, 4), board, PieceColor.White);

            Assert.That(queen.CanMoveTo(
                new Square(queen.GetPosition().XPosition + offsetX,
                           queen.GetPosition().YPosition + offsetY)));
        }

        [Test]
        public void Whether_Queen_CantMoveToOccupiedSquare_On_CanMoveTo()
        {
            var board = new Board();
            var queen = new Queen(new Square(4, 4), board, PieceColor.White);
            var anotherKnight = new Knight(new Square(6, 6), board, PieceColor.White);

            Assert.That(queen.CanMoveTo(new Square(6, 6)), Is.False);
        }

        [Test]
        public void Whether_Queen_CantMoveOutsideTheBoard_On_CanMoveTo()
        {
            var board = new Board();
            var queen = new Queen(new Square(1, 1), board, PieceColor.White);

            Assert.That(queen.PossibleMoves().Count() == 21);
            Assert.That(queen.CanMoveTo(new Square(-1, -1)), Is.False);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void Whether_Queen_CantJumpOverAnotherPiece_On_CanMoveTo(PieceColor pieceColor)
        {
            var board = new Board();
            var queen = new Queen(new Square(4, 4), board, PieceColor.White);
            var knight = new Knight(new Square(5, 5), board, pieceColor);

            Assert.That(queen.CanMoveTo(new Square(6, 6)), Is.False);
        }

        [Test]
        public void Whether_Queen_CanGrabOpponentsPiece_On_CanMoveTo()
        {
            var board = new Board();
            var queen = new Queen(new Square(4, 4), board, PieceColor.White);
            var knight = new Knight(new Square(5, 5), board, PieceColor.Black);

            Assert.That(queen.CanMoveTo(new Square(5, 5)), Is.True);
        }
    }
}

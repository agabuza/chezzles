using chezzles.engine.Core;
using chezzles.engine.Pieces;
using NUnit.Framework;
using System.Linq;

namespace chezzles.Engine.Tests.Pieces
{
    [TestFixture]
    public class KnightTests
    {
        [Test]
        public void Whether_Knight_ReturnsCorrectPossibleSquares_On_PossibleMoves()
        {
            var board = new Board();
            var knight = new Knight(new Square(4, 4), board, PieceColor.White);

            var possibleMoves = knight.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 8);
        }

        [Test]
        public void Whether_Knight_CantMoveToIncorrectSquare_On_CanMoveTo()
        {
            var board = new Board();
            var knight = new Knight(new Square(4, 4), board, PieceColor.White);

            Assert.That(knight.CanMoveTo(new Square(5, 5)), Is.False);
        }

        [TestCase(1, 2)]
        [TestCase(-2, 1)]
        [TestCase(-1, -2)]
        [TestCase(2, -1)]
        public void Whether_Knight_CanMoveToPossibleSquare_On_CanMoveTo(int offsetX, int offsetY)
        {
            var board = new Board();
            var knight = new Knight(new Square(4, 4), board, PieceColor.White);

            Assert.That(knight.CanMoveTo(
                new Square(knight.GetPosition().XPosition + offsetX,
                           knight.GetPosition().YPosition + offsetY)));
        }

        [Test]
        public void Whether_Knight_CantMoveToOccupiedSquare_On_CanMoveTo()
        {
            var board = new Board();
            var knight = new Knight(new Square(4, 4), board, PieceColor.White);
            var anotherKnight = new Knight(new Square(5, 6), board, PieceColor.White);

            Assert.That(knight.CanMoveTo(new Square(5, 6)), Is.False);
        }

        [Test]
        public void Whether_Knight_CantMoveOutsideTheBoard_On_CanMoveTo()
        {
            var board = new Board();
            var knight = new Knight(new Square(1, 1), board, PieceColor.White);

            Assert.That(knight.PossibleMoves().Count() == 2);
            Assert.That(knight.CanMoveTo(new Square(-1, 2)), Is.False);
        }
    }
}

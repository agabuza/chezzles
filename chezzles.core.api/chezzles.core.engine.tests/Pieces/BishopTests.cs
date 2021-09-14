using chezzles.core.engine.Core;
using chezzles.core.engine.Pieces;
using NUnit.Framework;
using System.Linq;

namespace chezzles.core.engine.Tests.Pieces
{
    [TestFixture]
    public class BishopTests
    {
        [Test]
        public void Whether_Bishop_ReturnsCorrectMoves_On_PossibleMoves()
        {
            var board = new Board();
            var bishop = new Bishop(new Square(4, 4), board, PieceColor.White);

            var possibleMoves = bishop.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 13);
        }

        [Test]
        public void Whether_Bishop_CantMoveToIncorrectSquare_On_CanMoveTo()
        {
            var board = new Board();
            var bishop = new Bishop(new Square(4, 4), board, PieceColor.White);

            Assert.That(bishop.CanMoveTo(new Square(6, 5)), Is.False);
        }

        [TestCase(1, 1)]
        [TestCase(-2, -2)]
        [TestCase(1, -1)]
        [TestCase(-3, 3)]
        public void Whether_Bishop_CanMoveToPossibleSquare_On_CanMoveTo(int offsetX, int offsetY)
        {
            var board = new Board();
            var bishop = new Bishop(new Square(4, 4), board, PieceColor.White);

            Assert.That(bishop.CanMoveTo(
                new Square(bishop.Position.XPosition + offsetX,
                           bishop.Position.YPosition + offsetY)));
        }

        [Test]
        public void Whether_Bishop_CantMoveToOccupiedSquare_On_CanMoveTo()
        {
            var board = new Board();
            var bishop = new Bishop(new Square(4, 4), board, PieceColor.White);
            var anotherKnight = new Knight(new Square(6, 6), board, PieceColor.White);

            Assert.That(bishop.CanMoveTo(new Square(6, 6)), Is.False);
        }

        [Test]
        public void Whether_Bishop_CantMoveOutsideTheBoard_On_CanMoveTo()
        {
            var board = new Board();
            var bishop = new Bishop(new Square(1, 1), board, PieceColor.White);

            Assert.That(bishop.PossibleMoves().Count() == 7);
            Assert.That(bishop.CanMoveTo(new Square(-1, -1)), Is.False);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void Whether_Bishop_CantJumpOverAnotherPiece_On_CanMoveTo(PieceColor pieceColor)
        {
            var board = new Board();
            var bishop = new Bishop(new Square(4, 4), board, PieceColor.White);
            var knight = new Knight(new Square(5, 5), board, pieceColor);

            Assert.That(bishop.CanMoveTo(new Square(6, 6)), Is.False);
        }
    }
}

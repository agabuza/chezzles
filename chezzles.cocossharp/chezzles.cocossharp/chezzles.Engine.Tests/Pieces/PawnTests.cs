using chezzles.engine.Core;
using chezzles.engine.Pieces;
using NUnit.Framework;
using System.Linq;

namespace chezzles.Engine.Tests.Pieces
{
    [TestFixture]
    public class PawnTests
    {
        [Test]
        public void Whether_Pawn_ReturnsCorrectMoves_On_PossibleMoves()
        {
            var board = new Board();
            var pawn = new Pawn(new Square(4, 4), board, PieceColor.White);

            var possibleMoves = pawn.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 1);
        }

        [TestCase(PieceColor.White, 2, true)]
        [TestCase(PieceColor.Black, 7, true)]
        [TestCase(PieceColor.White, 7, false)]
        [TestCase(PieceColor.Black, 2, false)]
        public void Whether_Pawn_CanJump2SquaresFromInitialPosition_On_PossibleMoves(PieceColor color, int YPosition, bool boardDirection)
        {
            var board = new Board();
            board.IsBottomUpDirection = boardDirection;
            var pawn = new Pawn(new Square(4, YPosition), board, color);

            var possibleMoves = pawn.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 2);
        }

        [Test]
        public void Whether_Pawn_CantMoveToIncorrectSquare_On_CanMoveTo()
        {
            var board = new Board();
            var pawn = new Pawn(new Square(4, 4), board, PieceColor.White);

            Assert.That(pawn.CanMoveTo(new Square(6, 5)), Is.False);
        }

        [TestCase(PieceColor.White, 1)]
        [TestCase(PieceColor.Black, -1)]
        public void Whether_Pawn_CanMoveToPossibleSquare_On_CanMoveTo(PieceColor color, int offsetY)
        {
            var board = new Board();
            var pawn = new Pawn(new Square(4, 4), board, color);

            Assert.That(pawn.CanMoveTo(
                new Square(pawn.Position.XPosition,
                           pawn.Position.YPosition + offsetY)));
        }

        [TestCase(PieceColor.White, -1)]
        [TestCase(PieceColor.Black, 1)]
        public void Whether_Pawn_CantMoveBack_On_CanMoveTo(PieceColor color, int offsetY)
        {
            var board = new Board();
            var pawn = new Pawn(new Square(4, 4), board, color);

            Assert.That(pawn.CanMoveTo(
                new Square(pawn.Position.XPosition,
                           pawn.Position.YPosition + offsetY)), Is.False);
        }

        [Test]
        public void Whether_Pawn_CantMoveToOccupiedSquare_On_CanMoveTo()
        {
            var board = new Board();
            var pawn = new Pawn(new Square(4, 4), board, PieceColor.White);
            var anotherKnight = new Knight(new Square(5, 5), board, PieceColor.White);

            Assert.That(pawn.CanMoveTo(new Square(5, 5)), Is.False);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void Whether_Pawn_CantJumpOverAnotherPiece_On_CanMoveTo(PieceColor pieceColor)
        {
            var board = new Board();
            var pawn = new Pawn(new Square(1, 2), board, PieceColor.White);
            var knight = new Knight(new Square(1, 3), board, pieceColor);

            Assert.That(pawn.CanMoveTo(new Square(1, 4)), Is.False);
        }

        [Test]
        public void Whether_Pawn_JumpsInCorrectDirection_OnCanMoveTo()
        {
            var board = new Board();
            board.IsBottomUpDirection = false;
            var pawn = new Pawn(new Square(1, 7), board, PieceColor.White);

            Assert.That(pawn.CanMoveTo(new Square(1, 5)), Is.True);
        }

        [Test]
        public void Whether_Pawn_CantJumpInOppositeDirection_OnCanMoveTo()
        {
            var board = new Board();
            board.IsBottomUpDirection = false;
            var pawn = new Pawn(new Square(1, 7), board, PieceColor.Black);

            Assert.That(pawn.CanMoveTo(new Square(1, 5)), Is.False);
        }

        [Test]
        public void Whether_Pawn_CanTakeTakeOpponentsPiece_On_CanMoveTo()
        {
            var board = new Board();
            var pawn = new Pawn(new Square(1, 2), board, PieceColor.White);
            var knight = new Knight(new Square(2, 3), board, PieceColor.Black);

            Assert.That(pawn.CanMoveTo(new Square(2, 3)), Is.True);
        }

        [Test]
        public void Whether_Pawn_CantMoveWhenBlocekdByAnotherPiece_On_CanMoveTo()
        {
            var board = new Board();
            var pawn = new Pawn(new Square(1, 2), board, PieceColor.White);
            var knight = new Knight(new Square(1, 3), board, PieceColor.Black);

            Assert.That(pawn.CanMoveTo(new Square(1, 3)), Is.False);
        }
    }
}

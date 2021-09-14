using chezzles.core.engine.Core;
using chezzles.core.engine.Pieces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.core.engine.Tests.Pieces
{
    [TestFixture]
    public class KingTests
    {
        [Test]
        public void Whether_king_ReturnsCorrectMoves_On_PossibleMoves()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);

            var possibleMoves = king.PossibleMoves();

            Assert.That(possibleMoves != null);
            Assert.That(possibleMoves.Count() == 8);
        }

        [Test]
        public void Whether_King_CantMoveToIncorrectSquare_On_CanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);

            Assert.That(king.CanMoveTo(new Square(6, 6)), Is.False);
        }

        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(0, 1)]
        public void Whether_King_CanMoveToPossibleSquare_On_CanMoveTo(int offsetX, int offsetY)
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);

            Assert.That(king.CanMoveTo(
                new Square(king.Position.XPosition + offsetX,
                           king.Position.YPosition + offsetY)));
        }

        [Test]
        public void Whether_King_CantMoveToOccupiedSquare_On_CanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);
            var anotherKnight = new Knight(new Square(5, 5), board, PieceColor.White);

            Assert.That(king.CanMoveTo(new Square(5, 5)), Is.False);
        }

        [Test]
        public void Whether_King_CantMoveOutsideTheBoard_On_CanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(1, 1), board, PieceColor.White);

            Assert.That(king.PossibleMoves().Count() == 3);
            Assert.That(king.CanMoveTo(new Square(-1, -1)), Is.False);
        }

        [Test]
        public void Whether_King_CantMoveToBeatenSquare_On_CanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);
            var bishop = new Bishop(new Square(4, 6), board, PieceColor.Black);

            Assert.That(king.CanMoveTo(new Square(5, 5)), Is.False);
        }

        [Test]
        public void Whether_King_CantMoveToBeatenPawnSquare_On_CanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);
            var bishop = new Pawn(new Square(5, 4), board, PieceColor.Black);

            Assert.That(king.CanMoveTo(new Square(4, 3)), Is.False);
        }

        [Test]
        public void Whether_King_CantEscapeFromRookOnTheSameFile_OnCanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);
            var bishop = new Rook(new Square(5, 4), board, PieceColor.Black);

            Assert.That(king.CanMoveTo(new Square(3, 4)), Is.False);
        }

        [Test]
        public void Whether_King_CantEscapeFromRookOnTheSameRank_On_CanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);
            var bishop = new Rook(new Square(4, 7), board, PieceColor.Black);

            Assert.That(king.CanMoveTo(new Square(4, 3)), Is.False);
        }

        [Test]
        public void Whether_King_CantEscapeFromBishopOnTheSameDiagonal_OnCanMoveTo()
        {
            var board = new Board();
            var king = new King(new Square(4, 4), board, PieceColor.White);
            var bishop = new Bishop(new Square(5, 5), board, PieceColor.Black);

            Assert.That(king.CanMoveTo(new Square(3, 3)), Is.False);
        }
    }
}

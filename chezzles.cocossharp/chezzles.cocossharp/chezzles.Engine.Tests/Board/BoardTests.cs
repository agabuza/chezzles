using chezzles.engine.Core;
using chezzles.engine.Pieces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.Engine.Tests.BoardTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void Whether_Piece_UpdatesPositionOnBoard_On_MoveTo()
       {
            var board = new Board();
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);
            var bishop = new Bishop(originalPosition, board, PieceColor.White);

            var possibleMoves = bishop.MoveTo(newPosition);

            Assert.That(board.Squares[originalPosition] == null);
            Assert.That(board.Squares[newPosition] == bishop);
        }
    }
}

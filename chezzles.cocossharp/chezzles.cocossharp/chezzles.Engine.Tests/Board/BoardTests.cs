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
        public void Whether_Board_UpdatesPositionOnBoard_On_MoveTo()
        {
            var board = new Board();
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);
            var bishop = new Bishop(originalPosition, board, PieceColor.White);

            var possibleMoves = bishop.MoveTo(newPosition);

            Assert.That(board.Squares[originalPosition] == null);
            Assert.That(board.Squares[newPosition] == bishop);
        }

        [Test]
        public void Whether_Board_InvokesPieceTaken_On_Caprute()
        {
            var board = new Board();
            var pieceTaken = false;
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);
            var bishop = new Bishop(originalPosition, board, PieceColor.White);
            var knight = new Bishop(newPosition, board, PieceColor.Black);
            knight.PieceTaken += (s) => pieceTaken = true;

            var possibleMoves = bishop.MoveTo(newPosition);

            Assert.That(board.Squares[originalPosition] == null);
            Assert.That(board.Squares[newPosition] == bishop);
            Assert.That(pieceTaken);
        }

        [Test]
        public void Whether_Piece_UpdatesPosition_On_MoveTo()
        {
            var board = new Board();
            var newPos = new Square(1, 6);
            var rook = new Rook(new Square(1, 1), board, PieceColor.White);

            rook.MoveTo(newPos);

            Assert.That(rook.Position.Equals(newPos));
        }

        [Test]
        public void Whether_Board_RemovesPiece_On_PutPiece()
        {
            var board = new Board();
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);

            var bishop = new Bishop(originalPosition, board, PieceColor.White);
            var knight = new Bishop(newPosition, board, PieceColor.Black);

            board.PutPiece(newPosition, bishop);

            Assert.That(board.Pieces != null);
            Assert.That(board.Pieces.Count() == 1);
        }

        [Test]
        public void Whether_Board_AddsPieces_On_PutPiece()
        {
            var board = new Board();
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);

            var bishop = new Bishop(originalPosition, board, PieceColor.White);
            var knight = new Bishop(newPosition, board, PieceColor.Black);

            Assert.That(board.Pieces != null);
            Assert.That(board.Pieces.Count() == 2);
        }
    }
}

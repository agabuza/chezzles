using chezzles.engine.Core;
using chezzles.engine.Core.Game;
using chezzles.engine.Pieces;
using NUnit.Framework;
using System.Linq;

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
        public void Whether_Board_InitializesIsWhiteMoveToDefault_On_Contruct()
        {
            var board = new Board();
            Assert.That(board.IsWhiteMove);
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
        public void Whether_Piece_PositionUpdatedRaised_On_PieceMoved()
        {
            var board = new Board();
            var newPos = new Square(1, 6);
            var moved = false;
            var rook = new Rook(new Square(1, 1), board, PieceColor.White);
            rook.PositionUpdated += (s, p) => moved = true;

            rook.MoveTo(newPos);

            Assert.That(moved, Is.True);
        }


        [Test]
        public void Whether_Board_UpdatesIsWhiteMove_On_MoveTo()
        {
            var board = new Board();
            var newPos = new Square(1, 6);
            var oldPos = new Square(1, 1);
            var rook = new Rook(oldPos, board, PieceColor.White);

            var hasMoved = rook.MoveTo(newPos);

            Assert.That(rook.Position.Equals(newPos));
            Assert.IsFalse(board.IsWhiteMove);
            Assert.That(hasMoved);
        }

        [Test]
        public void Whether_Piece_CantMoveWhenIsWhiteMove_On_MoveTo()
        {
            var board = new Board();
            var newPos = new Square(1, 6);
            var oldPos = new Square(1, 1);
            var rook = new Rook(oldPos, board, PieceColor.Black);

            var hasMoved = rook.MoveTo(newPos);

            Assert.That(board.IsWhiteMove);
            Assert.That(rook.Position.Equals(oldPos));
            Assert.That(!hasMoved);
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

        [Test]
        public void Whether_Board_SetsBottomTopDirection_On_Init()
        {
            var board = new Board();
            Assert.That(board.IsBottomUpDirection, Is.True);
        }

        [Test]
        public void Whether_Board_InvokesOnPieceMoved_On_PieceMoveTo()
        {
            var board = new Board();
            var pieceMoved = false;
            board.PieceMoved += (b, m) => pieceMoved = true;
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);

            var bishop = new Bishop(originalPosition, board, PieceColor.White);
            bishop.MoveTo(newPosition);

            Assert.That(pieceMoved);
        }

        [Test]
        public void Whether_Board_DoesntInvokePieceMoved_On_MakeMove()
        {
            var board = new Board();
            var pieceMoved = false;
            board.PieceMoved += (b, m) => pieceMoved = true;
            var originalPosition = new Square(5, 4);
            var newPosition = new Square(4, 3);

            var bishop = new Bishop(originalPosition, board, PieceColor.White);
            board.MakeMove(new Move()
            {
                Color = PieceColor.White,
                OriginalSquare = originalPosition,
                TargetSquare = newPosition,
                TargetPiece = bishop.Type
            });

            Assert.That(pieceMoved, Is.False);
        }

    }
}

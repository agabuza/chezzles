﻿using chezzles.core.engine.Core;
using chezzles.core.engine.Core.Game;
using chezzles.core.engine.Core.Game.Messages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.core.engine.Tests.GameTests
{
    public class GameTests
    {
        private static string fenGame = @"
[Event ""Rom""]
[Site ""?""]
[Date ""1987.??.??""]
[Round ""?""]
[White ""A. Lotti""]
[Black ""F. Lotti""]
[Result ""0-1""]
[Annotator ""T1R""]
[SetUp ""1""]
[FEN ""2r5/pp2p1k1/3pp1P1/q7/4P3/2r5/PPPQ4/1K5R b - - 0 1""]
[PlyCount ""5""]
[EventDate ""1987.??.??""]
[EventType ""game""]

1... Rh3 2. Qd1 Qd2 3. Rf1 Qxd1+ 0-1
";

        public const string fenWhiteMove = @"[Event ""Ungarn""]
[Site ""?""]
[Date ""1975.??.??""]
[Round ""?""]
[White ""Barczay""]
[Black ""Erdely""]
[Result ""1-0""]
[Annotator ""T1H""]
[SetUp ""1""]
[FEN ""2r4k/3r1p1p/1p2pP2/p2pPp1P/P2P1Q2/6R1/4B1PK/2q5 w - - 0 1""]
[PlyCount ""1""]
[EventDate ""1975.??.??""]
[EventType ""team""]

1. Rg8+ 1-0";

        [Test]
        public void Whether_Game_ParsesPgnGame_On_Construct()
        {
            var parser = new GameParser();

            var game = parser.Parse(fenGame).FirstOrDefault();            

            Assert.That(game.Board != null);
            Assert.That(game.Board.Pieces.Count() == 17);
        }

        [Test]
        public void Whether_Game_NotifiesWhenPuzzleFailed_On_PieceMove()
        {
            var failed = false;
            var parser = new GameParser();
            var result = parser.Parse(fenWhiteMove);
            var game = result.FirstOrDefault();
            game.PuzzleFailed += (b, m) => failed = true;
            var targetSquare = new Square(7, 7);

            var rook = game.Board.Pieces.FirstOrDefault(x => x.Type == PieceType.Rook
                    && x.PossibleMoves().Contains(targetSquare));

            game.Board.PutPiece(targetSquare, rook);

            Assert.That(failed, Is.True);
        }

        [Test]
        public void Whether_Game_NotifiesWhenPuzzleSolved_On_PieceMove()
        {
            var solved = false;
            var parser = new GameParser();
            var result = parser.Parse(fenWhiteMove);
            var game = result.FirstOrDefault();
            game.PuzzleSolved += (b, m) => solved = true;

            var targetSquare = new Square(7, 8);

            var rook = game.Board.Pieces.FirstOrDefault(x => x.Type == PieceType.Rook
                    && x.PossibleMoves().Contains(targetSquare)
                    && x.Color == (game.Board.IsWhiteMove ? PieceColor.White : PieceColor.Black));

            game.Board.PutPiece(targetSquare, rook);

            Assert.That(solved, Is.True);
        }

        [Test]
        public void Whether_Game_SendsPuzzleSolvedMessage_On_PuzzleSolved()
        {
            var solved = false;
            var parser = new GameParser();
            var result = parser.Parse(fenWhiteMove);
            var game = result.FirstOrDefault();
            var targetSquare = new Square(7, 8);
            // Messenger.Default.Register<PuzzleCompletedMessage>(this, (msg) => solved = msg.IsSolved);

            var rook = game.Board.Pieces.FirstOrDefault(x => x.Type == PieceType.Rook
                    && x.PossibleMoves().Contains(targetSquare)
                    && x.Color == (game.Board.IsWhiteMove ? PieceColor.White : PieceColor.Black));

            game.Board.PutPiece(targetSquare, rook);

            Assert.That(solved, Is.True);
        }

        [Test]
        public void Whether_Game_SendsPuzzleFailedMessage_On_PuzzleSolved()
        {
            var failed = false;
            var parser = new GameParser();
            var result = parser.Parse(fenWhiteMove);
            var game = result.FirstOrDefault();
            var targetSquare = new Square(7, 7);
            // Messenger.Default.Register<PuzzleCompletedMessage>(this, (msg) => failed = !msg.IsSolved);

            var rook = game.Board.Pieces.FirstOrDefault(x => x.Type == PieceType.Rook
                    && x.PossibleMoves().Contains(targetSquare)
                    && x.Color == (game.Board.IsWhiteMove ? PieceColor.White : PieceColor.Black));

            game.Board.PutPiece(targetSquare, rook);

            Assert.That(failed, Is.True);
        }

        [Test]
        public void Whether_Game_OpponentMakesNextMove_On_PieceMoved()
        {
            var pieceMoved = false;
            var parser = new GameParser();
            var result = parser.Parse(fenGame);
            var game = result.FirstOrDefault();
            var targetSquare = new Square(8, 3);

            var rook = game.Board.Pieces.FirstOrDefault(x => x.Type == PieceType.Rook
                    && x.PossibleMoves().Contains(targetSquare)
                    && x.Color == (game.Board.IsWhiteMove ? PieceColor.White : PieceColor.Black));

            var whiteQueen = game.Board.Pieces.FirstOrDefault(x => 
                    x.Type == PieceType.Queen
                    && x.Color == PieceColor.White);
            whiteQueen.PositionUpdated += (e, s) => pieceMoved = true;

            game.Board.PutPiece(targetSquare, rook);


            Assert.That(pieceMoved, Is.True);
        }
    }
}

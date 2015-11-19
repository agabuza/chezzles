using NUnit.Framework;
using chezzles.engine.Core.Game;
using System.Linq;

namespace chezzles.Engine.Tests
{
    [TestFixture]
    public class GameParserTests
    {
        private const string game = @"
[Event ""Breslau""]
[Site ""Breslau""]
[Date ""1879.??.??""]
[Round ""?""]
[White ""Tarrasch, Siegbert""]
[Black ""Mendelsohn, J.""]
[Result ""1-0""]
[WhiteElo """"]
[BlackElo """"]
[ECO ""C49""]

1.e4 e5 2.Nf3 Nc6 3.Nc3 Nf6 4.Bb5 Bb4 5.Nd5 Nxd5 6.exd5 Nd4 7.Ba4 b5 8.Nxd4 bxa4
9.Nf3 O-O 10.O-O d6 11.c3 Bc5 12.d4 exd4 13.Nxd4 Ba6 14.Re1 Bc4 15.Nc6 Qf6
16.Be3 Rfe8 17.Bxc5 Rxe1+ 18.Qxe1 dxc5 19.Qe4 Bb5 20.d6 Kf8 21.Ne7 Re8 22.Qxh7 Qxd6
23.Re1 Be2 24.Nf5  1-0
";

        private const string fenGame = @"
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

        private const string fenBlackMove = @"
[Event ""Lwow""]
[Site ""?""]
[Date ""1990.??.??""]
[Round ""?""]
[White ""Asmaiparaschwili""]
[Black ""Schirow""]
[Result ""0-1""]
[Annotator ""T1A""]
[SetUp ""1""]
[FEN ""1rr3k1/4pp1p/2Np2p1/PP6/2Q5/2R3Pb/2p2P1P/2q2BK1 b - - 0 1""]
[PlyCount ""3""]
[EventDate ""1990.??.??""]
[EventType ""team""]

1... Rxc6 2. bxc6 Rb1 0-1";

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
        public void Whether_GameParser_ParsesGame_On_Parse()
        {
            var parser = new GameParser();
            var result = parser.Parse(game);
            Assert.That(result != null);
        }

        [Test]
        public void Whether_GameParser_ParsesFENGame_On_Parse()
        {
            var parser = new GameParser();
            var result = parser.Parse(fenGame);
            Assert.That(result != null);
        }

        [Test]
        public void Whether_GameParser_ParsesFENGameMoves_On_Parse()
        {
            var parser = new GameParser();
            var result = parser.Parse(fenBlackMove);

            var game = result.FirstOrDefault();

            Assert.That(game != null);
            Assert.That(game.Moves != null);
            Assert.That(game.Moves.Count == 3);
        }

        [TestCase(fenWhiteMove, true)]
        [TestCase(fenBlackMove, false)]
        public void Whether_GameParser_ParsesIsWhiteMove_On_Parse(string fenGame, bool isWhiteMove)
        {
            var parser = new GameParser();
            var result = parser.Parse(fenGame);

            var game = result.FirstOrDefault();

            Assert.That(game != null);
            Assert.That(game.Board.IsWhiteMove == isWhiteMove);
        }
    }
}

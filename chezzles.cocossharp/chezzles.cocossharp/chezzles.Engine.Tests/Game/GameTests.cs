using chezzles.engine.Core.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.Engine.Tests.GameTests
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

        [Test]
        public void Whether_Game_ParsesPgnGame_On_Construct()
        {
            var parser = new GameParser();

            var game = parser.Parse(fenGame).FirstOrDefault();            

            Assert.That(game.Board != null);
            Assert.That(game.Board.Pieces.Count() == 17);
        }
    }
}

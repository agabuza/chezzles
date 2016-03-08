using NUnit.Framework;

namespace chezzles.cocossharp.tests.Game
{
    [TestFixture]
    public class GameLayerTests
    {
        [SetUp]
        public void Setup()
        {
            this.game = new GameLayer(null);
        }
    }
}

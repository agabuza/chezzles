using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.core.engine.Core.Game
{
    public class MoveEntry
    {
        public Move WhiteMove { get; set; }
        public Move BlackMove { get; set; }
        public int Number { get; set; }
        public bool IsGameEnd { get; internal set; }
        public GameResult Result { get; internal set; }
        public string Comment { get; internal set; }
    }
}

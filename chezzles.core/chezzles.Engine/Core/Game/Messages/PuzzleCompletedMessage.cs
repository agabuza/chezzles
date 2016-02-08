using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.engine.Core.Game.Messages
{
    public class PuzzleCompletedMessage
    {
        public PuzzleCompletedMessage(bool solved)
        {
            IsSolved = solved;
        }

        public bool IsSolved { get; private set; }
    }
}

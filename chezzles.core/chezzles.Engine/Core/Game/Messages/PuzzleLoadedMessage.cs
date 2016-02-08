using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.engine.Core.Game.Messages
{
    public class PuzzleLoadedMessage
    {
        public PuzzleLoadedMessage(bool isWhiteMove, string title)
        {
            IsWhiteMove = isWhiteMove;
            Title = title;
        }

        public bool IsWhiteMove { get; private set; }
        public string Title { get; private set; }
    }
}

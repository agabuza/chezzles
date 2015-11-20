using System;
using System.Collections.Generic;

namespace chezzles.engine.Core.Game
{

    public sealed class Game
    {

        public Board Board { get; set; }

        public IEnumerable<Move> Moves { get; set; }

        public Move NextMove
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public Move PrevMove
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

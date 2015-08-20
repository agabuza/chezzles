using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chezzles.engine.Core
{
    public struct Square
    {
        public Square(int XPosition, int YPosition)
        {
            this.XPosition = XPosition;
            this.YPosition = YPosition;
        }

        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}

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

        public static bool operator ==(Square a, Square b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.XPosition == b.XPosition && a.YPosition == b.YPosition;
        }

        public static bool operator !=(Square a, Square b)
        {
            return !(a == b);
        }
    }
}

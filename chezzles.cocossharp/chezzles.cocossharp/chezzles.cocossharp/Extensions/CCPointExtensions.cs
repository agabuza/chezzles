using chezzles.engine.Core;
using CocosSharp;

namespace chezzles.cocossharp.Extensions
{
    public static class CCPointExtensions
    {
        public static Square GetSquare(this CCPoint point, float width)
        {
            var origin = GameLayer.Origin;
            var squareSize = width / 8;
            var xPosition = (point.X - origin.X) / squareSize;
            var yPosition = (point.Y - origin.Y) / squareSize;

            return new Square((int)xPosition + 1, (int)yPosition + 1);
        }

        public static CCPoint GetPoint(this Square square, float width)
        {
            var origin = GameLayer.Origin;
            var squareSize = width / 8;
            var xPosition = (square.XPosition) * squareSize - squareSize / 2 + origin.X;
            var yPosition = (square.YPosition) * squareSize - squareSize / 2 + origin.Y;

            return new CCPoint(xPosition, yPosition);
        }
    }
}

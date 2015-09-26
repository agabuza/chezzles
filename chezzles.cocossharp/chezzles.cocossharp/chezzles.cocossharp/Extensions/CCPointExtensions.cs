using chezzles.engine.Core;
using CocosSharp;

namespace chezzles.cocossharp.Extensions
{
    public static class CCPointExtensions
    {
        public static Square GetSquare(this CCPoint point, float width)
        {
            var width = parent.Window.WindowSizeInPixels.Width;
            var squareSize = width / 8;
            var xPosition = point.X / squareSize;
            var yPosition = point.Y / squareSize;

            return new Square((int)xPosition, (int)yPosition);
        }

        public static CCPoint GetPoint(this Square square, float width)
        {
            var squareSize = width / 8;
            var xPosition = square.XPosition * squareSize + squareSize / 2;
            var yPosition = square.YPosition * squareSize + squareSize / 2;

            return new CCPoint(xPosition, yPosition);
        }
    }
}


namespace MT.Tavlei.Core.Common
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            var pt = obj as Point;
            return X == pt.X && Y == pt.Y;
        }

        public static Point GetLeft(int x, int y)
        {
            return new Point(x - 1, y);
        }

        public static Point GetRight(int x, int y)
        {
            return new Point(x + 1, y);
        }

        public static Point GetTop(int x, int y)
        {
            return new Point(x, y - 1);
        }

        public static Point GetBottom(int x, int y)
        {
            return new Point(x, y + 1);
        }
    }
}

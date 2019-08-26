using System.Collections.Generic;
using System.Numerics;

namespace MT.Tavlei.Core.Common
{
    public static class Extensions
    {
        public static bool IsBitSet(this BigInteger big, int bit)
        {
            return BigInteger.One == ((big >> bit) & BigInteger.One);
        }

        public static BigInteger SetBit(this BigInteger big, int bit)
        {
            big |= (BigInteger.One << bit);
            return big;
        }

        public static BigInteger ResetBit(this BigInteger big, int bit)
        {
            big &= ~(BigInteger.One << bit);
            return big;
        }

        public static IEnumerable<Point> GetPoints(this BitMatrix matrix)
        {
            for (int y = 0; y < matrix.Height; ++y)
            {
                for (int x = 0; x < matrix.Width; ++x)
                {
                    if (matrix.Is(x, y))
                        yield return new Point(x, y);
                }
            }
        }
    }
}

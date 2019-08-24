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
    }
}

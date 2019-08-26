using System.Numerics;

namespace MT.Tavlei.Core.Common
{
    public class BitMatrix
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly BitMatrixIndexer indexer;
        private BigInteger data;

        public BitMatrix(int width, int height)
            : this(width, height, BigInteger.Zero)
        {
        }

        public BitMatrix(int width, int height, string data)
            : this(width, height, BigInteger.Parse(data))
        {
        }

        private BitMatrix(int width, int height, BigInteger data)
        {
            Width = width;
            Height = height;

            indexer = new BitMatrixIndexer(width, height);
            this.data = data;
        }

        public bool IsEmpty()
        {
            return data.IsZero;
        }

        public bool Is(int x, int y)
        {
            var shift = indexer.GetShift(x, y);
            return data.IsBitSet(shift);
        }

        public void Set(int x, int y)
        {
            var bit = indexer.GetShift(x, y);
            data.SetBit(bit);
        }

        public void Reset(int x, int y)
        {
            var bit = indexer.GetShift(x, y);
            data.ResetBit(bit);
        }

        public bool IsOnLeft(int x, int y, int step = 1)
        {
            return (x - step >= 0) ? Is(x - step, y) : false;
        }

        public bool IsOnRight(int x, int y, int step = 1)
        {
            return (x + step < Width) ? Is(x + step, y) : false;
        }

        public bool IsOnTop(int x, int y, int step = 1)
        {
            return (y - step >= 0) ? Is(x, y - step) : false;
        }

        public bool IsOnBottom(int x, int y, int step = 1)
        {
            return (y + step < Height) ? Is(x, y + step) : false;
        }

        public bool IsAroundAll(int x, int y)
        {
            return IsOnLeft(x, y) && IsOnRight(x, y) && IsOnTop(x, y) && IsOnBottom(x, y);
        }

        public bool IsAroundOne(int x, int y)
        {
            return IsOnLeft(x, y) || IsOnRight(x, y) || IsOnTop(x, y) || IsOnBottom(x, y);
        }

        public void Move(int x0, int y0, int x1, int y1)
        {
            var sh0 = indexer.GetShift(x0, y0);
            var sh1 = indexer.GetShift(x1, y1);
            MoveBit(sh0, sh1);
        }

        private void MoveBit(int shift0, int shift1)
        {
            data.ResetBit(shift0);
            data.SetBit(shift1);
        }

        public BitMatrix GetZeroBitsCross(int x, int y, int max)
        {
            var shift = indexer.GetShift(x, y);
            var data = GetZeroBitsCross(shift, max);
            return new BitMatrix(Width, Height, data);
        }

        private BigInteger GetZeroBitsCross(int shift, int max)
        {
            var horizontal = GetZeroBitsHorizontal(shift, max);
            var vertical = GetZeroBitsVertical(shift, max);
            return horizontal | vertical;
        }

        private BigInteger GetZeroBitsHorizontal(int shift, int max)
        {
            var left = GetZeroBitsOnLeft(shift, max);
            var right = GetZeroBitsOnRight(shift, max);
            return left | right;
        }

        private BigInteger GetZeroBitsVertical(int shift, int max)
        {
            var top = GetZeroBitsOnTop(shift, max);
            var bottom = GetZeroBitsOnBottom(shift, max);
            return top | bottom;
        }

        private BigInteger GetZeroBitsOnLeft(int shift, int max)
        {
            var line = BigInteger.Zero;

            int step = 0;
            int edge = indexer.GetEdgeShiftRight(shift);
            for (int i = shift - 1; i >= edge; --i)
            {
                step++;
                if (step > max)
                    break;

                if (data.IsBitSet(i))
                    break;

                line.SetBit(i);
            }

            return line;
        }

        private BigInteger GetZeroBitsOnRight(int shift, int max)
        {
            var line = BigInteger.Zero;

            int step = 0;
            int edge = indexer.GetEdgeShiftLeft(shift);
            for (int i = shift + 1; i <= edge; ++i)
            {
                step++;
                if (step > max)
                    break;

                if (data.IsBitSet(i))
                    break;

                line.SetBit(i);
            }

            return line;
        }

        private BigInteger GetZeroBitsOnTop(int shift, int max)
        {
            var line = BigInteger.Zero;

            int step = 0;
            int edge = indexer.GetEdgeShiftBottom(shift);
            for (int i = shift - Width; i >= edge; i -= Width)
            {
                step++;
                if (step > max)
                    break;

                if (data.IsBitSet(i))
                    break;

                line.SetBit(i);
            }

            return line;
        }

        private BigInteger GetZeroBitsOnBottom(int shift, int max)
        {
            var line = BigInteger.Zero;

            int step = 0;
            int edge = indexer.GetEdgeShiftTop(shift);
            for (int i = shift + Width; i <= edge; i += Width)
            {
                step++;
                if (step > max)
                    break;

                if (data.IsBitSet(i))
                    break;

                line.SetBit(i);
            }

            return line;
        }

        public BitMatrix Clone()
        {
            return new BitMatrix(Width, Height, data);
        }

        #region Operators

        public static BitMatrix operator ~(BitMatrix mx)
        {
            return new BitMatrix(mx.Width, mx.Height, ~mx.data);
        }

        public static BitMatrix operator &(BitMatrix mx1, BitMatrix mx2)
        {
            if (mx1.Width != mx2.Width || mx1.Height != mx2.Height)
                throw new TavleiRuntimeException("Размеры матриц не совпадают.");

            return new BitMatrix(mx1.Width, mx1.Height, mx1.data & mx2.data);
        }

        public static BitMatrix operator |(BitMatrix mx1, BitMatrix mx2)
        {
            if (mx1.Width != mx2.Width || mx1.Height != mx2.Height)
                throw new TavleiRuntimeException("Размеры матриц не совпадают.");

            return new BitMatrix(mx1.Width, mx1.Height, mx1.data | mx2.data);
        }

        #endregion
    }
}

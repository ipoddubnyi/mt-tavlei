
namespace MT.Tavlei.Core.Common
{
    public class BitMatrixIndexer
    {
        private readonly int Width;
        private readonly int Height;

        public BitMatrixIndexer(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int GetShift(int x, int y)
        {
            return x + y * Width;
        }

        public int GetX(int shift)
        {
            return shift % Width;
        }

        public int GetY(int shift)
        {
            return shift / Width;
        }

        //

        public int GetShiftNextLeft(int shift, int step = 1)
        {
            return shift - step;
        }

        public int GetShiftNextRight(int shift, int step = 1)
        {
            return shift + step;
        }

        public int GetShiftNextTop(int shift, int step = 1)
        {
            return shift - Width * step;
        }

        public int GetShiftNextBottom(int shift, int step = 1)
        {
            return shift + Width * step;
        }

        //

        public bool IsEdgeLeft(int shift)
        {
            return GetX(shift) == 0;
        }

        public bool IsEdgeRight(int shift)
        {
            return GetX(shift) == Width - 1;
        }

        public bool IsEdgeTop(int shift)
        {
            return GetY(shift) == 0;
        }

        public bool IsEdgeBottom(int shift)
        {
            return GetY(shift) == Height - 1;
        }

        //

        public int GetEdgeShiftLeft(int shift)
        {
            return shift - GetX(shift);
        }

        public int GetEdgeShiftRight(int shift)
        {
            return shift + Width - GetX(shift) - 1;
        }

        public int GetEdgeShiftTop(int shift)
        {
            return shift % Width;
        }

        public int GetEdgeShiftBottom(int shift)
        {
            return shift + Width * (Height - GetY(shift) - 1);
        }
    }
}

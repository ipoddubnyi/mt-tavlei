using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using MT.Tavlei.Core.Common;

namespace MT.Tavlei.Core
{
    public class Engine
    {
        private readonly int WIDTH;
        private readonly int HEIGHT;
        private readonly int FULLSIZE;

        private readonly BigInteger _lineHorizontal = 511;
        private readonly BigInteger _lineVertical = BigInteger.Parse("4731607904558235517441");

        private Board board;

        public Engine(Board board)
        {
            this.board = board;
        }

        /*// TODO: правило
        public Point[] GetWays(int x, int y)
        {
            return GetWayMatrix(x, y).GetPoints().ToArray();
        }

        // TODO: правило
        private BitMatrix GetWayMatrix(int x, int y)
        {
            var fig = GetFigure(x, y);
            var ways = GetZeroBitsCross(x, y, fig.MaxSteps);

            if (!(fig is King))
            {
                ways &= ~_fieldExits;
                ways &= ~_fieldThrone;
            }

            return ways;
        }

        // TODO: правило
        public Point[] GetCatches(int x, int y)
        {
            if (IsAttacker(x, y))
            {
                return GetCatchesAttacker(x, y);
            }
            
            if (IsDefenderOrKing(x, y))
            {
                return GetCatchesDefender(x, y);
            }

            // TODO: сделать иключение
            throw new Exception("В заданной ячейке нет фигуры.");
        }*/

        /*#region Catch Attacker

        private Point[] GetCatchesAttacker(int x, int y)
        {
            var catches = new List<Point>();

            if (GetCatchesAttackerLeft(x, y))
                catches.Add(Point.GetLeft(x, y));

            if (GetCatchesAttackerRight(x, y))
                catches.Add(Point.GetRight(x, y));

            if (GetCatchesAttackerTop(x, y))
                catches.Add(Point.GetTop(x, y));

            if (GetCatchesAttackerBottom(x, y))
                catches.Add(Point.GetBottom(x, y));

            return catches.ToArray();
        }

        private bool GetCatchesAttackerLeft(int x, int y)
        {
            if (!_figuresD.IsOnLeft(x, y))
                return false;

            if (_figuresDK.IsOnLeft(x, y))
            {
                // князь слева

                if (IsKingOnThrone())
                    return _figuresA.IsAroundAll(x - 1, y);

                if (IsThroneNear(x - 1, y))
                    return IsCatchKingNearThrone(x - 1, y);

                if (_figuresA.IsOnLeft(x, y, 2))
                    return true;
            }
            else
            {
                // защитник слева

                if (_figuresA.IsOnLeft(x, y, 2))
                    return true;

                if (_fieldExits.IsOnLeft(x, y, 2))
                    return true;

                if (_fieldThrone.IsOnLeft(x, y, 2) && !_figuresDK.IsOnLeft(x, y, 2))
                    return true;
            }

            return false;
        }

        private bool GetCatchesAttackerRight(int x, int y)
        {
            if (!_figuresD.IsOnRight(x, y))
                return false;

            if (_figuresDK.IsOnRight(x, y))
            {
                // князь слева

                if (IsKingOnThrone())
                    return _figuresA.IsAroundAll(x + 1, y);

                if (IsThroneNear(x + 1, y))
                    return IsCatchKingNearThrone(x + 1, y);

                if (_figuresA.IsOnRight(x, y, 2))
                    return true;
            }
            else
            {
                // защитник слева

                if (_figuresA.IsOnRight(x, y, 2))
                    return true;

                if (_fieldExits.IsOnRight(x, y, 2))
                    return true;

                if (_fieldThrone.IsOnRight(x, y, 2) && !_figuresDK.IsOnRight(x, y, 2))
                    return true;
            }

            return false;
        }

        private bool GetCatchesAttackerTop(int x, int y)
        {
            if (!_figuresD.IsOnTop(x, y))
                return false;

            if (_figuresDK.IsOnTop(x, y))
            {
                // князь слева

                if (IsKingOnThrone())
                    return _figuresA.IsAroundAll(x, y - 1);

                if (IsThroneNear(x, y - 1))
                    return IsCatchKingNearThrone(x, y - 1);

                if (_figuresA.IsOnTop(x, y, 2))
                    return true;
            }
            else
            {
                // защитник слева

                if (_figuresA.IsOnTop(x, y, 2))
                    return true;

                if (_fieldExits.IsOnTop(x, y, 2))
                    return true;

                if (_fieldThrone.IsOnTop(x, y, 2) && !_figuresDK.IsOnTop(x, y, 2))
                    return true;
            }

            return false;
        }

        private bool GetCatchesAttackerBottom(int x, int y)
        {
            if (!_figuresD.IsOnBottom(x, y))
                return false;

            if (_figuresDK.IsOnBottom(x, y))
            {
                // князь слева

                if (IsKingOnThrone())
                    return _figuresA.IsAroundAll(x, y + 1);

                if (IsThroneNear(x, y + 1))
                    return IsCatchKingNearThrone(x, y + 1);

                if (_figuresA.IsOnBottom(x, y, 2))
                    return true;
            }
            else
            {
                // защитник слева

                if (_figuresA.IsOnBottom(x, y, 2))
                    return true;

                if (_fieldExits.IsOnBottom(x, y, 2))
                    return true;

                if (_fieldThrone.IsOnBottom(x, y, 2) && !_figuresDK.IsOnBottom(x, y, 2))
                    return true;
            }

            return false;
        }

        private bool IsKingOnThrone()
        {
            return _figuresDK == _fieldThrone;
        }

        public bool IsKingOnExit()
        {
            return !(_figuresDK & _fieldExits).IsEmpty();
        }

        private bool IsWinAttacker()
        {
            return IsExistK();
        }

        private bool IsWinDefender()
        {
            return !IsExistAnyA() || IsKingOnExit();
        }

        public bool IsExistAnyA()
        {
            return !_figuresA.IsEmpty();
        }

        public bool IsExistAnyD()
        {
            return !_figuresDD.IsEmpty();
        }

        public bool IsExistK()
        {
            return !_figuresDK.IsEmpty();
        }

        private bool IsThroneNear(int x, int y)
        {
            return _fieldThrone.IsAroundOne(x, y);
        }

        private bool IsCatchKingNearThrone(int x, int y)
        {
            if (_fieldThrone.IsOnLeft(x, y))
            {
                return _figuresA.IsOnRight(x, y) &&
                       _figuresA.IsOnTop(x, y) &&
                       _figuresA.IsOnBottom(x, y);
            }

            if (_fieldThrone.IsOnRight(x, y))
            {
                return _figuresA.IsOnLeft(x, y) &&
                       _figuresA.IsOnTop(x, y) &&
                       _figuresA.IsOnBottom(x, y);

            }

            if (_fieldThrone.IsOnTop(x, y))
            {
                return _figuresA.IsOnLeft(x, y) &&
                       _figuresA.IsOnRight(x, y) &&
                       _figuresA.IsOnBottom(x, y);
            }

            if (_fieldThrone.IsOnBottom(x, y))
            {
                return _figuresA.IsOnLeft(x, y) &&
                       _figuresA.IsOnRight(x, y) &&
                       _figuresA.IsOnTop(x, y);
            }

            return false;
        }

        #endregion*/

        /*#region Catch Defender

        private Point[] GetCatchesDefender(int x, int y)
        {
            var catches = new List<Point>();

            if (GetCatchesDefenderLeft(x, y))
                catches.Add(Point.GetLeft(x, y));

            if (GetCatchesDefenderRight(x, y))
                catches.Add(Point.GetRight(x, y));

            if (GetCatchesDefenderTop(x, y))
                catches.Add(Point.GetTop(x, y));

            if (GetCatchesDefenderBottom(x, y))
                catches.Add(Point.GetBottom(x, y));

            return catches.ToArray();
        }

        private bool GetCatchesDefenderLeft(int x, int y)
        {
            if (!_figuresA.IsOnLeft(x, y))
                return false;

            if (_figuresD.IsOnLeft(x, y, 2))
                return true;

            if (_fieldExits.IsOnLeft(x, y, 2))
                return true;

            if (_fieldThrone.IsOnLeft(x, y, 2))
                return true;

            return false;
        }

        private bool GetCatchesDefenderRight(int x, int y)
        {
            if (!_figuresA.IsOnRight(x, y))
                return false;

            if (_figuresD.IsOnRight(x, y, 2))
                return true;

            if (_fieldExits.IsOnRight(x, y, 2))
                return true;

            if (_fieldThrone.IsOnRight(x, y, 2))
                return true;

            return false;
        }

        private bool GetCatchesDefenderTop(int x, int y)
        {
            if (!_figuresA.IsOnTop(x, y))
                return false;

            if (_figuresD.IsOnTop(x, y, 2))
                return true;

            if (_fieldExits.IsOnTop(x, y, 2))
                return true;

            if (_fieldThrone.IsOnTop(x, y, 2))
                return true;

            return false;
        }

        private bool GetCatchesDefenderBottom(int x, int y)
        {
            if (!_figuresA.IsOnBottom(x, y))
                return false;

            if (_figuresD.IsOnBottom(x, y, 2))
                return true;

            if (_fieldExits.IsOnBottom(x, y, 2))
                return true;

            if (_fieldThrone.IsOnBottom(x, y, 2))
                return true;

            return false;
        }

        #endregion*/

        /*private BitMatrix GetFigures(BitMatrixType type)
        {
            switch (type)
            {
                case BitMatrixType.Attacker:
                    return _figuresA;
                case BitMatrixType.Defender:
                    return _figuresD;
                case BitMatrixType.DefenderDefender:
                    return _figuresDD;
                case BitMatrixType.DefenderKing:
                    return _figuresDK;
                case BitMatrixType.Exits:
                    return _fieldExits;
                case BitMatrixType.Throne:
                    return _fieldThrone;
            }

            return null;
        }

        private bool IsFigureOnLeft(BitMatrixType type, int x, int y, int step = 1)
        {
            return GetFigures(type).IsOnLeft(x, y, step);
        }

        private bool IsFigureOnRight(BitMatrixType type, int x, int y, int step = 1)
        {
            return GetFigures(type).IsOnRight(x, y, step);
        }

        private bool IsFigureOnTop(BitMatrixType type, int x, int y, int step = 1)
        {
            return GetFigures(type).IsOnTop(x, y, step);
        }

        private bool IsFigureOnBottom(BitMatrixType type, int x, int y, int step = 1)
        {
            return GetFigures(type).IsOnBottom(x, y, step);
        }*/

        /*private BigInteger GetCanGoAD()
        {
            return ~(_figures | _fieldExits | _fieldThrone);
        }

        private BigInteger GetCanGoK()
        {
            return ~_figures;
        }*/

        /*private BigInteger GetBitsLeft(int sh0, int sh1)
        {
            var l = GetLeftBit(x);

            return res;
        }*/

        private BigInteger GetLineHorizontal(int sh)
        {
            /*var field = BigInteger.Zero;

            int shL = GetShiftEdgeL(sh);
            int shR = GetShiftEdgeR(sh);

            for (int i = shR; i <= shL; ++i)
            {
                field <<= shR;
            }

            return field << shR;*/

            //int shR = GetShiftEdgeR(sh);
            //return _lineHorizontal << shR;

            return BigInteger.Zero;
        }

        private BigInteger GetLineVertical(int sh)
        {
            //int shB = GetShiftEdgeB(sh);
            //return _lineVertical << shB;

            return BigInteger.Zero;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    public class Board
    {
        public const int WIDTH = 9;
        public const int HEIGHT = 9;

        private BitMatrix figuresA;
        private BitMatrix figuresD;
        private BitMatrix figuresK;
        private BitMatrix figuresDK;
        private BitMatrix figuresAll;

        private BitMatrix fieldExits;
        private BitMatrix fieldThrone;

        public Board()
        {
            figuresA = new BitMatrix(WIDTH, HEIGHT, InitFiguresA());
            figuresD = new BitMatrix(WIDTH, HEIGHT, InitFiguresD());
            figuresK = new BitMatrix(WIDTH, HEIGHT, InitFiguresK());
            figuresDK = figuresD | figuresK;
            figuresAll = figuresA | figuresDK;

            fieldExits = new BitMatrix(WIDTH, HEIGHT, InitFieldExits());
            fieldThrone = new BitMatrix(WIDTH, HEIGHT, InitFieldThrone());
        }

        private string InitFiguresA()
        {
            var sb = new StringBuilder();
            sb.Append("000111000");
            sb.Append("000010000");
            sb.Append("000000000");
            sb.Append("100000001");
            sb.Append("110000011");
            sb.Append("100000001");
            sb.Append("000000000");
            sb.Append("000010000");
            sb.Append("000111000");
            return sb.ToString();
        }

        private string InitFiguresD()
        {
            var sb = new StringBuilder();
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000010000");
            sb.Append("000010000");
            sb.Append("001101100");
            sb.Append("000010000");
            sb.Append("000010000");
            sb.Append("000000000");
            sb.Append("000000000");
            return sb.ToString();
        }

        private string InitFiguresK()
        {
            var sb = new StringBuilder();
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000010000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            return sb.ToString();
        }

        private string InitFieldExits()
        {
            var sb = new StringBuilder();
            sb.Append("100000001");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("100000001");
            return sb.ToString();
        }

        private string InitFieldThrone()
        {
            var sb = new StringBuilder();
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000010000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            sb.Append("000000000");
            return sb.ToString();
        }

        public bool IsOnBoard(int x, int y)
        {
            if (x < 0 || x >= WIDTH)
                return false;

            if (y < 0 || y >= HEIGHT)
                return false;

            return true;
        }

        public bool IsFigureType(int x, int y, FigureType type)
        {
            return GetFigureType(x, y) == type;
        }

        public bool IsPlayerSide(int x, int y, PlayerSide side)
        {
            return GetPlayerSide(x, y) == side;
        }

        public bool IsCellType(int x, int y, CellType type)
        {
            return GetCellType(x, y) == type;
        }

        public bool IsFigure(int x, int y)
        {
            return figuresA.Is(x, y) || figuresD.Is(x, y) || figuresK.Is(x, y);
        }

        public FigureType GetFigureType(int x, int y)
        {
            if (figuresA.Is(x, y))
                return FigureType.Attacker;

            if (figuresD.Is(x, y))
                return FigureType.Defender;

            if (figuresK.Is(x, y))
                return FigureType.King;

            throw new TavleiBoardException("В ячейке нет фигуры.");
        }

        public PlayerSide GetPlayerSide(int x, int y)
        {
            if (figuresA.Is(x, y))
                return PlayerSide.Attacker;

            if (figuresD.Is(x, y) || figuresK.Is(x, y))
                return PlayerSide.Defender;

            throw new TavleiBoardException("В ячейке нет фигуры.");
        }

        public CellType GetCellType(int x, int y)
        {
            if (fieldExits.Is(x, y))
                return CellType.Exit;

            if (fieldThrone.Is(x, y))
                return CellType.Throne;

            return CellType.Cell;
        }

        public void Move(int x0, int y0, int x1, int y1)
        {
            switch (GetFigureType(x0, y0))
            {
                case FigureType.Attacker:
                    MoveA(x0, y0, x1, y1);
                    break;
                case FigureType.Defender:
                    MoveD(x0, y0, x1, y1);
                    break;
                case FigureType.King:
                    MoveK(x0, y0, x1, y1);
                    break;
            }
        }

        public void MoveA(int x0, int y0, int x1, int y1)
        {
            figuresA.Move(x0, y0, x1, y1);
            figuresAll.Move(x0, y0, x1, y1);
        }

        public void MoveD(int x0, int y0, int x1, int y1)
        {
            figuresD.Move(x0, y0, x1, y1);
            figuresDK.Move(x0, y0, x1, y1);
            figuresAll.Move(x0, y0, x1, y1);
        }

        public void MoveK(int x0, int y0, int x1, int y1)
        {
            figuresK.Move(x0, y0, x1, y1);
            figuresDK.Move(x0, y0, x1, y1);
            figuresAll.Move(x0, y0, x1, y1);
        }

        public void Kill(int x, int y)
        {
            switch (GetFigureType(x, y))
            {
                case FigureType.Attacker:
                    KillA(x, y);
                    break;
                case FigureType.Defender:
                    KillD(x, y);
                    break;
                case FigureType.King:
                    KillK(x, y);
                    break;
            }
        }

        public void KillA(int x, int y)
        {
            figuresA.Reset(x, y);
            figuresAll.Reset(x, y);
        }

        public void KillD(int x, int y)
        {
            figuresD.Reset(x, y);
            figuresDK.Reset(x, y);
            figuresAll.Reset(x, y);
        }

        public void KillK(int x, int y)
        {
            figuresK.Reset(x, y);
            figuresDK.Reset(x, y);
            figuresAll.Reset(x, y);
        }
    }
}

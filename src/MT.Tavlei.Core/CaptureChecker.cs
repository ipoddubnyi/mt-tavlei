using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    class CaptureChecker
    {
        private readonly Board board;
        private readonly int x;
        private readonly int y;
        private readonly FigureType figure;

        private readonly bool onThrone;
        private readonly bool nearThrone;

        public CaptureChecker(Board board, int x, int y)
        {
            this.board = board;
            this.x = x;
            this.y = y;

            figure = board.GetFigureType(x, y);
            onThrone = false;
            nearThrone = false;

            if (figure == FigureType.King)
            {
                onThrone = board.IsCellType(x, y, CellType.Throne);

                if (!onThrone)
                {
                    nearThrone =
                        board.IsCellType(x - 1, y, CellType.Throne) ||
                        board.IsCellType(x + 1, y, CellType.Throne) ||
                        board.IsCellType(x, y - 1, CellType.Throne) ||
                        board.IsCellType(x, y + 1, CellType.Throne);
                }
            }
        }

        public bool Check(int x1, int y1, int x2, int y2)
        {
            switch (figure)
            {
                case FigureType.Attacker:
                    return CheckAttacker(x1, y1, x2, y2);
                case FigureType.Defender:
                    return CheckDefender(x1, y1, x2, y2);
                case FigureType.King:
                    return CheckKing(x1, y1, x2, y2);
            }

            return false;
        }

        public bool CheckAttacker(int x1, int y1, int x2, int y2)
        {
            // правило захвата А
            if (board.IsFigureType(x1, y1, FigureType.Defender) &&
                board.IsFigureType(x2, y2, FigureType.Defender))
                return true;

            // правило захвата Б
            if (board.IsFigureType(x1, y1, FigureType.Defender) &&
                board.IsCellType(x2, y2, CellType.Exit))
                return true;

            // правило захвата В
            if (board.IsFigureType(x1, y1, FigureType.Defender) &&
                board.IsCellType(x2, y2, CellType.Throne))
                return true;

            return false;
        }

        public bool CheckDefender(int x1, int y1, int x2, int y2)
        {
            // правило захвата А
            if (board.IsFigureType(x1, y1, FigureType.Attacker) &&
                board.IsFigureType(x2, y2, FigureType.Attacker))
                return true;

            // правило захвата Б
            if (board.IsFigureType(x1, y1, FigureType.Attacker) &&
                board.IsCellType(x2, y2, CellType.Exit))
                return true;

            // правило захвата Г
            if (board.IsFigureType(x1, y1, FigureType.Attacker) &&
                board.IsCellType(x2, y2, CellType.Throne) && !board.IsFigure(x2, y2))
                return true;

            return false;
        }

        public bool CheckKing(int x1, int y1, int x2, int y2)
        {
            if (onThrone)
            {
                // правило захвата князя А
                if (board.IsFigureType(x - 1, y, FigureType.Attacker) &&
                    board.IsFigureType(x + 1, y, FigureType.Attacker) &&
                    board.IsFigureType(x, y - 1, FigureType.Attacker) &&
                    board.IsFigureType(x, y + 1, FigureType.Attacker))
                    return true;

                return false;
            }

            if (nearThrone)
            {
                // правило захвата князя Б
                if ((board.IsFigureType(x - 1, y, FigureType.Attacker) || board.IsCellType(x - 1, y, CellType.Throne)) &&
                    (board.IsFigureType(x + 1, y, FigureType.Attacker) || board.IsCellType(x + 1, y, CellType.Throne)) &&
                    (board.IsFigureType(x, y - 1, FigureType.Attacker) || board.IsCellType(x, y - 1, CellType.Throne)) &&
                    (board.IsFigureType(x, y + 1, FigureType.Attacker) || board.IsCellType(x, y + 1, CellType.Throne)))
                    return true;

                return false;
            }

            // правило захвата А
            if (board.IsFigureType(x1, y1, FigureType.Attacker) &&
                board.IsFigureType(x2, y2, FigureType.Attacker))
                return true;

            // правило захвата Б
            if (board.IsFigureType(x1, y1, FigureType.Attacker) &&
                board.IsCellType(x2, y2, CellType.Exit))
                return true;

            return false;
        }
    }
}

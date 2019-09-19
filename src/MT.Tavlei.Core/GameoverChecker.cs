using System.Collections.Generic;
using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    class GameoverChecker
    {
        private readonly Board board;
        private readonly List<Point> captures;

        public GameoverChecker(Board board, List<Point> captures)
        {
            this.board = board;
            this.captures = captures;
        }

        public bool Check()
        {
            foreach (var point in captures)
            {
                if (board.IsFigureType(point.X, point.Y, FigureType.King))
                    return true;
            }

            if (board.IsKingOnExit())
                return true;

            return false;
        }
    }
}

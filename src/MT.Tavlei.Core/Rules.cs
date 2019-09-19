using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    public class Rules
    {
        private enum StepValidation
        {
            CanStep,
            CannotStepContinue,
            CannotStepStop
        }

        private const int KingStepMaxLength = 3;

        private readonly Board board;

        public Rules(Board board)
        {
            this.board = board;
        }

        /*public static PlayerSide getPlayerEnemy(PlayerSide plr)
        {
            return (PlayerSide.Attacker == plr) ? PlayerSide.Defender : PlayerSide.Attacker;
        }

        public static PlayerSide getFigurePlayer(FigureType fig)
        {
            return (FigureType.Attacker == fig) ? PlayerSide.Attacker : PlayerSide.Defender;
        }

        public static PlayerSide getFigureEnemy(FigureType fig)
        {
            return (FigureType.Attacker == fig) ? PlayerSide.Defender : PlayerSide.Attacker;
        }

        public static bool isPlayerFigure(PlayerSide plr, FigureType fig)
        {
            if (PlayerSide.Attacker == plr)
            {
                return (FigureType.Attacker == fig);
            }
            else
            {
                return (FigureType.Attacker != fig);
            }
        }

        public static bool IsEmenies(FigureType fig1, FigureType fig2)
        {
            return (
                    (FigureType.Attacker == fig1) && (FigureType.Attacker != fig2)
            ) || (
                    (FigureType.Attacker != fig1) && (FigureType.Attacker == fig2)
            );
        }*/

        /*public static List<Move> GetAllStepsForPlayer(PlayerSide plr)
        {
            ArrayList<Move> waysAll = new ArrayList<>();
            for (int j = 0; j < field.getHeight(); ++j)
            {
                for (int i = 0; i < field.getWidth(); ++i)
                {
                    FigureType fig = field.getFigure(i, j);
                    if (null != fig)
                    {
                        if (isPlayerFigure(plr, fig))
                        {
                            var ways = getWaysFrom(field, i, j);
                            foreach (var way in ways)
                            {
                                waysAll.add(new Move(i, j, way));
                            }
                        }
                    }
                }
            }
            return waysAll;
        }*/

        public List<Point> GetSteps(int x, int y)
        {
            var ways = new List<Point>();

            if (!board.IsFigure(x, y))
                return ways;

            var fig = board.GetFigureType(x, y);
            int steplengthmax = (fig == FigureType.King) ? KingStepMaxLength : Board.WIDTH;

            int steplength = 0;
            for (int i = x + 1; i < Board.WIDTH; ++i)
            {
                if (steplength >= steplengthmax)
                    break;

                var val = IsValidDestination(i, y, fig);
                if (StepValidation.CanStep == val)
                {
                    ways.Add(new Point(i, y));
                }
                else if (StepValidation.CannotStepStop == val)
                {
                    break;
                }

                steplength += 1;
            }

            steplength = 0;
            for (int i = x - 1; i >= 0; --i)
            {
                if (steplength >= steplengthmax)
                    break;

                var val = IsValidDestination(i, y, fig);
                if (StepValidation.CanStep == val)
                {
                    ways.Add(new Point(i, y));
                }
                else if (StepValidation.CannotStepStop == val)
                {
                    break;
                }

                steplength += 1;
            }

            steplength = 0;
            for (int j = y + 1; j < Board.HEIGHT; ++j)
            {
                if (steplength >= steplengthmax)
                    break;

                var val = IsValidDestination(x, j, fig);
                if (StepValidation.CanStep == val)
                {
                    ways.Add(new Point(x, j));
                }
                else if (StepValidation.CannotStepStop == val)
                {
                    break;
                }

                steplength += 1;
            }

            steplength = 0;
            for (int j = y - 1; j >= 0; --j)
            {
                if (steplength >= steplengthmax)
                    break;

                var val = IsValidDestination(x, j, fig);
                if (StepValidation.CanStep == val)
                {
                    ways.Add(new Point(x, j));
                }
                else if (StepValidation.CannotStepStop == val)
                {
                    break;
                }

                steplength += 1;
            }

            return ways;
        }

        private StepValidation IsValidDestination(int x, int y, FigureType fig)
        {
            if (board.IsFigure(x, y))
                return StepValidation.CannotStepStop;

            if (fig == FigureType.King)
                return StepValidation.CanStep;

            if (!board.IsCellType(x, y, CellType.Cell))
                return StepValidation.CannotStepContinue;

            return StepValidation.CanStep;
        }

        public List<Point> GetCaptures(int x, int y)
        {
            var captures = new List<Point>();

            if (x > 1)
            {
                if (board.IsFigure(x - 1, y))
                {
                    var cc = new CaptureChecker(board, x - 1, y);
                    if (cc.Check(x, y, x - 2, y))
                        captures.Add(new Point(x - 1, y));
                }
            }

            if (x < Board.WIDTH - 2)
            {
                if (board.IsFigure(x + 1, y))
                {
                    var cc = new CaptureChecker(board, x + 1, y);
                    if (cc.Check(x, y, x + 2, y))
                        captures.Add(new Point(x + 1, y));
                }
            }

            if (y > 1)
            {
                if (board.IsFigure(x, y - 1))
                {
                    var cc = new CaptureChecker(board, x, y - 1);
                    if (cc.Check(x, y, x, y - 2))
                        captures.Add(new Point(x, y - 1));
                }
            }

            if (y < Board.HEIGHT - 2)
            {
                if (board.IsFigure(x, y + 1))
                {
                    var cc = new CaptureChecker(board, x, y + 1);
                    if (cc.Check(x, y, x, y + 2))
                        captures.Add(new Point(x, y + 1));
                }
            }

            return captures;
        }
    }
}

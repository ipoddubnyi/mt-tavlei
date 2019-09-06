using System.Collections.Generic;
using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    public class Game
    {
        public delegate void SelectFigureHandler(FigureType figure, Point[] ways);
        public delegate void MoveHandler(FigureType figure, Point from, Point to, Point[] catches);
        public delegate void GameOverHandler(FigureType figure, Point from, Point to);

        public Board Board { get; private set; }
        private PlayerSide[] players;
        private int playerCurrent;

        public PlayerSide CurrentPlayer => players[playerCurrent];
        public Stack<StepInfo> History { get; private set; }

        public event SelectFigureHandler OnSelectFigure;
        public event MoveHandler OnMove;
        public event GameOverHandler OnGameOver;

        public Game()
        {
            Board = new Board();
            players = new PlayerSide[2] { PlayerSide.Attacker, PlayerSide.Defender };
            playerCurrent = 0;
            History = new Stack<StepInfo>();
        }

        public void NextPlayer()
        {
            playerCurrent = (playerCurrent + 1) % players.Length;
        }

        public void Select(int x, int y)
        {
            if (!Board.IsPlayerSide(x, y, CurrentPlayer))
                return;

            /*var fig = _engine.GetFigure(x, y);

            if (null == fig)
                throw new NoFigureException();

            if (!_players.Current.IsPlayerFigureType(fig))
                throw new EnemyFigureException();

            var ways = _engine.GetWays(x, y);

            if (OnSelectFigure != null)
                OnSelectFigure(fig, ways);*/
        }

        public StepInfo Move(int x0, int y0, int x1, int y1)
        {
            var figure = Board.GetFigureType(x0, y0);

            // TODO
            Board.Move(x0, y0, x1, y1);

            var info = new StepInfo(figure, x0, y0, x1, y1);
            History.Push(info);
            return info;
        }

        public void Analize()
        {
            // TODO
        }
    }
}

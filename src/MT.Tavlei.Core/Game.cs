using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    public class Game
    {
        public delegate void SelectFigureHandler(FigureType figure, Point[] ways);
        public delegate void MoveHandler(FigureType figure, Point from, Point to, Point[] catches);
        public delegate void GameOverHandler(FigureType figure, Point from, Point to);

        private Board field;
        private PlayerSide[] players;
        private int playerCurrent;

        public PlayerSide CurrentPlayer => players[playerCurrent];

        public event SelectFigureHandler OnSelectFigure;
        public event MoveHandler OnMove;
        public event GameOverHandler OnGameOver;

        public Game()
        {
            field = new Board();
            players = new PlayerSide[2] { PlayerSide.Attacker, PlayerSide.Defender };
            playerCurrent = 0;
        }

        public void NextPlayer()
        {
            playerCurrent = (playerCurrent + 1) % players.Length;
        }

        public void Select(int x, int y)
        {
            if (!field.IsPlayerSide(x, y, CurrentPlayer))
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

        public void Move(int x0, int y0, int x1, int y1)
        {
            // TODO
        }

        public void Analize()
        {
            // TODO
        }
    }
}

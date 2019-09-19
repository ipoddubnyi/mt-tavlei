using Out = System.Console;
using MT.Tavlei.Console.Engine.Commands;
using MT.Tavlei.Core;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Console.Engine
{
    class GameCycle
    {
        private Game game;

        public GameCycle(Game game)
        {
            this.game = game;
        }

        public void Start()
        {
            try
            {
                do
                {
                    Redraw();
                    Update();
                }
                while (true);
            }
            catch (GameCycleExitException)
            {
            }
            catch (GameCycleGameoverException)
            {
                Redraw();
                Out.WriteLine("Игра окончена. Победил {0}", game.CurrentPlayer);
            }
        }

        private void Redraw()
        {
            Out.Clear();
            DrawBoard(game.Board);
            Out.WriteLine();
        }

        private void Update()
        {
            Out.WriteLine("Ход игрока {0}:", game.CurrentPlayer);
            var line = Out.ReadLine();
            var command = CommandFactory.Create(line);

            while (!command.Check(game, out var error))
            {
                Out.WriteLine(error);

                Out.WriteLine("Ход игрока {0}:", game.CurrentPlayer);
                line = Out.ReadLine();

                command = CommandFactory.Create(line);
            }

            command.Do(game);
            command.Analize(game);
        }

        private void DrawBoard(Board board)
        {
            Out.Write("  ");
            for (int x = 0; x < Board.WIDTH; ++x)
            {
                Out.Write((char)('а' + x));
            }
            Out.WriteLine();
            Out.WriteLine();

            for (int y = 0; y < Board.HEIGHT; ++y)
            {
                Out.Write(y + 1);
                Out.Write(' ');

                for (int x = 0; x < Board.WIDTH; ++x)
                {
                    var ch = GetChar(board, x, y);
                    Out.Write(ch);
                }

                Out.WriteLine();
            }
        }

        private char GetChar(Board board, int x, int y)
        {
            if (board.IsFigure(x, y))
            {
                var figure = board.GetFigureType(x, y);
                switch (figure)
                {
                    case FigureType.Attacker:
                        return 'A';
                    case FigureType.Defender:
                        return 'D';
                    case FigureType.King:
                        return 'K';
                }
            }

            var cell = board.GetCellType(x, y);
            switch (cell)
            {
                case CellType.Cell:
                    return '.';
                case CellType.Throne:
                    return 'T';
                case CellType.Exit:
                    return 'X';
            }

            return '.';
        }
    }
}

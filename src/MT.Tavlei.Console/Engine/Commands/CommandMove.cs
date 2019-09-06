﻿using MT.Tavlei.Core;

namespace MT.Tavlei.Console.Engine.Commands
{
    class CommandMove : ICommand
    {
        private readonly int x0, y0;
        private readonly int x1, y1;

        public CommandMove(int x0, int y0, int x1, int y1)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
        }

        public bool Check(Game game, out string error)
        {
            error = "";

            if (!game.Board.IsFigure(x0, y0))
            {
                error = "В исходной клетке нет фигуры.";
                return false;
            }

            if (!game.Board.IsPlayerSide(x0, y0, game.CurrentPlayer))
            {
                error = "В исходной клетке фигура другого игрока.";
                return false;
            }

            if (game.Board.IsFigure(x1, y1))
            {
                error = "Клетка назначения занята.";
                return false;
            }

            return true;
        }

        public void Do(Game game)
        {
            game.Move(x0, y0, x1, y1);

            // TODO: сделать захват
            // TODO: сделать проверку на победу

            game.NextPlayer();
        }

        public static bool TryParse(string line, out CommandMove command)
        {
            command = null;
            try
            {
                line = line.Trim();
                var parts = line.Split(' ', '-', ':', 'x');

                if (parts.Length != 2)
                    return false;

                var part0 = parts[0];
                var part1 = parts[1];

                if (part0.Length != 2 || part1.Length != 2)
                    return false;

                int x0 = part0[0] - 'а';
                int y0 = int.Parse(part0[1].ToString()) - 1;

                int x1 = part1[0] - 'а';
                int y1 = int.Parse(part1[1].ToString()) - 1;

                command = new CommandMove(x0, y0, x1, y1);
            }
            catch
            {
            }
            return true;
        }
    }
}

using System;
using MT.Tavlei.Core;

namespace MT.Tavlei.Console.Engine.Commands
{
    class CommandExit : ICommand
    {
        public bool Check(Game game, out string error)
        {
            error = "";
            return true;
        }

        public void Do(Game game)
        {
            throw new GameCycleExitException();
        }

        public static bool TryParse(string line, out CommandExit command)
        {
            command = null;
            try
            {
                line = line.Trim();

                if (!line.Equals("выход", StringComparison.InvariantCultureIgnoreCase))
                    return false;

                command = new CommandExit();
            }
            catch
            {
            }
            return true;
        }
    }
}

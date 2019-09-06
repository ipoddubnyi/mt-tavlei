
namespace MT.Tavlei.Console.Engine.Commands
{
    static class CommandFactory
    {
        public static ICommand Create(string line)
        {
            if (CommandMove.TryParse(line, out var move))
                return move;

            if (CommandExit.TryParse(line, out var exit))
                return exit;

            return new CommandUnknown();
        }
    }
}

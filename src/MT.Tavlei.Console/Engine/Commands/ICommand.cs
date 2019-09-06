using MT.Tavlei.Core;

namespace MT.Tavlei.Console.Engine.Commands
{
    interface ICommand
    {
        bool Check(Game game, out string error);

        void Do(Game game);
    }
}

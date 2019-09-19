using MT.Tavlei.Core;

namespace MT.Tavlei.Console.Engine.Commands
{
    class CommandUnknown : ICommand
    {
        private const string Message = "Неизвестная команда или неверный формат.";

        public bool Check(Game game, out string error)
        {
            error = Message;
            return false;
        }

        public void Do(Game game)
        {
            throw new GameCycleException(Message);
        }

        public void Analize(Game game)
        {
        }
    }
}

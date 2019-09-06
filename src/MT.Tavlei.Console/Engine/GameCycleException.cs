using System;

namespace MT.Tavlei.Console.Engine
{
    class GameCycleException : Exception
    {
        public GameCycleException(string message) :
            base(message)
        {
        }
    }
}

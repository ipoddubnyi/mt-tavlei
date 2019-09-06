using System;
using Out = System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Tavlei.Core;
using MT.Tavlei.Core.Types;
using MT.Tavlei.Console.Engine;
using MT.Tavlei.Console.Engine.Commands;

namespace MT.Tavlei.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var game = new Game();
                var cycle = new GameCycle(game);
                cycle.Start();
            }
            catch (Exception ex)
            {
                Out.WriteLine(ex);
            }

            Out.WriteLine();
            Out.WriteLine("Нажмите любую клавишу для выхода...");
            Out.ReadKey();
        }
    }
}

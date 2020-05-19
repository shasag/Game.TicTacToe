using System;

namespace Game.TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            var uiInteractor = new ConsoleInteractor(Console.In, Console.Out);
            uiInteractor.Run();
        }
    }
}

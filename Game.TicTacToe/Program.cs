using System;

namespace Game.TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            TicTacToe ticTacToeGame = new TicTacToe();
            ticTacToeGame.InitalizeInputs();
            ticTacToeGame.StartGame();
            Console.ReadLine();
        }
    }
}

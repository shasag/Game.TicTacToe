using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.TicTacToe
{
    public class HumanPlayer : IPlayer
    {

        public CellOption PreferredSymbol { get; private set; }
        public string Name { get; private set; }
        public GameBoard Game { get; set; }
        private readonly TextReader Input;
        private readonly TextWriter Output;

        public HumanPlayer(string name, GameBoard game, char symbol, TextReader input, TextWriter output)
        {
            Name = name;
            Game = game;
            Input = input;
            Output = output;
            if (symbol == 'X')
                PreferredSymbol = CellOption.CrossCell;
            else
                PreferredSymbol = CellOption.NoughtCell;
        }

        public int TakeTurn(int difficultyLevel)
        {
            int cellNumber = -1;
            do
            {
                do
                {
                    Output.Write(MovePrompt());
                    int.TryParse(Input.ReadLine().Trim(), out cellNumber);
                }
                while (cellNumber == -1);
                
                Output.WriteLine();
            } while (!Game.IsValidMove(cellNumber));
            return cellNumber;
        }

        private string MovePrompt()
        {
            return $"{Name}, select your move : ";
        }
    }
}

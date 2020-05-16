using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe
{
    public class HumanPlayer : IPlayer
    {

        public CellOption PreferredSymbol { get; set; }
        public string Name { get; set; }

        public HumanPlayer(string name, char symbol)
        {
            Name = name;
            if (symbol == 'X')
                PreferredSymbol = CellOption.CrossCell;
            else
                PreferredSymbol = CellOption.NoughtCell;
        }

        public int TakeTurn()
        {
            int cellNumber = int.Parse(Console.ReadLine());
            return cellNumber;
        }
    }
}

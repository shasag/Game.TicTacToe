using Game.TicTacToe.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Interfaces
{
    public interface IPlayer
    {
        CellOption PreferredSymbol { get; set; }
        string Name { get; set; }

        public int TakeTurn();
    }
}

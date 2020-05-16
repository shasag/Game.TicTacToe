using Game.TicTacToe.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Interfaces
{
    public interface IPlayer
    {
        CellOption PreferredSymbol { get; }
        string Name { get; }

        public int TakeTurn();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Interfaces
{
    public interface IPlayer
    {
        char PreferredSymbol { get; set; }
        string Name { get; set; }

        public int TakeTurn();
    }
}

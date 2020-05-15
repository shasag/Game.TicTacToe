using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Interfaces
{
    interface IPlayer
    {
        string PreferredSymbol { get; set; }
        string Name { get; set; }
    }
}

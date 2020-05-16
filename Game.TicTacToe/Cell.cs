using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe
{
    public class Cell
    {
        CellOption cellState = CellOption.EmptyCell;

        public Cell()
        {
            cellState = CellOption.EmptyCell;
        }

        public CellOption GetCellState()
        {
            return cellState;
        }

        public bool IsEmpty()
        {
            if (cellState != CellOption.EmptyCell)
                return false;
            return true;
        }

        public void MarkCell(IPlayer player)
        {
            if (IsEmpty())
            {
                if (player.PreferredSymbol == 'X')
                    cellState = CellOption.CrossCell;
                else
                    cellState = CellOption.NoughtCell;
            }
        }
    }
}

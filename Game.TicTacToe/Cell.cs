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
        public int Row { get; set; }
        public int Col { get; set; }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
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

        public void ResetCell()
        {
            cellState = CellOption.EmptyCell;
        }

        public void MarkCell(CellOption playerSymbol)
        {
            if (IsEmpty())
            {
                cellState = playerSymbol;
            }
        }
    }
}

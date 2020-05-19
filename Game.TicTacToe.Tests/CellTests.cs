using Game.TicTacToe.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class CellTests
    {

        [TestMethod]
        public void GetCellStateTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.MarkCell(CellOption.CrossCell, 1);
            var cellCoord = gameBoardInstance.GetCoordinates(1);
            var cell = gameBoardInstance.Board[cellCoord.Key, cellCoord.Value];
            Assert.IsTrue(cell.GetCellState() != Enums.CellOption.EmptyCell);
        }


        [TestMethod]
        public void ResetCellTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.MarkCell(CellOption.CrossCell, 1);
            var cellCoord = gameBoardInstance.GetCoordinates(1);
            var cell = gameBoardInstance.Board[cellCoord.Key, cellCoord.Value];
            cell.ResetCell();

            Assert.IsTrue(cell.GetCellState() == Enums.CellOption.EmptyCell);
        }

        [TestMethod]
        public void MarkCellTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.MarkCell(CellOption.EmptyCell, 1);
            var cellCoord = gameBoardInstance.GetCoordinates(1);
            var cell = gameBoardInstance.Board[cellCoord.Key, cellCoord.Value];
            cell.MarkCell(Enums.CellOption.NoughtCell);

            Assert.IsTrue(cell.GetCellState() == Enums.CellOption.NoughtCell);
        }
    }
}

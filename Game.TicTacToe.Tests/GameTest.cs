using Game.TicTacToe.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void MarkTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 1);

            var cellStatus = gameBoardInstance.Board[0, 1].GetCellState();

            Assert.AreEqual(cellStatus == Enums.CellOption.EmptyCell, true);
        }

        [TestMethod]
        public void ChangePlayerToOTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerO = new HumanPlayer("TestPlayerO", 'O');
            gameBoardInstance.CurrentPlayer = testPlayerX;
            gameBoardInstance.ChangePlayer(testPlayerX, testPlayerO);

            Assert.AreEqual(gameBoardInstance.CurrentPlayer, testPlayerO);
        }

        [TestMethod]
        public void ChangePlayerToXTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerO = new HumanPlayer("TestPlayerO", 'O');
            gameBoardInstance.CurrentPlayer = testPlayerO;
            gameBoardInstance.ChangePlayer(testPlayerX, testPlayerO);

            Assert.AreEqual(gameBoardInstance.CurrentPlayer, testPlayerX);
        }

        [TestMethod]
        public void CheckWinRowTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 1);
            gameBoardInstance.MarkCell(testPlayer, 2);
            gameBoardInstance.MarkCell(testPlayer, 3);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void CheckWinColumnTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 1);
            gameBoardInstance.MarkCell(testPlayer, 4);
            gameBoardInstance.MarkCell(testPlayer, 7);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void DisplayBoardTest()
        {
            var currentConsoleOut = Console.Out;

            var gameBoardInstance = new GameBoard();
            

            //string text =   "   |   |   \n" +
            //                " 1 | 2 | 3 \n" +
            //                "___|___|___\n" +
            //                "   |   |   \n" +
            //                " 4 | 5 | 6 \n" +
            //                "___|___|___\n" +
            //                "   |   |   \n" +
            //                " 7 | 8 | 9 \n" +
            //                "   |   |   \n\n" ;

            using (var consoleOutput = new ConsoleOutput())
            {
                gameBoardInstance.DisplayBoard();
                Assert.AreEqual(consoleOutput.GetOuput().ToString().Trim().Contains("1"), true);
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void ClearBoardTest()
        {
            var currentConsoleOut = Console.Out;

            var gameBoardInstance = new GameBoard();


            string text =   "";

            using (var consoleOutput = new ConsoleOutput())
            {
                //gameBoardInstance.DisplayBoard();
                gameBoardInstance.ClearBoard();
                Assert.AreEqual(text, consoleOutput.GetOuput());
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void CheckWinLeftDigonalTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 1);
            gameBoardInstance.MarkCell(testPlayer, 5);
            gameBoardInstance.MarkCell(testPlayer, 9);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void CheckWinRightDigonalTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 3);
            gameBoardInstance.MarkCell(testPlayer, 5);
            gameBoardInstance.MarkCell(testPlayer, 7);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void ChekDrawTest()
        {
            var gameBoardInstance = new GameBoard();

            gameBoardInstance.MoveCounter = GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE;

            Assert.AreEqual(gameBoardInstance.CheckDraw(), true);
        }

        [TestMethod]
        public void AvailableMoveTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerY = new HumanPlayer("TestPlayerY", 'O');
            gameBoardInstance.MarkCell(testPlayerX, 1);
            gameBoardInstance.MarkCell(testPlayerY, 2);
            gameBoardInstance.MarkCell(testPlayerX, 3);
            gameBoardInstance.MarkCell(testPlayerY, 4);
            gameBoardInstance.MarkCell(testPlayerX, 5);
            gameBoardInstance.MarkCell(testPlayerY, 6);
            gameBoardInstance.MarkCell(testPlayerY, 7);
            var expectedList = new List<int> { 8, 9};
            var actualList = gameBoardInstance.GetAvailableMoves().ToList();
            Assert.IsFalse(expectedList.Except(actualList).Any());
            Assert.IsFalse(actualList.Except(expectedList).Any());
        }

        [TestMethod]
        public void BackTrackMovetest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX, 1);
            gameBoardInstance.MarkCell(testPlayerX, 2);
            gameBoardInstance.MarkCell(testPlayerX, 9);
            gameBoardInstance.BackTrackMove();
            var expectedList = new List<int>() { 3, 4, 5, 6, 7, 8, 9 };
            var actualList = gameBoardInstance.GetAvailableMoves().ToList();
            Assert.IsFalse(expectedList.Except(actualList).Any());
            Assert.IsFalse(actualList.Except(expectedList).Any());
        }

        [TestMethod]
        public void ChekMoveRemainingTest()
        {
            var gameBoardInstance = new GameBoard();

            gameBoardInstance.MoveCounter = GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE - 3;

            Assert.AreEqual(gameBoardInstance.IsMoveRemaining(), true);
        }

        [TestMethod]
        public void ChekMoveNotRemainingTest()
        {
            var gameBoardInstance = new GameBoard();

            gameBoardInstance.MoveCounter = GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE;

            Assert.AreEqual(gameBoardInstance.IsMoveRemaining(), false);
        }

        [TestMethod]
        public void GetOpponentSymbolForPlayerXTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerY = new HumanPlayer("TestPlayerY", 'O');
            gameBoardInstance.CurrentPlayer = testPlayerX;

            Assert.AreEqual(gameBoardInstance.GetOpponentSymbol(gameBoardInstance.CurrentPlayer.PreferredSymbol), testPlayerY.PreferredSymbol);
        }

        [TestMethod]
        public void GetOpponentSymbolForPlayerYTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerY = new HumanPlayer("TestPlayerY", 'O');
            gameBoardInstance.CurrentPlayer = testPlayerY;

            Assert.AreEqual(gameBoardInstance.GetOpponentSymbol(gameBoardInstance.CurrentPlayer.PreferredSymbol), testPlayerX.PreferredSymbol);
        }
    }
}

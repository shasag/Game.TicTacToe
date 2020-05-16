using Game.TicTacToe.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 1);

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
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 2);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 3);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void CheckWinColumnTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 4);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 7);
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
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 9);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void CheckWinRightDigonalTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 3);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 7);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void CheckNotWinRightDigonalTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 3);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayer.PreferredSymbol, 6);
            Assert.AreEqual(gameBoardInstance.CheckWin(), false);
        }

        [TestMethod]
        public void CheckDrawTest()
        {
            var gameBoardInstance = new GameBoard();

            //gameBoardInstance.IsMoveRemaining() = GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE;
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 1);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 2); 
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 3);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 4);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 5);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 6);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 7);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 8);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 9);
            Assert.AreEqual(gameBoardInstance.CheckDraw(), true);
        }

        [TestMethod]
        public void IsMoveRemainingTest()
        {
            var gameBoardInstance = new GameBoard();

            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 1);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 2);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 3);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 4);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 5);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 6);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 7);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 8);
            Assert.AreEqual(gameBoardInstance.IsMoveRemaining(), true);
        }

        [TestMethod]
        public void IsMoveNotRemainingTest()
        {
            var gameBoardInstance = new GameBoard();

            //gameBoardInstance.IsMoveRemaining() = GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE;
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 1);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 2);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 3);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 4);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 5);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 6);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 7);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 8);
            gameBoardInstance.MarkCell(Enums.CellOption.NoughtCell, 9);
            Assert.AreEqual(gameBoardInstance.IsMoveRemaining(), false);
        }

        [TestMethod]
        public void AvailableMoveTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerY = new HumanPlayer("TestPlayerY", 'O');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayerY.PreferredSymbol, 2);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 3);
            gameBoardInstance.MarkCell(testPlayerY.PreferredSymbol, 4);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayerY.PreferredSymbol, 6);
            gameBoardInstance.MarkCell(testPlayerY.PreferredSymbol, 7);
            var expectedList = new List<int> { 8, 9};
            var actualList = gameBoardInstance.GetUnPlayedMoves().ToList();
            Assert.IsFalse(expectedList.Except(actualList).Any());
            Assert.IsFalse(actualList.Except(expectedList).Any());
        }

        [TestMethod]
        public void BackTrackMovetest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 2);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 9);
            gameBoardInstance.BackTrackMove();
            var expectedList = new List<int>() { 3, 4, 5, 6, 7, 8, 9 };
            var actualList = gameBoardInstance.GetUnPlayedMoves().ToList();
            Assert.IsFalse(expectedList.Except(actualList).Any());
            Assert.IsFalse(actualList.Except(expectedList).Any());
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

        [TestMethod]
        public void TakeTurnAIWinTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerO = new AIPlayer(gameBoardInstance);
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 2);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 4);

            Assert.AreEqual(testPlayerO.TakeTurn(), 8);
        }

        [TestMethod]
        public void AIPlayer_ConstructurTest()
        {
            var gameBoardInstance = new GameBoard();
            AIPlayer player = new Mock<AIPlayer>(gameBoardInstance).Object;

            Assert.AreEqual(player.Name, "AI Player");
        }

        [TestMethod]
        public void HumanPlayer_ConstructurTes5()
        {
            HumanPlayer player = new Mock<HumanPlayer>("Test", 'X').Object;

            Assert.AreEqual(player.Name, "Test");
        }

        [TestMethod]
        public void GameBoard_ConstructurTest()
        {
            GameBoard gameBoard = new Mock<GameBoard>().Object;

            Assert.IsTrue(gameBoard.Board[0, 0].IsEmpty());
        }

        [TestMethod]
        public void Cell_ConstructurTest()
        {
            Cell cell = new Mock<Cell>(1, 1).Object;

            Assert.IsTrue(cell.GetCellState() == Enums.CellOption.EmptyCell);
        }

        [TestMethod]
        public void TakeTurnAISaveTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerO = new AIPlayer(gameBoardInstance);
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 3);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 7);

            Assert.AreEqual(testPlayerO.TakeTurn(), 4);
        }

        [TestMethod]
        public void IsCellEmptyTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);

            Assert.IsTrue(gameBoardInstance.IsCellEmpty(2));
        }

        [TestMethod]
        public void IsCellEmptyNotTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);

            Assert.IsFalse(gameBoardInstance.IsCellEmpty(1));
        }

        [TestMethod]
        public void GetCellStateTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            var cell = gameBoardInstance.Board[0, 0];

            Assert.IsTrue(cell.GetCellState() != Enums.CellOption.EmptyCell);
        }

        [TestMethod]
        public void IsEmptyTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            var cell = gameBoardInstance.Board[0, 0];

            Assert.IsTrue(cell.GetCellState() != Enums.CellOption.EmptyCell);
        }

        [TestMethod]
        public void ResetCellTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            var cell = gameBoardInstance.Board[0, 0];
            cell.ResetCell();

            Assert.IsTrue(cell.GetCellState() == Enums.CellOption.EmptyCell);
        }

        [TestMethod]
        public void NotMarkCellTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            var cell = gameBoardInstance.Board[0, 0];
            cell.MarkCell(Enums.CellOption.NoughtCell);

            Assert.IsFalse(cell.GetCellState() == Enums.CellOption.NoughtCell);
        }

        [TestMethod]
        public void MarkCellTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.MarkCell(Enums.CellOption.EmptyCell, 1);
            var cell = gameBoardInstance.Board[0, 0];
            cell.MarkCell(Enums.CellOption.NoughtCell);

            Assert.IsTrue(cell.GetCellState() == Enums.CellOption.NoughtCell);
        }
    }
}

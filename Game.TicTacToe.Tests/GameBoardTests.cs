using Game.TicTacToe.Enums;
using Game.TicTacToe.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class GameBoardTests
    {
        private GameBoard game;

        public void CreateGame()
        {
            game = new GameBoard();
        }

        [TestMethod]
        public void XPlayerGoesFirst()
        {
            CreateGame();
            Assert.AreEqual(game.CurrentSymbol, CellOption.CrossCell);
        }

        [TestMethod]
        public void OPlayerGoesSecond()
        {
            CreateGame();
            game.MarkCell(CellOption.CrossCell, 1);
            Assert.AreEqual(game.CurrentSymbol, CellOption.NoughtCell);
        }

        [TestMethod]
        public void NewGameIsNotOver()
        {
            CreateGame();
            Assert.AreEqual(game.IsOver(), false);
        }

        [TestMethod]
        public void GameIsOverWhenOnePlayerHasThreeInARow()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 8);
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            Assert.AreEqual(game.IsOver(), true);
        }

        [TestMethod]
        public void WinnerIfXGets123()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 8);
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            Assert.AreEqual(game.CheckWin(), true);
        }

        [TestMethod]
        public void WinnerIfUserGetsRow()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 5);
            game.MarkCell(Enums.CellOption.CrossCell, 9);
            game.MarkCell(Enums.CellOption.NoughtCell, 6);
            Assert.AreEqual(game.CheckWin(), true);
        }

        [TestMethod]
        public void WinnerIfOGetsBackwardDigonal()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            game.MarkCell(Enums.CellOption.NoughtCell, 1);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 9);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            Assert.AreEqual(game.CheckWin(), true);
        }

        [TestMethod]
        public void WinnerIfUserGetsColumn()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 1);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 8);
            Assert.AreEqual(game.CheckWin(), true);
        }

        [TestMethod]
        public void WinnerIfOGets369()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 3);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 6);
            game.MarkCell(Enums.CellOption.CrossCell, 8);
            game.MarkCell(Enums.CellOption.NoughtCell, 9);
            Assert.AreEqual(game.CheckWin(), true);
        }

        [TestMethod]
        public void AvailableMovesShouldShowAvailableMoves()
        {
            CreateGame();
            var game = new GameBoard();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 3);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 5);
            game.MarkCell(Enums.CellOption.CrossCell, 9);
            var expectedList = new List<int>() { 4, 6, 7, 8 };
            var actualList = game.GetUnPlayedMoves().ToList();
            Assert.IsFalse(actualList.Except(expectedList).Any());
            Assert.IsFalse(expectedList.Except(actualList).Any());
        }

        [TestMethod]
        public void IsMoveRemainingTest()
        {
            CreateGame();

            game.MarkCell(CellOption.CrossCell, 1);
            game.MarkCell(CellOption.NoughtCell, 2);
            game.MarkCell(CellOption.CrossCell, 3);
            game.MarkCell(CellOption.NoughtCell, 4);
            game.MarkCell(CellOption.CrossCell, 5);
            game.MarkCell(CellOption.NoughtCell, 6);
            game.MarkCell(CellOption.CrossCell, 8);
            game.MarkCell(CellOption.NoughtCell, 7);
            Assert.AreEqual(game.IsMoveRemaining(), true);
        }

        [TestMethod]
        public void AvailableMoveTest()
        {
            CreateGame();
            game.MarkCell(CellOption.CrossCell, 1);
            game.MarkCell(CellOption.NoughtCell, 2);
            game.MarkCell(CellOption.CrossCell, 3);
            game.MarkCell(CellOption.NoughtCell, 4);
            game.MarkCell(CellOption.CrossCell, 5);
            game.MarkCell(CellOption.NoughtCell, 6);
            game.MarkCell(CellOption.CrossCell, 8);
            var expectedList = new List<int> { 7, 9 };
            var actualList = game.GetUnPlayedMoves().ToList();
            Assert.IsFalse(expectedList.Except(actualList).Any());
            Assert.IsFalse(actualList.Except(expectedList).Any());
        }

        [TestMethod]
        public void BackTrackMovetest()
        {
            CreateGame();
            game.MarkCell(CellOption.CrossCell, 1);
            game.MarkCell(CellOption.NoughtCell, 2);
            game.MarkCell(CellOption.CrossCell, 9);
            game.BackTrackMove();
            var expectedList = new List<int>() { 3, 4, 5, 6, 7, 8, 9 };
            var actualList = game.GetUnPlayedMoves().ToList();
            Assert.IsFalse(expectedList.Except(actualList).Any());
            Assert.IsFalse(actualList.Except(expectedList).Any());
        }

        [TestMethod]
        public void GetOpponentSymbolForPlayerXTest()
        {
            CreateGame();
            //var input = new StubTextReader();
            //var output = new StringWriter();
            //var testPlayerX = new HumanPlayer("TestUser", game, 'X', input, output);
            game.CurrentSymbol = CellOption.CrossCell;
            Assert.AreEqual(game.GetOpponentSymbol(game.CurrentSymbol), CellOption.NoughtCell);
        }

        [TestMethod]
        public void GetOpponentSymbolForPlayerYTest()
        {
            CreateGame();
            //var input = new StubTextReader();
            //var output = new StringWriter();
            //var testPlayerX = new HumanPlayer("TestUser", game, 'X', input, output);
            game.CurrentSymbol = CellOption.NoughtCell;
            Assert.AreEqual(game.GetOpponentSymbol(game.CurrentSymbol), CellOption.CrossCell);
        }

        [TestMethod]
        public void IsCellEmptyTest()
        {
            CreateGame();
            game.MarkCell(CellOption.CrossCell, 1);
            Assert.IsTrue(game.IsCellEmpty(2));
        }

        [TestMethod]
        public void IsCellEmptyNotTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.MarkCell(CellOption.CrossCell, 1);
            Assert.IsFalse(gameBoardInstance.IsCellEmpty(1));
        }


        [TestMethod]
        public void GetMoveCoordintesTest()
        {
            var gameBoardInstance = new GameBoard();
            var coord = gameBoardInstance.GetCoordinates(1);
            var xCoord = 0;
            var yCoord = 0;
            Assert.IsTrue(coord.Key == xCoord && coord.Value == yCoord);
        }

        [TestMethod]
        public void ChangePlayerToXTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.CurrentSymbol = CellOption.NoughtCell;
            gameBoardInstance.ChangePlayers();

            Assert.AreEqual(gameBoardInstance.CurrentSymbol, CellOption.CrossCell);
        }

        [TestMethod]
        public void ChangePlayerToOTest()
        {
            var gameBoardInstance = new GameBoard();
            gameBoardInstance.CurrentSymbol = CellOption.CrossCell;
            gameBoardInstance.ChangePlayers();

            Assert.AreEqual(gameBoardInstance.CurrentSymbol, CellOption.NoughtCell);
        }

        [TestMethod]
        public void ValidMovesAreAllowed()
        {
            CreateGame();
            Assert.IsTrue(game.IsValidMove(1));
        }

        [TestMethod]
        public void MovesAlreadyTakenAreNotAllowed()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 3);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 5);
            game.MarkCell(Enums.CellOption.CrossCell, 9);
            Assert.IsFalse(game.IsValidMove(1));
        }

        [TestMethod]
        public void NewGameIsNotADraw()
        {
            CreateGame();
            Assert.IsFalse(game.CheckDraw());
        }

        [TestMethod]
        public void DrawIfNoWinnerAndNoAvailableMoves()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 2);
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            game.MarkCell(Enums.CellOption.CrossCell, 6);
            game.MarkCell(Enums.CellOption.NoughtCell, 9);
            game.MarkCell(Enums.CellOption.CrossCell, 8);
            Assert.IsTrue(game.CheckDraw());
        }

        [TestMethod]
        public void IsOverIfDraw()
        {
            CreateGame();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 2);
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            game.MarkCell(Enums.CellOption.CrossCell, 6);
            game.MarkCell(Enums.CellOption.NoughtCell, 9);
            game.MarkCell(Enums.CellOption.CrossCell, 8);
            Assert.IsTrue(game.IsOver());
        }
    }
}

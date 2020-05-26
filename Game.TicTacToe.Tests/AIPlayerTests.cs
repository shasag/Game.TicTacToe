using Game.TicTacToe.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class AIPlayerTests
    {
        private StubTextReader input;
        private StringWriter output;

        [TestMethod]
        public void AIShouldPickTheWinningMoveTest()
        {
            input = new StubTextReader();
            output = new StringWriter();
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", gameBoardInstance, 'X', input, output);
            var testPlayerO = new AIPlayer("AIUser", gameBoardInstance, 'O');
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 2);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 4);

            Assert.AreEqual(testPlayerO.TakeTurn(10), 8);
        }

        [TestMethod]
        public void AIShouldPickTheDefendingMoveTest()
        {
            input = new StubTextReader();
            output = new StringWriter();
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", gameBoardInstance, 'X', input, output);
            var testPlayerO = new AIPlayer("AIUser", gameBoardInstance, 'O');
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 5);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 1);
            gameBoardInstance.MarkCell(testPlayerO.PreferredSymbol, 3);
            gameBoardInstance.MarkCell(testPlayerX.PreferredSymbol, 7);

            Assert.AreEqual(testPlayerO.TakeTurn(10), 4);
        }

        [TestMethod]
        public void AIPlayerOConstructorTest()
        {
            var gameBoardInstance = new GameBoard();
            AIPlayer player = new Mock<AIPlayer>("AIUser", gameBoardInstance, 'O').Object;

            Assert.AreEqual(player.Name, "AIUser");
        }

        [TestMethod]
        public void AIPlayerXConstructorTest()
        {
            var gameBoardInstance = new GameBoard();
            AIPlayer player = new Mock<AIPlayer>("AIUser", gameBoardInstance, 'X').Object;

            Assert.AreEqual(player.Name, "AIUser");
        }
    }
}

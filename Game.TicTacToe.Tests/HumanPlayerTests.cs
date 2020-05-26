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
    public class HumanPlayerTests
    {
        private GameBoard game;
        private StubTextReader input;
        private StringWriter output;
        private HumanPlayer player;

        public void CreatePlayer()
        {
            game = new GameBoard();
            input = new StubTextReader();
            output = new StringWriter();
            player = new HumanPlayer("TestUser", game, 'X', input, output);
        }

        [TestMethod]
        public void GetNextMoveShouldPromptForMove()
        {
            CreatePlayer();
            input.WriteLine("1");
            player.TakeTurn(0);
            Assert.IsTrue(output.ToString().Contains("select your move "));
        }

        [TestMethod]
        public void GetNextMoveShouldOnlyShowAvailableMoves()
        {
            CreatePlayer();
            input.WriteLine("8");
            var game = new GameBoard();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 2);
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            player.TakeTurn(10);
            Assert.IsTrue(output.ToString().Contains("select your move"));
        }

        [TestMethod]
        public void GetNextMoveShouldReturnValueFromInput()
        {
            CreatePlayer();
            input.WriteLine("5");
            Assert.AreEqual(player.TakeTurn(10), 5);
        }

        [TestMethod]
        public void ShouldContinuePromptingOnInvalidInput()
        {
            CreatePlayer();
            //input.WriteLine("INVALID");
            input.WriteLine("1");

            var result = player.TakeTurn(10);

            var prompt = "select your move";
            Assert.IsTrue(output.ToString().Contains(prompt));
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void HumanPlayer_ConstructorXTest()
        {
            HumanPlayer player = new Mock<HumanPlayer>("TestUser", game, 'X', input, output).Object;
            Assert.AreEqual(player.Name, "TestUser");
        }
    }
}

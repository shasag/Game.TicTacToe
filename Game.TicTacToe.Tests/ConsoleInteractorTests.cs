﻿using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
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
    public class ConsoleInteractorTests
    {
        private StubTextReader input;
        private StringWriter output;
        private ConsoleInteractor ui;

        public void CreateUI()
        {
            input = new StubTextReader();
            output = new StringWriter();
            ui = new ConsoleInteractor(input, output);
        }

        [TestMethod]
        public void ShouldPromptForPlayer()
        {
            CreateUI();
            input.WriteLine("X");

            ui.SelectPlayers(null);

            Assert.IsTrue(output.ToString().StartsWith("select User Type (1. Human 2. Computer) "));
        }

        [TestMethod]
        public void ShouldContinuePromptingForPlayerUntilValidInput()
        {
            CreateUI();
            input.WriteLine("INVALID");
            input.WriteLine("X");

            ui.SelectPlayers(null);

            var prompt = "select User Type (1. Human 2. Computer) : ";// + Environment.NewLine;
            Assert.IsTrue(output.ToString().StartsWith(prompt));
        }

        [TestMethod]
        public void SelectingPlayerXAsHumanAndOtherAsAIPlayer()
        {
            CreateUI();
            input.WriteLine("1");
            input.WriteLine("Test");
            input.WriteLine("2");

            ui.SelectPlayers(null);
            ui.SelectPlayers(null);

            Assert.IsTrue(ui.GetPlayer(Enums.CellOption.CrossCell).GetType() == typeof(HumanPlayer));
            Assert.IsTrue(ui.GetPlayer(Enums.CellOption.NoughtCell).GetType() == typeof(AIPlayer));
        }

        //[TestMethod]
        //public void PlayShouldUseMovesFromPlayerUntilGameIsOver()
        //{
        //    CreateUI();
        //    var game = new GameBoard();
        //    ui.SetPlayer(StubPlayer.FromMoves("1", "2", "3"));
        //    ui.SetPlayer(StubPlayer.FromMoves("4", "5"));

        //    ui.Play(game);

        //    Assert.IsTrue(game.IsOver());
        //    Assert.IsTrue(game.GetUnPlayedMoves().ToList().Contains(1));// new List<int>() { 1, 4, 5 }));
        //}

        [TestMethod]
        public void PrintResultShouldShowDraw()
        {
            CreateUI();
            var game = new GameBoard();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 2);
            game.MarkCell(Enums.CellOption.CrossCell, 3);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);
            game.MarkCell(Enums.CellOption.CrossCell, 6);
            game.MarkCell(Enums.CellOption.NoughtCell, 9);
            game.MarkCell(Enums.CellOption.CrossCell, 8);

            ui.PrintResult(game);

            Assert.AreEqual(output.ToString(), "Draw!" + Environment.NewLine);
        }

        [TestMethod]
        public void GetUserTypeInputTest()
        {
            CreateUI();
            input.WriteLine("1");

            string actualUserType = ui.GetUserType();

            Assert.IsTrue(actualUserType == "1");
        }

        [TestMethod]
        public void PrintResultShouldShowWinner()
        {
            CreateUI();
            input.WriteLine("1");
            input.WriteLine("Test");
            input.WriteLine("2");

            ui.SelectPlayers(null);
            ui.SelectPlayers(null);

            var game = new GameBoard();
            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 3);
            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 5);
            game.MarkCell(Enums.CellOption.CrossCell, 9);
            game.MarkCell(Enums.CellOption.NoughtCell, 7);

            ui.PrintResult(game);

            Assert.IsTrue(output.ToString().Contains("Wins!"));
        }



        [TestMethod]
        public void AIUsersSimulationTest()
        {
            CreateUI();
            input.WriteLine("2");
            input.WriteLine("2");

            var game = new GameBoard();
            ui.SelectPlayers(game);
            ui.SelectPlayers(game);

            ui.Play(game);
            ui.PrintResult(game);
            Assert.AreEqual(output.ToString().Trim().Contains("Draw"), true);
        }

        [TestMethod]
        public void AIUserWinsBySecondColumnTest()
        {
            CreateUI();
            input.WriteLine("2");
            input.WriteLine("1");
            input.WriteLine("TestUser2");

            var game = new GameBoard();
            ui.SelectPlayers(game);
            ui.SelectPlayers(game);

            game.MarkCell(Enums.CellOption.CrossCell, 2);
            game.MarkCell(Enums.CellOption.NoughtCell, 1);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);

            ui.Play(game);
            ui.PrintResult(game);
            Assert.AreEqual(output.ToString().Trim().Contains("Wins"), true);
        }



        [TestMethod]
        public void AIUserGameDrawTest()
        {
            CreateUI();
            input.WriteLine("2");
            input.WriteLine("1");
            input.WriteLine("TestUser2");

            var game = new GameBoard();
            ui.SelectPlayers(game);
            ui.SelectPlayers(game);

            game.MarkCell(Enums.CellOption.CrossCell, 1);
            game.MarkCell(Enums.CellOption.NoughtCell, 2);
            game.MarkCell(Enums.CellOption.CrossCell, 5);
            game.MarkCell(Enums.CellOption.NoughtCell, 4);
            game.MarkCell(Enums.CellOption.CrossCell, 6);
            game.MarkCell(Enums.CellOption.NoughtCell, 3);
            game.MarkCell(Enums.CellOption.CrossCell, 7);
            game.MarkCell(Enums.CellOption.NoughtCell, 9);

            ui.Play(game);
            ui.PrintResult(game);
            Assert.AreEqual(output.ToString().Trim().Contains("Draw"), true);
        }

        [TestMethod]
        public void DisplayBoardTest()
        {
            CreateUI();
            var game = new GameBoard();
            ui.DisplayBoard(game);
            //string text =   "   |   |   \n" +
            //                " 1 | 2 | 3 \n" +
            //                "___|___|___\n" +
            //                "   |   |   \n" +
            //                " 4 | 5 | 6 \n" +
            //                "___|___|___\n" +
            //                "   |   |   \n" +
            //                " 7 | 8 | 9 \n" +
            //                "   |   |   \n\n" ;
            Assert.AreEqual(output.ToString().Trim().Contains("1"), true);
        }

        [TestMethod]
        public void ClearBoardTest()
        {
            CreateUI();
            var game = new GameBoard();
            ui.DisplayBoard(game);
            var currentConsoleOut = Console.Out;
            string text = "";

            using (var consoleOutput = new ConsoleOutput())
            {
                ui.ClearBoard();
                Assert.AreEqual(text, consoleOutput.GetOuput());
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        //public class StubPlayer : IPlayer
        //{
        //    private int index = 0;
        //    private readonly string[] moves;

        //    public CellOption PreferredSymbol { get; set; }

        //    public string Name { get; set; }

        //    public static StubPlayer FromMoves(params string[] moves)
        //    {
        //        return new StubPlayer(moves);
        //    }

        //    private StubPlayer(string[] moves)
        //    {
        //        this.moves = moves;
        //    }

        //    public virtual string GetNextMove()
        //    {
        //        var result = moves[index];
        //        index += 1;
        //        return result;
        //    }

        //    public int TakeTurn()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}

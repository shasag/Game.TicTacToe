using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Game.TicTacToe
{
    public class ConsoleInteractor
    {
        private readonly List<IPlayer> players = new List<IPlayer>();
        private readonly TextReader input;
        private readonly TextWriter output;

        public ConsoleInteractor(TextReader input, TextWriter output)
        {
            this.input = input;
            this.output = output;
        }

        public virtual IPlayer GetPlayer(CellOption symbol)
        {
            return players.SingleOrDefault(x => x.PreferredSymbol == symbol);
        }

        public virtual void SetPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public virtual void Run()
        {
            var game = new GameBoard();
            SelectPlayers(game);
            SelectPlayers(game);
            Play(game);
            PrintResult(game);
        }

        public virtual void SelectPlayers(GameBoard game)
        {
            string userName;
            char symbol = 'X';
            if(GetPlayer(CellOption.CrossCell) != null)
                symbol = 'O';
            var userType = GetUserType();
            if (userType == "1")
            {
                output.Write("Select player name: ");
                userName = input.ReadLine().ToUpper();
                output.WriteLine();
                SetPlayer(new HumanPlayer(userName, game, symbol, input, output));
            }
            else
            {
                userName = "AIUser_" + symbol;
                SetPlayer(new AIPlayer(userName, game, symbol));
            }
        }

        public virtual string GetUserType()
        {
            string userType;
            do
            {
                output.Write("select User Type (1. Human 2. Computer) : ");
                userType = input.ReadLine().ToUpper();
                output.WriteLine();
            }
            while (!"12".Contains(userType) && userType != "12");

            return userType;
        }

        public void DisplayBoard(GameBoard game)
        {
            //Using ASCII constant to ensure the single character in case we increase the dimension
            const int ASCII_CODE_0 = 48;
            int cellNumber = 1;
            for (int i = 0; i < GameBoard.BOARD_SIZE; i++)
            {
                output.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("   |", GameBoard.BOARD_SIZE - 1)), "   "));
                for (int j = 0; j < GameBoard.BOARD_SIZE; j++)
                {
                    if (game.Board[i, j].IsEmpty())
                        output.Write($" {(char)(ASCII_CODE_0 + cellNumber)} ");
                    else
                        output.Write($" {(char)game.Board[i, j].GetCellState()} ");
                    cellNumber++;

                    if (j < GameBoard.BOARD_SIZE - 1)
                        output.Write("|");
                }
                output.Write("\n");
                if (i < GameBoard.BOARD_SIZE - 1)
                    output.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("___|", GameBoard.BOARD_SIZE - 1)), "___"));
                else if (i == GameBoard.BOARD_SIZE - 1)
                    output.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("   |", GameBoard.BOARD_SIZE - 1)), "   "));
            }
            output.WriteLine();
        }

        public void ClearBoard()
        {
            Console.Clear();
        }

        public virtual void Play(GameBoard game)
        {
            ClearBoard();
            DisplayBoard(game);
            while (!game.IsOver())
            {
                var move = GetPlayer(game.CurrentSymbol).TakeTurn();
                game.MarkCell(game.CurrentSymbol, move);
                ClearBoard();
                DisplayBoard(game);
            }
        }

        public virtual void PrintResult(GameBoard game)
        {
            if (game.CheckDraw())
                output.WriteLine("Draw!");
            else
                output.WriteLine("Player {0} Wins!", GetPlayer(game.CurrentSymbol).Name);
        }
    }
}

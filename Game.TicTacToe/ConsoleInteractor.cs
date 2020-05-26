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
            int difficultyLevel = 0;
            var game = new GameBoard(SelectBoardSize());
            SelectPlayers(game);
            SelectPlayers(game);
            if (players.Any(x => x.GetType() == typeof(AIPlayer)))
                difficultyLevel = SelectDifficultyLevel();
            Play(game, difficultyLevel);
            PrintResult(game);
        }

        public virtual int SelectBoardSize()
        {
            int boardSize = 3;
            do
            {
                output.Write("Select board size (3, 5, 7): ");
                int.TryParse(input.ReadLine().Trim(), out boardSize);
                output.WriteLine();
            }
            while (boardSize < 0 || boardSize == 1 || boardSize % 2 == 0);
            output.WriteLine();
            return boardSize;
        }

        public virtual int SelectDifficultyLevel()
        {
            int level;
            string levelString;
            do
            {
                output.Write("Select difficulty level (B(begineer), M(medium), E(expert)): ");
                levelString = input.ReadLine().Trim();
                if (levelString == "B")
                    level = 0;
                else if (levelString == "M")
                    level = 5;
                else if (levelString == "E")
                    level = 100;
                else
                    level = 0;
                output.WriteLine();
            }
            while (levelString != "B" && levelString != "M" && levelString != "E");
            output.WriteLine();
            return level;
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
                userName = string.Empty;
                do
                {
                    output.Write("Select player name: ");
                    userName = input.ReadLine().ToUpper();
                    output.WriteLine();
                }
                while (string.IsNullOrWhiteSpace(userName.Trim()));
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
            //const int ASCII_CODE_0 = 48;
            int cellNumber = 1;
            for (int i = 0; i < game.BOARD_SIZE; i++)
            {
                output.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("   |", game.BOARD_SIZE - 1)), "   "));
                for (int j = 0; j < game.BOARD_SIZE; j++)
                {
                    if (game.Board[i, j].IsEmpty())
                        output.Write($"   ");//output.Write($" {(char)(ASCII_CODE_0 + cellNumber)} ");
                    else
                        output.Write($" {(char)game.Board[i, j].GetCellState()} ");
                    cellNumber++;

                    if (j < game.BOARD_SIZE - 1)
                        output.Write("|");
                }
                output.Write("\n");
                if (i < game.BOARD_SIZE - 1)
                    output.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("___|", game.BOARD_SIZE - 1)), "___"));
                else if (i == game.BOARD_SIZE - 1)
                    output.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("   |", game.BOARD_SIZE - 1)), "   "));
            }
            output.WriteLine();
            output.WriteLine($"Allowed Moves {1} TO {game.BOARD_SIZE * game.BOARD_SIZE}");
            output.WriteLine();
        }

        public void ClearBoard()
        {
            Console.Clear();
        }

        public virtual void Play(GameBoard game, int difficultyLevel)
        {
            ClearBoard();
            DisplayBoard(game);
            while (!game.IsOver())
            {
                var move = GetPlayer(game.CurrentSymbol).TakeTurn(difficultyLevel);
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
                output.WriteLine("Player {0} Wins!", GetPlayer(game.GetOpponentSymbol(game.CurrentSymbol)).Name);
        }
    }
}

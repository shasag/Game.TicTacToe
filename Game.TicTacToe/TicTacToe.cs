using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe
{
    public class TicTacToe
    {
        public string ErrorMessage { get; set; }
        public TicTacToe()
        {
            StartGame();
        }
        private void StartGame()
        {
            GameBoard gameBoard = new GameBoard();
            IPlayer playerX = InitializeHumanUser(gameBoard, 1);
            Console.WriteLine($"Play Type : \nPress 1 for 2-Player game\n" +
                                              "Press any other key to play with computer");
            var type = Console.ReadLine();
            IPlayer playerY;
            if(type == "1")
                playerY = InitializeHumanUser(gameBoard, 2);
            else
                playerY = InitializeAIUser(gameBoard, 2);
            Console.Clear();
            gameBoard.CurrentPlayer = playerX;
            bool play = true;
            while (play)
            {
                gameBoard.DisplayBoard();
                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                {
                    Console.WriteLine(ErrorMessage);
                    ErrorMessage = string.Empty;
                }
                Console.WriteLine($"Player : {gameBoard.CurrentPlayer.Name} Enter the field in which you want to put character: ");
                try
                {
                    var turnValue = gameBoard.CurrentPlayer.TakeTurn();

                    if (turnValue > GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE)
                    {
                        ErrorMessage = "Invalid entry. Try again!!";
                        Console.Clear();
                        continue;
                    }
                    else if (!gameBoard.IsCellEmpty(turnValue)) 
                    {
                        ErrorMessage = "The cell poisition is already played. Try again!!";
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        gameBoard.MarkCell(gameBoard.CurrentPlayer.PreferredSymbol, turnValue);
                        gameBoard.ClearBoard();

                        if (gameBoard.CheckWin())
                        {
                            Console.WriteLine($"Player {gameBoard.CurrentPlayer.Name} with symbol {gameBoard.CurrentPlayer.PreferredSymbol} has won !!");
                            gameBoard.DisplayBoard();
                            play = false;
                        }
                        else if (gameBoard.CheckDraw())
                        {
                            Console.WriteLine($"GAME DRAW");
                            gameBoard.DisplayBoard();
                            play = false;
                        }

                        gameBoard.ChangePlayer(playerX, playerY);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Invalid Input");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private IPlayer InitializeAIUser(GameBoard gameBoard, int v)
        {
            Console.Clear();
            return new AIPlayer(gameBoard);
        }

        private IPlayer InitializeHumanUser(GameBoard gameBoard, int i)
        {
            Console.Clear();
            var prefSymbol = 'X';
            if (i == 2)
                prefSymbol = 'O';
            gameBoard.DisplayBoard();
            Console.WriteLine($"Player {i} Name with symbol ({prefSymbol}): ");
            var name = Console.ReadLine();
            return new HumanPlayer(name, prefSymbol);
        }
    }
}

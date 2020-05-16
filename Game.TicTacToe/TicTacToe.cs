using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe
{
    public class TicTacToe
    {
        public TicTacToe()
        {
            StartGame();
        }

        private void StartGame()
        {
            GameBoard gameBoard = new GameBoard();
            IPlayer playerX = InitializeHumanUser(gameBoard, 1);
            Console.WriteLine($"Play Type : \n1. 2-Player \n2. Play with computer");
            var type = Console.ReadLine();
            IPlayer playerY;
            if(type == "1")
                playerY = InitializeHumanUser(gameBoard, 2);
            else
                playerY = InitializeHumanUser(gameBoard, 2);
            Console.Clear();
            gameBoard.CurrentPlayer = playerX;
            gameBoard.MoveCounter = 0;
            bool play = true;
            while (play)
            {
                gameBoard.DisplayBoard();
                Console.WriteLine($"Player : {gameBoard.CurrentPlayer.Name} Enter the field in which you want to put character: ");
                try
                {
                    gameBoard.MarkCell(gameBoard.CurrentPlayer, gameBoard.CurrentPlayer.TakeTurn());
                    gameBoard.ClearBoard();
                    gameBoard.MoveCounter++;

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
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
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

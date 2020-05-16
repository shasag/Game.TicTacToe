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
            IPlayer playerY = InitializeHumanUser(gameBoard, 2);
            Console.Clear();
            gameBoard.CurrentPlayer = playerX;
            bool play = true;
            int moveCounter = 0;
            while (play)
            {
                gameBoard.DisplayBoard();
                Console.WriteLine($"Player : {gameBoard.CurrentPlayer.Name} Enter the field in which you want to put character: ");
                try
                {
                    gameBoard.MarkCell(gameBoard.CurrentPlayer, gameBoard.CurrentPlayer.TakeTurn());
                    gameBoard.ClearBoard();
                    moveCounter++;

                    if (gameBoard.CheckWin())
                    {
                        Console.WriteLine($"Player {gameBoard.CurrentPlayer.Name} with symbol {gameBoard.CurrentPlayer.PreferredSymbol} has won !!");
                        gameBoard.DisplayBoard();
                        play = false;
                    }
                    else if (gameBoard.CheckDraw(moveCounter))
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

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
            IPlayer currentPlayer = playerX;
            bool play = true;
            int moveCounter = 0;
            while (play)
            {
                gameBoard.DisplayBoard();
                Console.WriteLine($"Player : {currentPlayer.Name} Enter the field in which you want to put character: ");
                try
                {
                    gameBoard.MarkCell(currentPlayer, currentPlayer.TakeTurn());
                    gameBoard.ClearBoard();
                    moveCounter++;

                    if (gameBoard.CheckWin())
                    {
                        Console.WriteLine($"Player {currentPlayer.Name} with symbol {currentPlayer.PreferredSymbol} has won !!");
                        gameBoard.DisplayBoard();
                        play = false;
                    }
                    else if (gameBoard.CheckDraw(moveCounter))
                    {
                        Console.WriteLine($"GAME DRAW");
                        gameBoard.DisplayBoard();
                        play = false;
                    }

                    currentPlayer = ChangePlayer(currentPlayer, playerX, playerY);

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

        private IPlayer ChangePlayer(IPlayer currentPlayer, IPlayer playerX, IPlayer playerY)
        {
            return currentPlayer == playerX ? playerY : playerX;
        }
    }
}

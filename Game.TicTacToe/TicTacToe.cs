using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe
{
    public class TicTacToe
    {
        public string ErrorMessage { get; set; }
        public IPlayer PlayerX { get; set; }
        public IPlayer PlayerO { get; set; }
        public GameBoard GameBoard { get; set; }

        public TicTacToe()
        {
            //StartGame();
        }

        public void InitalizeInputs()
        {
            GameBoard = new GameBoard();
            PlayerX = InitializeHumanUser(GameBoard, 1);
            Console.WriteLine($"Play Type : \nPress 1 for 2-Player game\n" +
                                              "Press any other key to play with computer");
            var type = Console.ReadLine();
            if (type == "1")
                PlayerO = InitializeHumanUser(GameBoard, 2);
            else
                PlayerO = InitializeAIUser(GameBoard, 2);
            Console.Clear();
            GameBoard.CurrentPlayer = PlayerX;
        }

        public void StartGame()
        {
            bool play = PlayerX != null && PlayerO != null ? true : false;
            while (play)
            {
                GameBoard.DisplayBoard();
                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                {
                    Console.WriteLine(ErrorMessage);
                    ErrorMessage = string.Empty;
                }
                Console.WriteLine($"Player : {GameBoard.CurrentPlayer.Name} Enter the field in which you want to put character: ");
                try
                {
                    var turnValue = GameBoard.CurrentPlayer.TakeTurn();

                    if (turnValue > GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE)
                    {
                        ErrorMessage = "Invalid entry. Try again!!";
                        Console.Clear();
                        continue;
                    }
                    else if (!GameBoard.IsCellEmpty(turnValue)) 
                    {
                        ErrorMessage = "The cell poisition is already played. Try again!!";
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        GameBoard.MarkCell(GameBoard.CurrentPlayer.PreferredSymbol, turnValue);
                        GameBoard.ClearBoard();

                        if (GameBoard.CheckWin())
                        {
                            Console.WriteLine($"Player {GameBoard.CurrentPlayer.Name} with symbol {GameBoard.CurrentPlayer.PreferredSymbol} has won !!");
                            GameBoard.DisplayBoard();
                            play = false;
                        }
                        else if (GameBoard.CheckDraw())
                        {
                            Console.WriteLine($"GAME DRAW");
                            GameBoard.DisplayBoard();
                            play = false;
                        }

                        GameBoard.ChangePlayer(PlayerX, PlayerO);
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

        private IPlayer InitializeAIUser(GameBoard GameBoard, int v)
        {
            Console.Clear();
            return new AIPlayer(GameBoard);
        }

        private IPlayer InitializeHumanUser(GameBoard GameBoard, int i)
        {
            Console.Clear();
            var prefSymbol = 'X';
            if (i == 2)
                prefSymbol = 'O';
            GameBoard.DisplayBoard();
            Console.WriteLine($"Player {i} Name with symbol ({prefSymbol}): ");
            var name = Console.ReadLine();
            return new HumanPlayer(name, prefSymbol);
        }
    }
}

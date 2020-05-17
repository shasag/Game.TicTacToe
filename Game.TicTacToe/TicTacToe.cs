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
        public bool Play { get; set; }

        public TicTacToe()
        {
            //StartGame();
        }

        public void InitalizeInputs()
        {
            GameBoard = new GameBoard();
            PlayerX = GetHumanUser(GameBoard, 1);
            Console.WriteLine($"Play Type : \nPress 1 for 2-Player game\n" +
                                              "Press any other key to play with computer");
            var type = Console.ReadLine();
            if (type == "1")

                PlayerO = GetHumanUser(GameBoard, 2);
            else
                PlayerO = InitializeAIUser(GameBoard, 2);
            Console.Clear();
            GameBoard.CurrentPlayer = PlayerX;
        }

        public void StartGame()
        {
            Play = (PlayerX != null && PlayerO != null) ? true : false;
            while (Play)
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

                    if (!GameBoard.IsValidMove(turnValue))
                    {
                        ErrorMessage = "Invalid entry / move already played. Try again!!";
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
                            Play = false;
                        }
                        else if (GameBoard.CheckDraw())
                        {
                            Console.WriteLine($"GAME DRAW");
                            GameBoard.DisplayBoard();
                            Play = false;
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

        public IPlayer InitializeAIUser(GameBoard GameBoard, int v)
        {
            Console.Clear();
            return new AIPlayer(GameBoard);
        }

        public IPlayer InitializeHumanUser(GameBoard GameBoard, string name, int i)
        {
            return new HumanPlayer(name, GetSymbolForPlayer(i));
        }

        private IPlayer GetHumanUser(GameBoard GameBoard, int i)
        {
            Console.Clear();
            GameBoard.DisplayBoard();
            Console.WriteLine($"Player {i} Name with symbol ({GetSymbolForPlayer(i)}): ");
            var name = Console.ReadLine();
            return InitializeHumanUser(GameBoard, name, i);
        }

        public char GetSymbolForPlayer(int i)
        {
            return (i == 1 ? 'X' : 'O');
        }
    }
}

using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.TicTacToe
{
    public class GameBoard
    {
        public const int BOARD_SIZE = 3;

        public Cell[,] Board { get; set; }

        public IPlayer CurrentPlayer { get; set; }

        private Stack<Cell> _moveStack;

        public GameBoard()
        {
            _moveStack = new Stack<Cell>();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            Board = new Cell[BOARD_SIZE, BOARD_SIZE];
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Board[i, j] = new Cell(i, j);
                }
            }
        }

        public void DisplayBoard()
        {
            //Using ASCII constant to ensure the single character in case we increase the dimension
            const int ASCII_CODE_0 = 48;
            int cellNumber = 1;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                Console.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("   |", BOARD_SIZE -1)), "   "));
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (Board[i, j].IsEmpty())
                        Console.Write($" {(char)(ASCII_CODE_0 + cellNumber)} ");
                    else
                        Console.Write($" {(char)Board[i, j].GetCellState()} ");
                    cellNumber++;

                    if (j < BOARD_SIZE - 1)
                        Console.Write("|");
                }
                Console.Write("\n");
                if (i < BOARD_SIZE - 1)
                    Console.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("___|", BOARD_SIZE - 1)), "___"));
                else if (i == BOARD_SIZE - 1)
                    Console.WriteLine(string.Concat(string.Concat(Enumerable.Repeat("   |", BOARD_SIZE - 1)), "   "));
            }
            Console.WriteLine();
        }

        public void ClearBoard()
        {
            Console.Clear();
        }

        public CellOption GetOpponentSymbol(CellOption currentPlayerSymbol)
        {
            if (currentPlayerSymbol == CellOption.CrossCell)
                return  CellOption.NoughtCell;
            else
                return CellOption.CrossCell;
        }

        public void ChangePlayer(IPlayer playerX, IPlayer playerY)
        {
            if (CurrentPlayer == playerX)
            {
                CurrentPlayer = playerY;
            }
            else
            {
                CurrentPlayer = playerX;
            }
        }

        public IEnumerable<int> GetUnPlayedMoves()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (Board[i, j].IsEmpty())
                        yield return (i * BOARD_SIZE + j + 1);
                }
            }
        }

        public void BackTrackMove()
        {
            var popedCell = _moveStack.Pop();
            Board[popedCell.Row, popedCell.Col].ResetCell();
        }

        public bool CheckDraw()
        {
            return IsMoveRemaining() ? false : true;
        }

        public bool IsMoveRemaining()
        {
            return _moveStack.Count() < BOARD_SIZE * BOARD_SIZE;
        }

        public bool IsValidMove(int moveValue)
        {
            var isValidMove = true;
            if (moveValue > GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE || moveValue < 1)
            {
                isValidMove = false;
            }
            else if (!IsCellEmpty(moveValue))
            {
                isValidMove = false;
            }
            return isValidMove;
        }

        public bool IsCellEmpty(int cNum)
        {
            int xPos = (cNum - 1) / BOARD_SIZE;
            int yPos = (cNum - 1) % BOARD_SIZE;

            return Board[xPos, yPos].IsEmpty();
        }

        public void MarkCell(CellOption playerSymbol, int cNum)
        {
            int xPos = (cNum - 1) / BOARD_SIZE;
            int yPos = (cNum - 1) % BOARD_SIZE;

            Board[xPos, yPos].MarkCell(playerSymbol);
            _moveStack.Push(Board[xPos, yPos]);
        }

        public bool CheckWin()
        {
            return checkRowsForWin()
                    || checkColumnsForWin()
                    || checkDigonalsForWin();
        }

        #region Helpers

        private bool checkDigonalsForWin()
        {
            return checkDigonals(BOARD_SIZE);
            //return checkPattern(Board[0, 0].GetCellState(), Board[1, 1].GetCellState(), Board[2, 2].GetCellState()) ||
            //    checkPattern(Board[2, 0].GetCellState(), Board[1, 1].GetCellState(), Board[0, 2].GetCellState());
        }

        private bool checkColumnsForWin()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if (checkPattern(false, i, BOARD_SIZE))
                    //if (checkPattern(Board[0, i].GetCellState(), Board[1, i].GetCellState(), Board[2, i].GetCellState()))
                    return true;

            }
            return false;
        }

        private bool checkRowsForWin()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if(checkPattern(true, i, BOARD_SIZE))
                //if (checkPattern(Board[i, 0].GetCellState(), Board[i, 1].GetCellState(), Board[i, 2].GetCellState()))
                    return true;

            }
            return false;
        }

        private bool checkPattern(bool isRow, int position, int bOARD_SIZE)
        {
            var lstCells = new List<CellOption>();
            for (int i = 0; i < bOARD_SIZE; i++)
            {
                if (isRow)
                    lstCells.Add(Board[position, i].GetCellState());
                else
                    lstCells.Add(Board[i, position].GetCellState());
            }

            if (lstCells.Any(x => x == CellOption.EmptyCell) || lstCells.Any(o => o != lstCells[0]))
                return false;
            else
                return true;
        }

        private bool checkDigonals(int boardSize)
        {
            var retValLeft = false;
            var retValRight = false;
            var lstCellsLeft = new List<CellOption>();
            var lstCellsRight = new List<CellOption>();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if(i == j)
                        lstCellsLeft.Add(Board[i, j].GetCellState());
                    if((i == j && i+j == boardSize -1) || i + j == boardSize - 1)
                        lstCellsRight.Add(Board[i, j].GetCellState());
                }
            }

            if (lstCellsLeft.Any(x => x == CellOption.EmptyCell) || lstCellsLeft.Any(o => o != lstCellsLeft[0]))
                retValLeft =  false;
            else
                retValLeft = true;

            if (lstCellsRight.Any(x => x == CellOption.EmptyCell) || lstCellsRight.Any(o => o != lstCellsRight[0]))
                retValRight = false;
            else
                retValRight = true;

            return retValLeft || retValRight;
        }

        //private bool checkPattern(CellOption cellOption1, CellOption cellOption2, CellOption cellOption3)
        //{
        //    return cellOption1 != CellOption.EmptyCell && cellOption2 != CellOption.EmptyCell && cellOption3 != CellOption.EmptyCell
        //            && cellOption1 == cellOption2 && cellOption2 == cellOption3;
        //}

        #endregion
    }
}

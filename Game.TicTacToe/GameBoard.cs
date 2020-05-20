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

        public CellOption CurrentSymbol { get; set; }

        private Stack<Cell> _moveStack;

        public GameBoard()
        {
            CurrentSymbol = CellOption.CrossCell;
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

        public KeyValuePair<int, int> GetCoordinates(int cNum)
        {
            int xPos = (cNum - 1) / BOARD_SIZE;
            int yPos = (cNum - 1) % BOARD_SIZE;
            return new KeyValuePair<int, int>(xPos, yPos);
        }

        public CellOption GetOpponentSymbol(CellOption currentPlayerSymbol)
        {
            if (currentPlayerSymbol == CellOption.CrossCell)
                return  CellOption.NoughtCell;
            else
                return CellOption.CrossCell;
        }

        public void ChangePlayers()
        {
            CurrentSymbol = (CurrentSymbol == CellOption.CrossCell ? CellOption.NoughtCell : CellOption.CrossCell);
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
            ChangePlayers();
        }

        public virtual bool IsOver()
        {
            return CheckDraw() || CheckWin();
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
            var getCoord = GetCoordinates(cNum);
            return Board[getCoord.Key, getCoord.Value].IsEmpty();
        }

        private void RecordMove(int cNum)
        {
            var getCoord = GetCoordinates(cNum);
            _moveStack.Push(Board[getCoord.Key, getCoord.Value]);
        }

        public void MarkCell(CellOption playerSymbol, int cNum)
        {
            var getCoord = GetCoordinates(cNum);
            Board[getCoord.Key, getCoord.Value].MarkCell(playerSymbol);
            RecordMove(cNum);
            ChangePlayers();
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
        }

        private bool checkColumnsForWin()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if (checkPattern(false, i, BOARD_SIZE))
                    return true;

            }
            return false;
        }

        private bool checkRowsForWin()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if(checkPattern(true, i, BOARD_SIZE))
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
                    if(i + j == boardSize - 1)
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

        #endregion
    }
}

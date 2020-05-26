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
        private const int _minimuBoardSize = 3;
        private int _matchLength;
        public int BOARD_SIZE { get; private set; }

        public Cell[,] Board { get; set; }

        public CellOption CurrentSymbol { get; set; }

        private Stack<Cell> _moveStack;

        public GameBoard()
        {
            BOARD_SIZE = _minimuBoardSize;
            _matchLength = _minimuBoardSize;
            CurrentSymbol = CellOption.CrossCell;
            _moveStack = new Stack<Cell>();
            InitializeBoard();
        }

        public GameBoard(int size)
        {
            BOARD_SIZE = size;

            _matchLength = _minimuBoardSize;
            if (BOARD_SIZE > _minimuBoardSize)
                _matchLength = _minimuBoardSize + 1;

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
            if (moveValue > BOARD_SIZE * BOARD_SIZE || moveValue < 1)
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

            if (IsPatternFound(lstCells))
                return true;
            else
                return false;
        }

        private bool checkDigonals(int boardSize)
        {
            var retValLeft = false;
            var retValRight = false;
            var lstCellsLeftUpper = new List<CellOption>();
            var lstCellsLeft = new List<CellOption>();
            var lstCellsLeftLower = new List<CellOption>();
            var lstCellsRightUpper = new List<CellOption>();
            var lstCellsRight = new List<CellOption>();
            var lstCellsRightLower = new List<CellOption>();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if(i == j)
                        lstCellsLeft.Add(Board[i, j].GetCellState());
                    if (i + 1 == j)
                        lstCellsLeftLower.Add(Board[i, j].GetCellState());
                    if (i == j + 1)
                        lstCellsLeftUpper.Add(Board[i, j].GetCellState());
                    if (i + j == boardSize - 1)
                        lstCellsRight.Add(Board[i, j].GetCellState());
                    if (i + j == boardSize - 2)
                        lstCellsRightUpper.Add(Board[i, j].GetCellState());
                    if (i + j == boardSize)
                        lstCellsRightLower.Add(Board[i, j].GetCellState());
                }
            }

            retValLeft = IsPatternFound(lstCellsLeft);
            retValLeft = retValLeft || IsPatternFound(lstCellsLeftUpper);
            retValLeft = retValLeft || IsPatternFound(lstCellsLeftLower);

            retValRight = IsPatternFound(lstCellsRight);
            retValRight = retValRight || IsPatternFound(lstCellsRightUpper);
            retValRight = retValRight || IsPatternFound(lstCellsRightLower);

            return retValLeft || retValRight;
        }

        public bool IsPatternFound(List<CellOption> lstSymbol)
        {
            if (lstSymbol.Count() < _matchLength)
                return false;
            var currentCount = 0;
            var symbol = CellOption.NoughtCell;
            for (int i = 0; i < _matchLength; i++)
            {
                if (lstSymbol[i] == symbol)
                    currentCount++;
                else
                    currentCount = 0;
            }

            if (currentCount < _matchLength)
            {
                currentCount = 0;
                symbol = CellOption.CrossCell;
                for (int i = 0; i < _matchLength; i++)
                {
                    if (lstSymbol[i] == symbol)
                        currentCount++;
                    else
                        currentCount = 0;
                }
            }

            return currentCount >= _matchLength;
        }

        #endregion
    }
}

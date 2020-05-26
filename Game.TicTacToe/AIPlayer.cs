using Game.TicTacToe.Entities;
using Game.TicTacToe.Enums;
using Game.TicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace Game.TicTacToe
{
    public class AIPlayer : IPlayer
    {
        private enum MinMax { Min, Max}
        public CellOption PreferredSymbol { get; private set; }
        public string Name { get; private set; }
        public GameBoard Board { get; private set; }

        public AIPlayer(string name, GameBoard board, char symbol)
        {
            Name = name;
            Board = board;
            if (symbol == 'X')
                PreferredSymbol = CellOption.CrossCell;
            else
                PreferredSymbol = CellOption.NoughtCell;
        }

        public int TakeTurn(int difficultyLevel)
        {
            MovePrompt();

            int cellNumber = ComputeBestMove(MinMax.Max, 0, difficultyLevel).Move;
            var lm = Board.GetUnPlayedMoves().ToList();
            if (cellNumber == 0)
            {
                var randomMove = new Random();
                var randomMoveIndex = randomMove.Next(lm.Count());
                cellNumber = lm[randomMoveIndex];
            }
            return cellNumber;
        }

        private string MovePrompt()
        {
            return $"{Name}, select your move : ";
        }

        private LevelResult ComputeBestMove(MinMax levelType, int depth, int maxDepth)
        {
            var nextLevelType = MinMax.Min;
            var symbol = this.PreferredSymbol;
            var winningScore = 1000000000;
            LevelResult result = new LevelResult(depth, -10, 0);
            if (levelType == MinMax.Min)
            {
                symbol = Board.GetOpponentSymbol(this.PreferredSymbol);
                nextLevelType = MinMax.Max;
                winningScore = -1000000000;
                result = new LevelResult(depth, 10, 0);
            }
            var lm = Board.GetUnPlayedMoves().ToList();
            foreach (var move in lm)
            {
                Board.MarkCell(symbol, move);
                if (Board.CheckWin())
                {
                    Board.BackTrackMove();
                    return new LevelResult(depth, winningScore, move); ;
                }
                if (depth > maxDepth)
                {
                    Board.BackTrackMove();
                    var randomMove = new Random();
                    var randomMoveIndex = randomMove.Next(lm.Count());
                    return new LevelResult(depth, winningScore, lm[randomMoveIndex]);
                }
                LevelResult lr = new LevelResult(depth, 0, maxDepth);
                if (!Board.CheckDraw())
                {
                    lr = ComputeBestMove(nextLevelType, depth + 1, maxDepth);
                }
                if (levelType == MinMax.Min && (result.Result > lr.Result || (result.Result == lr.Result && result.Level > lr.Level))) result = lr;
                if (levelType == MinMax.Max && (result.Result < lr.Result || (result.Result == lr.Result && result.Level > lr.Level))) result = lr;
                lr.Move = move;
                Board.BackTrackMove();
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Entities
{
    public class LevelResult
    {
        public LevelResult(int level, int result, int move)
        {
            Level = level;
            Result = result;
            Move = move;
        }
        public int Result { get; set; }
        public int Level { get; set; }
        public int Move { get; set; }
    }
}

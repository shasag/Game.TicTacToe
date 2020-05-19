﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.TicTacToe.Tests.Helpers
{
    public class StubTextReader : TextReader
    {
        private string buffer = "";
        private int position = 0;

        public void WriteLine(string text)
        {
            buffer += text + Environment.NewLine;
        }

        public override string ReadLine()
        {
            var length = buffer.IndexOf(Environment.NewLine, position) - position;
            if (length < 0)
                return "";

            var result = buffer.Substring(position, length);
            position += length + Environment.NewLine.Length;
            return result;
        }
    }
}

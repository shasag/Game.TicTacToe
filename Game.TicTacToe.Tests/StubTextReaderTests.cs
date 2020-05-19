using Game.TicTacToe.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class StubTextReaderTests
    {
        [TestMethod]
        public void ReadLineReturnsEmptyStringIfNothingWritten()
        {
            var reader = new StubTextReader();
            Assert.AreEqual(reader.ReadLine(), "");
        }

        [TestMethod]
        public void ReadLineReturnsFirstLineOfWhatWasWritten()
        {
            var reader = new StubTextReader();
            reader.WriteLine("First Line");
            reader.WriteLine("Second Line");

            Assert.AreEqual(reader.ReadLine(), "First Line");
        }

        [TestMethod]
        public void ReadLineRemembersWhatWasAlreadyRead()
        {
            var reader = new StubTextReader();
            reader.WriteLine("First Line");
            reader.WriteLine("Second Line");
            reader.ReadLine();

            Assert.AreEqual(reader.ReadLine(), "Second Line");
        }
    }
}

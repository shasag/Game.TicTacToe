using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ticTacToeInstance = new Program();
            Assert.AreEqual(4, 4);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ticTacToeInstance = new TicTacToe();
            var sum = ticTacToeInstance.GetSum(2, 3);
            Assert.AreEqual(sum, 4);
        }
    }
}

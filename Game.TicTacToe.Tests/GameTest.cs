using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.TicTacToe.Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void MarkTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 1);

            var cellStatus = gameBoardInstance.Board[0, 1].GetCellState();

            Assert.AreEqual(cellStatus == Enums.CellOption.EmptyCell, true);
        }

        [TestMethod]
        public void ChangePlayerTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerO = new HumanPlayer("TestPlayerO", 'O');
            gameBoardInstance.CurrentPlayer = testPlayerX;
            gameBoardInstance.ChangePlayer(testPlayerX, testPlayerO);

            Assert.AreEqual(gameBoardInstance.CurrentPlayer, testPlayerO);
        }

        [TestMethod]
        public void CheckWinTest()
        {
            //First row same
            var gameBoardInstance = new GameBoard();
            var testPlayer = new HumanPlayer("TestPlayer", 'X');
            gameBoardInstance.MarkCell(testPlayer, 1);
            gameBoardInstance.MarkCell(testPlayer, 2);
            gameBoardInstance.MarkCell(testPlayer, 3);
            Assert.AreEqual(gameBoardInstance.CheckWin(), true);
        }

        [TestMethod]
        public void ChekDrawTest()
        {
            var gameBoardInstance = new GameBoard();

            var moveCounter = GameBoard.BOARD_SIZE * GameBoard.BOARD_SIZE;

            Assert.AreEqual(gameBoardInstance.CheckDraw(moveCounter), true);
        }

        [TestMethod]
        public void GetOpponentSymbolTest()
        {
            var gameBoardInstance = new GameBoard();
            var testPlayerX = new HumanPlayer("TestPlayerX", 'X');
            var testPlayerY = new HumanPlayer("TestPlayerY", 'O');
            gameBoardInstance.CurrentPlayer = testPlayerX;

            Assert.AreEqual(gameBoardInstance.GetOpponentSymbol(testPlayerX.PreferredSymbol), testPlayerY.PreferredSymbol);
        }
    }
}

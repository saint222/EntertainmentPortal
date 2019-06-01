using EP.DotsBoxes.Data;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GameBoardDataTests
    {
        [Test]
        public  void Test_Create_And_Get_GameBoard()
        {
            var gameBoardData = new GameBoardData();
            var gameBoard = new GameBoardDb() { Row = 5, Column = 6};

            var array = gameBoardData.Create(gameBoard);
            
            Assert.AreEqual(array.GetLength(0),gameBoard.Row);
            Assert.AreEqual(array.GetLength(1),gameBoard.Column);
        }

        [Test]
        public void Test_Get_GameBoard_Is_Not_Null_Or_Empty()
        {
            var gameBoardData = new GameBoardData();
            var gameBoard = new GameBoardDb() { Row = 3, Column = 3 };

            var array = gameBoardData.Create(gameBoard);

            Assert.IsNotNull(array);
            Assert.IsNotEmpty(array);
        }

        [Test]
        public void Test_Add_Line_To_Array()
        {
            var gameBoardData = new GameBoardData();
            var gameBoard = new GameBoardDb() { Row = 3, Column = 3 };

            var array = gameBoardData.Create(gameBoard);
            var line = (int)Box.Left;
            var cell = new GameBoardDb() { Row = 2, Column = 1 };
            var addLine = gameBoardData.Update(cell, line);

            Assert.AreEqual(addLine[cell.Row, cell.Column],line);
        }
    }
}
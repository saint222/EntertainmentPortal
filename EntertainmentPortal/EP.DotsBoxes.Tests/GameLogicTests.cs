using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Logic.Models;
using NUnit.Framework;

namespace EP.DotsBoxes.Tests
{
    [TestFixture]
    public class GameLogicTests
    {
        [Test]
        public void Test_Create_And_Get_Game_Board()
        {
            var game = new GameLogic();
            var rows = 5;
            var columns = 5;

            var gameBoard = game.CreateGameBoard(rows, columns);

            Assert.IsNotNull(gameBoard);
            Assert.IsNotEmpty(gameBoard);
            Assert.AreEqual(gameBoard.Count, rows * columns);
        }

        [Test]
        public void Test_Add_Current_Side_If_Top_True()
        {
            var game = new GameLogic();
            var side = new Cell() { Row = 2, Column = 2, Top = true };          
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var currentSide = game.AddCurrentSide(side);             

            Assert.IsTrue(currentSide.Top);
            Assert.IsFalse(currentSide.Bottom);
            Assert.IsFalse(currentSide.Right);
            Assert.IsFalse(currentSide.Left);
        }

        [Test]
        public void Test_Add_Current_Side_If_Bottom_True()
        {
            var game = new GameLogic();           
            var side = new Cell() { Row = 2, Column = 2, Bottom = true };          
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);           
            var currentSide = game.AddCurrentSide(side);

            Assert.IsFalse(currentSide.Top);
            Assert.IsTrue(currentSide.Bottom);
            Assert.IsFalse(currentSide.Right);
            Assert.IsFalse(currentSide.Left);
        }

        [Test]
        public void Test_Add_Current_Side_If_Left_True()
        {
            var game = new GameLogic();
            var side = new Cell() { Row = 2, Column = 2, Left = true };
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var currentSide = game.AddCurrentSide(side);

            Assert.IsFalse(currentSide.Top);
            Assert.IsFalse(currentSide.Bottom);
            Assert.IsFalse(currentSide.Right);
            Assert.IsTrue(currentSide.Left);
        }

        [Test]
        public void Test_Add_Current_Side_If_Right_True()
        {
            var game = new GameLogic();
            var side = new Cell() { Row = 2, Column = 2, Right = true };
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var currentSide = game.AddCurrentSide(side);

            Assert.IsFalse(currentSide.Top);
            Assert.IsFalse(currentSide.Bottom);
            Assert.IsTrue(currentSide.Right);
            Assert.IsFalse(currentSide.Left);
        }

        [Test]
        public void Test_Add_Common_Side_If_Top_True()
        {
            var game = new GameLogic();
            var cell = new Cell() { Row = 2, Column = 2, Top = true };
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var commonSide = game.AddCommonSide(cell, rows, columns);

            Assert.IsTrue(commonSide.Row == cell.Row - 1);
            Assert.IsTrue(commonSide.Column == cell.Column);
            Assert.IsTrue(commonSide.Bottom);
        }

        [Test]
        public void Test_Add_Common_Side_If_Bottom_True()
        {
            var game = new GameLogic();
            var cell = new Cell() { Row = 2, Column = 2, Bottom = true };
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var commonSide = game.AddCommonSide(cell, rows, columns);

            Assert.IsTrue(commonSide.Row == cell.Row + 1);
            Assert.IsTrue(commonSide.Column == cell.Column);
            Assert.IsTrue(commonSide.Top);
        }

        [Test]
        public void Test_Add_Common_Side_If_Left_True()
        {
            var game = new GameLogic();
            var cell = new Cell() { Row = 2, Column = 2, Left = true };
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var commonSide = game.AddCommonSide(cell, rows, columns);

            Assert.IsTrue(commonSide.Row == cell.Row);
            Assert.IsTrue(commonSide.Column == cell.Column - 1);
            Assert.IsTrue(commonSide.Right);
        }

        [Test]
        public void Test_Add_Common_Side_If_Right_True()
        {
            var game = new GameLogic();
            var cell = new Cell() { Row = 2, Column = 2, Right = true };
            var rows = 5;
            var columns = 5;

            game.CreateGameBoard(rows, columns);
            var commonSide = game.AddCommonSide(cell, rows, columns);

            Assert.IsTrue(commonSide.Row == cell.Row);
            Assert.IsTrue(commonSide.Column == cell.Column + 1);
            Assert.IsTrue(commonSide.Left);
        }

        [Test]
        public void Test_Add_Score_If_All_Sides_True()
        {           
            var game = new GameLogic(new List<Player>()
            {
                new Player() { Name = "Vasya" },
                new Player() { Name = "Petya" }
            });            
           
            Cell[] cells = {
                new Cell()
                {
                    Row = 2,
                    Column = 2,
                    Top = true,
                    Bottom = true,
                    Left = true,
                    Right = true,
                    Name = "Vasya"
                },
                new Cell()
                {
                    Row = 2,
                    Column = 2,
                    Top = true,
                    Bottom = false,
                    Left = true,
                    Right = true,
                    Name = "Petya"
                }
            };

            var player = game.CheckSquare(cells);

            Assert.AreEqual(player.Name, "Vasya");
            Assert.IsTrue(player.Score == 1);
        }
    }
}

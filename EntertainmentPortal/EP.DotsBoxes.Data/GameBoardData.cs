using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EP.DotsBoxes.Data.Models;


namespace EP.DotsBoxes.Data
{
    public class GameBoardData
    {
        private int[,] _gameBoardArray = null;

        public GameBoardData()
        {
        }

        public int[,] Get => _gameBoardArray;

        public int[,] Create(GameBoardDb gameBoard)
        {
            var row = gameBoard.Row;
            var column = gameBoard.Column;
            return _gameBoardArray = new int[row, column];
        }

        public int[,] Update(GameBoardDb gameBoard, int value)
        {
            int row = gameBoard.Row;
            int column = gameBoard.Column;
            _gameBoardArray[row, column] = value;
            return _gameBoardArray;
        }
    }
}

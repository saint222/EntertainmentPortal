using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Data.Models;


namespace EP.DotsBoxes.Data
{
    public class GameBoardData
    {
        private readonly GameBoardDb _gameBoard;
        private int[,] _gameBoardArray;


        public GameBoardData(GameBoardDb gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public int[,] GetGameBoard => _gameBoardArray;

        public int[,] Save(int[,] gameBoard)
        {
           return _gameBoardArray = gameBoard;
        }

    }
}

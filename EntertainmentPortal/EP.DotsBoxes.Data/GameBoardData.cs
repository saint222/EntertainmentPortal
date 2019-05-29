using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Data.Models;


namespace EP.DotsBoxes.Data
{
    public class GameBoardData
    {
        private readonly GameBoardDb _gameBoard;
        private int[,] _gameBoardArray = null;


        public GameBoardData(GameBoardDb gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public int[,] GetGameBoard => _gameBoardArray;

        public void Save(int[,] gameBoard)
        {
           _gameBoardArray = gameBoard;
        }

    }
}

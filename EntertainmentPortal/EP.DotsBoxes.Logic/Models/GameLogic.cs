using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus.Extensions;
using EP.DotsBoxes.Data.Context;
using JetBrains.Annotations;

namespace EP.DotsBoxes.Logic.Models
{
    public class GameLogic
    {
        private List<Cell> _cells = new List<Cell>();

        private  List<Player> _players = new List<Player>();

        public GameLogic()
        {
        }

        public GameLogic(List<Cell> cells)
        {
            _cells = cells;
        }

        public List<Cell> CreateGameBoard(int row, int column)
        {
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= column; j++)
                {
                    _cells.Add(new Cell() {Row = i, Column = j});
                }
            }

            return _cells;
        }

        public List<Cell> AddCommonSide(Cell cell, int rows, int columns)
        {
            var row = cell.Row;
            var column = cell.Column;
            AddCurrentSide(cell);

            if ((row != 1) & cell.Top)
            {
                _cells.Where(p => p.Row == row - 1 & p.Column == column).ToList().ForEach(p => p.Bottom = true);
            }
            if ((row != rows) & cell.Bottom)
            {
                _cells.Where(p => p.Row == row + 1 & p.Column == column).ToList().ForEach(p => p.Top = true);
            }
            if ((column != 1) & cell.Left)
            {
                _cells.Where(p => p.Row == row & p.Column == column - 1).ToList().ForEach(p => p.Right = true);
            }
            if ((column != columns) & cell.Right)
            {
                _cells.Where(p => p.Row == row & p.Column == column + 1).ToList().ForEach(p => p.Left = true);
            }

            return _cells;
        }

        public List<Cell> AddCurrentSide(Cell cell)
        {
            var row = cell.Row;
            var column = cell.Column;

            if (cell.Top)
            {
                _cells.Where(p => p.Row == row & p.Column == column).ToList().ForEach(p => p.Top = true);
            }

            if (cell.Bottom)
            {
                _cells.Where(p => p.Row == row & p.Column == column).ToList().ForEach(p => p.Bottom = true);
            }
            if (cell.Left)
            {
                _cells.Where(p => p.Row == row & p.Column == column).ToList().ForEach(p => p.Left = true);
            }
            if (cell.Right)
            {
                _cells.Where(p => p.Row == row & p.Column == column).ToList().ForEach(p => p.Right = true);
            }

            return _cells;
        }

        public List<Player> CheckSquare()
        {
            foreach (var cell in _cells)
            {
                if (cell.Top & cell.Bottom & cell.Left & cell.Right)
                {
                    _players.Where(p => p.Name == cell.Name).ToList().ForEach(p => p.Score += 1);
                }
            }

            return _players;
        }
    }
}

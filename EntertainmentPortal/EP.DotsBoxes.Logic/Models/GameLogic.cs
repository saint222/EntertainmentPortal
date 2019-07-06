using System;
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

        private List<Player> _players = new List<Player>();

        public GameLogic()
        {
        }

        public GameLogic(List<Cell> cells, List<Player> players)
        {
            _cells = cells;
            _players = players;
        }

        public List<Cell> CreateGameBoard(int row, int column)
        {
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= column; j++)
                {
                    _cells.Add(new Cell() { Row = i, Column = j });
                }
            }

            return _cells;
        }

        public Cell[] AddSides(Cell cell, int rows, int columns)
        {
           return new Cell[] { AddCurrentSide(cell), AddCommonSide(cell, rows, columns) };                    
        }

        public Cell AddCommonSide(Cell cell, int rows, int columns)
        {
            var row = cell.Row;
            var column = cell.Column;
            Cell result = null;

            if ((row != 1) & cell.Top)
            {
                result = _cells.Where(p => p.Row == row - 1 & p.Column == column).First();
                result.Bottom = true;
            }
            if ((row != rows) & cell.Bottom)
            {
                result = _cells.Where(p => p.Row == row + 1 & p.Column == column).First();
                result.Top = true;
            }
            if ((column != 1) & cell.Left)
            {
                result = _cells.Where(p => p.Row == row & p.Column == column - 1).First();
                result.Right = true;
            }
            if ((column != columns) & cell.Right)
            {
                result = _cells.Where(p => p.Row == row & p.Column == column + 1).First();
                result.Left = true;
            }
            result.Name = cell.Name;
            return result;
        }

        public Cell AddCurrentSide(Cell cell)
        {
            var row = cell.Row;
            var column = cell.Column;
            Cell result = null;

            if (cell.Top)
            {
                result = _cells.Where(p => p.Row == row & p.Column == column).First();
                result.Top = true;
            }
            if (cell.Bottom)
            {
                result = _cells.Where(p => p.Row == row & p.Column == column).First();
                result.Bottom = true;
            }
            if (cell.Left)
            {
                result = _cells.Where(p => p.Row == row & p.Column == column).First();
                result.Left = true;
            }
            if (cell.Right)
            {
                result = _cells.Where(p => p.Row == row & p.Column == column).First();
                result.Right = true;
            }
            result.Name = cell.Name;
            return result;
        }

        public Player CheckSquare(Cell[] cells)
        {           
            Player result = null;

            foreach (var cell in cells)
            {              
                if (cell != null && cell.Top & cell.Bottom & cell.Left & cell.Right)
                {
                    result = _players.Where(p => p.Name.Equals(cell.Name)).First();
                    result.Score += 1;                 
                }
            }

            return result;
        }
    }
}

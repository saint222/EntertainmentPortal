using System;
using System.Collections.Generic;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// Game map with the value (size) of the measurement of the playing
    /// space transferred to the constructor
    /// </summary>
    public class Map : IMap, IGame
    {
        /// <summary>
        /// Size property. Stores the size of playing field.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Fields property. Represents playing field as array of cells.
        /// </summary>
        public Cell[,] Fields { get; }

        /// <summary>
        /// Map constructor.
        /// </summary>
        /// <param name="size">
        /// Parameter size requires an integer argument. 
        /// </param>
        public Map(int size)
        {
            Size = size;
            Fields = InitMap(size);
        }

        public Cell[,] InitMap(int size)
        {
            Cell[,] fields = new Cell[size, size];
            for (var i = 0; i < size; i++)     // lines
                for (var j = 0; j < size; j++) // column letters
                {
                    var cell = new Cell(i, j) { Letter = null };
                    fields[i, j] = cell;
                }

            return fields;
        }

        /// <summary>
        /// GetIndexCell method.
        /// </summary>
        /// <param name="x">Matrix element X</param>
        /// <param name="y">Matrix element Y</param>
        /// <returns>The method returns cell</returns>
        private int GetIndexCell(int x, int y)
        {
            return y + Size * x;
        }
        
        /// <summary>
        /// Returns the cell by the given coordinates X and Y
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <returns>returns cell</returns>
        public Cell GetCell(int x, int y)
        {
            return Fields[x, y];
        }

        /// <summary>
        /// The empty cell value is checked.
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <returns>returns true if empty</returns>
        public bool IsEmptyCell(int x, int y)
        {
            return GetCell(x, y).IsEmpty();
        }

        /// <summary>
        /// Check if the cell is allowed to insert a new letter.
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <returns>returns true if allowed</returns>
        public bool IsAllowedCell(int x, int y)
        {
            if (!IsEmptyCell(x, y)) return false;

            // variables for busy cell checks
            var checkUp = y + 1;   // cell on top
            var checkDown = y - 1; // bottom cell
            var checkRight = x + 1;   // right cell
            var checkLeft = x - 1;    // left cell

            if (checkRight >= Size && checkLeft < 0) return false;

            if (checkUp <= Size)
                if (Fields[x, checkUp].Letter != null)
                    return true;

            if (checkDown >= 0)
                if (Fields[x, checkDown].Letter != null)
                    return true;

            if(checkLeft >= 0)
                if (Fields[checkLeft, y].Letter != null)
                    return true;

            if (checkRight <= Size)
                if (Fields[checkLeft, y].Letter != null)
                    return true;

            return false;
        }

        /// <summary>
        /// Check that all received letters in the form of a tuple
        /// list of coordinates of their location on the map, comply
        /// with the rules of the game on making words
        /// </summary>
        /// <param name="word">Tuple list of coordinates</param>
        /// <returns>returns true if this is the correct word</returns>
        public bool IsItCorrectWord(List<Cell> word)
        {
            var isAllLetterTrue = false;
            for (var let = 0; let < word.Count; let++)
            {
                //current cell in the word
                Cell currentCell = word[let];

                //check that a non-empty cell is selected in the word
                if (currentCell.IsEmpty()) return false;

                //next cell in the word
                Cell nextCell;
                if (let < word.Count - 1) nextCell = word[let + 1];
                else break;

                //checking that the cell is not pointing to itself
                if ((currentCell.X - nextCell.X == 0) & (currentCell.Y - nextCell.Y == 0)) return false;

                //check that the next cell is not empty
                if (IsEmptyCell(nextCell.X, nextCell.Y)) return false;

                //check that the next cell is located with an offset of
                //not more than 1 and strictly horizontally from the current
                if ((Math.Abs(currentCell.X - nextCell.X) == 0) & (Math.Abs(currentCell.Y - nextCell.Y) == 1))
                {
                    isAllLetterTrue = true;
                    continue;
                }

                // check that the next cell is located with an offset of
                // not more than 1 and strictly vertically from the current
                if ((Math.Abs(currentCell.Y - nextCell.Y) == 0) & (Math.Abs(currentCell.X - nextCell.Y) == 1))
                {
                    isAllLetterTrue = true;
                    continue;
                }

                isAllLetterTrue = false;
            }

            return isAllLetterTrue;
        }

        /// <summary>
        /// Returns the word from the game map according to the transmitted list of coordinates
        /// </summary>
        /// <param name="words"></param>
        /// <param name="map"></param>
        /// <returns>The word from the game map</returns>
        public string GetSelectedWord(IEnumerable<Cell> words)
        {
            var word = "";
            foreach (var cell in words) word += cell.Letter;
            return word;
        }
    }
}
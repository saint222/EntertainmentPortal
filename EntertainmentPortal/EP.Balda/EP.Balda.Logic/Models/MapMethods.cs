using System;
using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{

    public class MapMethods
        {
            /// <summary>
            ///     Id property. Represents playing field Id.
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            ///     Fields property. Represents playing field as array of cells.
            /// </summary>
            public Cell[,] CellsArr { get; set; }

            /// <summary>
            ///     Fields property. Represents playing field as array of cells.
            /// </summary>
            public List<Cell> Cells { get; set; }

            public MapMethods()
            {
            }

            /// <summary>
            ///     Map constructor.
            /// </summary>
            /// <param name="size">
            ///     Parameter size requires an integer argument.
            /// </param>
            public MapMethods(int size)
            {
                Size = size;
                CellsArr = InitMap(size);
            }

            /// <summary>
            ///     The method checks that all letters
            ///     comply with the rules of the game on making words.
            /// </summary>
            /// <param name="word">Parameter requires List of Cell argument.</param>
            /// <returns>returns true if this is the correct word</returns>
            public bool IsItCorrectWord(List<Cell> word)
            {
                var isAllLetterTrue = false;
                for (var let = 0; let < word.Count; let++)
                {
                    //current cell in the word
                    var currentCell = word[let];

                    //check that a non-empty cell is selected in the word
                    if (currentCell.IsEmpty()) return false;

                    //next cell in the word
                    Cell nextCell;
                    if (let < word.Count - 1) nextCell = word[let + 1];
                    else break;

                    //checking that the cell is not pointing to itself
                    if ((currentCell.X - nextCell.X == 0) & (currentCell.Y - nextCell.Y == 0))
                        return false;

                    //check that the next cell is not empty
                    if (IsEmptyCell(nextCell.X, nextCell.Y)) return false;

                    //check that the next cell is located with an offset of
                    //not more than 1 and strictly horizontally from the current
                    if ((Math.Abs(currentCell.X - nextCell.X) == 0) &
                        (Math.Abs(currentCell.Y - nextCell.Y) == 1))
                    {
                        isAllLetterTrue = true;
                        continue;
                    }

                    // check that the next cell is located with an offset of
                    // not more than 1 and strictly vertically from the current
                    if ((Math.Abs(currentCell.Y - nextCell.Y) == 0) &
                        (Math.Abs(currentCell.X - nextCell.Y) == 1))
                    {
                        isAllLetterTrue = true;
                        continue;
                    }

                    isAllLetterTrue = false;
                }

                return isAllLetterTrue;
            }

            /// <summary>
            ///     The method returns the word from the game map according to the entered cells.
            /// </summary>
            /// <param name="words">Parameter requires IEnumerable of Cell argument.</param>
            /// <returns>The method returns word from the game map.</returns>
            public string GetSelectedWord(List<Cell> words)
            {
                var word = "";
                foreach (var cell in words) word += cell.Letter;
                return word;
            }

            /// <summary>
            ///     Size property. Stores the size of playing field.
            /// </summary>
            public int Size { get; }

            /// <summary>
            ///     The method returns the cell from game map by the given coordinates.
            /// </summary>
            /// <param name="x">Parameter x requires an integer argument.</param>
            /// <param name="y">Parameter y requires an integer argument.</param>
            /// <returns>The method returns cell.</returns>
            public Cell GetCell(int x, int y)
            {
                return CellsArr[x, y];
            }

            /// <summary>
            ///     The method checks if the cell is empty.
            /// </summary>
            /// <param name="x">Parameter x requires an integer argument.</param>
            /// <param name="y">Parameter y requires an integer argument.</param>
            /// <returns>The method returns true if the cell is empty</returns>
            public bool IsEmptyCell(int x, int y)
            {
                return GetCell(x, y).IsEmpty();
            }

            /// <summary>
            ///     The method checks if the cell is allowed to insert a new letter.
            /// </summary>
            /// <param name="x">Parameter x requires an integer argument.</param>
            /// <param name="y">Parameter y requires an integer argument.</param>
            /// <returns>returns true if allowed</returns>
            public bool IsAllowedCell(int x, int y)
            {
                if (!IsEmptyCell(x, y))
                    return false;

                // variables for busy cell checks
                var checkUp = y + 1;    // cell on top
                var checkDown = y - 1;  // bottom cell
                var checkRight = x + 1; // right cell
                var checkLeft = x - 1;  // left cell

                if (checkUp < Size)
                    if (CellsArr[x, checkUp].Letter != null)
                        return true;

                if (checkDown >= 0)
                    if (CellsArr[x, checkDown].Letter != null)
                        return true;

                if (checkLeft >= 0)
                    if (CellsArr[checkLeft, y].Letter != null)
                        return true;

                if (checkRight < Size)
                    if (CellsArr[checkRight, y].Letter != null)
                        return true;

                return false;
            }

            /// <summary>
            ///     The method initializes array of cells that represents the game map.
            /// </summary>
            /// <param name="size">
            ///     Parameter size requires an integer argument.
            /// </param>
            private Cell[,] InitMap(int size)
            {
                var fields = new Cell[size, size];
                for (var i = 0; i < size; i++)     // lines
                    for (var j = 0; j < size; j++) // column letters
                    {
                        var cell = new Cell(i, j) { Letter = null };
                        fields[i, j] = cell;
                    }

                return fields;
            }
        }
    }
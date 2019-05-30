using System;
using System.Collections.Generic;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    public class Step : IStep
    {
        private static int GetIndexCell(int x, int y, Map map)
        {
            return y + map.Size * x;
        }

        #region Implementation of IStep

        /// <summary>
        ///     Returns the cell by the given coordinates X and Y
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns cell</returns>
        public Cell GetCell(int x, int y, Map map)
        {
            return map.Fields[GetIndexCell(x, y, map)];
        }

        /// <summary>
        ///     The empty cell value is checked
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns true if empty</returns>
        public bool IsEmptyCell(int x, int y, Map map)
        {
            return (this as IStep).GetCell(x, y, map).Letter == null;
        }

        /// <summary>
        ///     Check if the cell is allowed to insert a new letter.
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns true if allowed</returns>
        public bool IsAllowedCell(int x, int y, Map map)
        {
            if (map != null && !(this as IStep).IsEmptyCell(x, y, map)) return false;
            var idx = GetIndexCell(x, y, map);
            var mapCapacity = map.Size * map.Size - 1;

            // variables for busy cell checks
            var chkUp = idx - map.Size;   // cell on top
            var chkDown = idx + map.Size; // bottom cell
            var chkRight = idx + 1;       // right cell
            var chkLeft = idx - 1;        // left cell

            if (chkRight >= mapCapacity && chkLeft < 0) return false;

            if (chkUp >= 0)
                if (map.Fields[chkUp].Letter != null)
                    return true;

            if (chkDown < mapCapacity)
                if (map.Fields[chkDown].Letter != null)
                    return true;

            if (idx != mapCapacity)
                if (map.Fields[chkRight].Letter != null)
                    return true;

            if (map.Fields[chkLeft].Letter != null)
                return true;

            return false;
        }

        /// <summary>
        ///     Check that all received letters in the form of a tuple
        ///     list of coordinates of their location on the map, comply
        ///     with the rules of the game on making words
        /// </summary>
        /// <param name="wordTuples">Tuple list of coordinates</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns true if this is the correct word</returns>
        public bool IsItCorrectWord(List<(int x, int y)> wordTuples, Map map)
        {
            var isAllLetterTrue = false;
            for (var let = 0; let < wordTuples.Count; let++)
            {
                //current cell in the word
                var (x, y) = wordTuples[let];

                //check that a non-empty cell is selected in the word
                if (IsEmptyCell(x, y, map)) return false;

                //next cell in the word
                int dx, dy;
                if (let < wordTuples.Count - 1) (dx, dy) = wordTuples[let + 1];
                else break;

                //checking that the cell is not pointing to itself
                if ((x - dx == 0) & (y - dy == 0)) return false;

                //check that the next cell is not empty
                if (IsEmptyCell(dx, dy, map)) return false;

                //check that the next cell is located with an offset of
                //not more than 1 and strictly horizontally from the current
                if ((Math.Abs(x - dx) == 0) & (Math.Abs(y - dy) == 1))
                {
                    isAllLetterTrue = true;
                    continue;
                }

                // check that the next cell is located with an offset of
                // not more than 1 and strictly vertically from the current
                if ((Math.Abs(y - dy) == 0) & (Math.Abs(x - dx) == 1))
                {
                    isAllLetterTrue = true;
                    continue;
                }

                isAllLetterTrue = false;
            }

            return isAllLetterTrue;
        }

        /// <summary>
        ///     Returns the word from the game map according to the transmitted list of coordinates
        /// </summary>
        /// <param name="wordTuples"></param>
        /// <param name="map"></param>
        /// <returns>The word from the game map</returns>
        public string GetSelectedWord(IEnumerable<(int x, int y)> wordTuples, Map map)
        {
            var word = "";
            foreach (var (x, y) in wordTuples) word += GetCell(x, y, map).Letter;
            return word;
        }

        #endregion
    }
}
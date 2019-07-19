using EP.Balda.Logic.Models;
using System.Collections.Generic;

namespace EP.Balda.Web.Services
{
    public static class Helpers
    {
        public static Cell[,] Do2DimArray(Map map)
        {
            var cells = new Cell[map.Size, map.Size];

            for (int i = 0; i < map.Size; i++)     // lines
            {
                for (int j = map.Size - 1; j >= 0; j--) // columns
                {
                    foreach (var c in map.Cells)
                    {
                        if (j == c.X && i == c.Y)
                        {
                            cells[i, j] = c;
                        }
                    }
                }
            }

            return cells;
        }

        public static char[] GetEnglishAlphabet()
        {
            char[] alphabet = new char[26];
            char letter = 'A';

            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet[i] = letter++;
            }

            return alphabet;
        }

        public static string FormWordFromCells(List<Cell> cells)
        {
            string word = "";

            foreach (var cell in cells)
            {
                word += cell.Letter;
            }

            return word;
        }
    }
}

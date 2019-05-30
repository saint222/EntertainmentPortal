using System;


namespace EP.Sudoku.Logic.Services
{
    public class SolverManual
    {
        private readonly int[] _grid;

        public SolverManual(int[,] matrix)
        {
            _grid = new int[81];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _grid[i] = matrix[i, j];
                }
            }
        }

        public void Solve()
        {
            PlaceNumber(0);
        }

        private void PlaceNumber(int pos)
        {
            if (pos == 81)
            {
                throw new Exception("Finished!");
            }
            if (_grid[pos] > 0)
            {
                PlaceNumber(pos + 1);
                return;
            }
            for (int n = 1; n <= 9; n++)
            {
                if (CheckValidity(n, pos % 9, pos / 9))
                {
                    _grid[pos] = n;
                    PlaceNumber(pos + 1);
                    _grid[pos] = 0;
                }
            }
        }

        private bool CheckValidity(int val, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (_grid[y * 9 + i] == val || _grid[i * 9 + x] == val)
                    return false;
            }
            int startX = (x / 3) * 3;
            int startY = (y / 3) * 3;
            for (int i = startY; i < startY + 3; i++)
            {
                for (int j = startX; j < startX + 3; j++)
                {
                    if (_grid[i * 9 + j] == val)
                        return false;
                }
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Models;

namespace EP.Sudoku.Logic.Services
{
    public class GenerationSudokuService
    {
        private const int GRID_DIMENSION = 9;
        private readonly Random _random = new Random();

        public int[,] GetBaseGrid()
        {
            int[,] baseGrid = new int[GRID_DIMENSION, GRID_DIMENSION]{
                {1, 2, 3, 4, 5, 6, 7, 8, 9},
                {4, 5, 6, 7, 8, 9, 1, 2, 3},
                {7, 8, 9, 1, 2, 3, 4, 5, 6},
                {2, 3, 4, 5, 6, 7, 8, 9, 1},
                {5, 6, 7, 8, 9, 1, 2, 3, 4},
                {8, 9, 1, 2, 3, 4, 5, 6, 7},
                {3, 4, 5, 6, 7, 8, 9, 1, 2},
                {6, 7, 8, 9, 1, 2, 3, 4, 5},
                {9, 1, 2, 3, 4, 5, 6, 7, 8}
            };

            return baseGrid;
        }

        public int[,] Transposition(int[,] sGrid)
        {
            int[,] trans = new int[GRID_DIMENSION, GRID_DIMENSION];

            for (int i = 0; i < GRID_DIMENSION; i++)
            {
                for (int j = 0; j < GRID_DIMENSION; j++)
                {
                    trans[i, j] = sGrid[j, i];
                }
            }

            return trans;
        }

        public int[,] SwapRowsSmall(int[,] grid)
        {
            int swap;
            int area = _random.Next(0, 3);
            int row1 = _random.Next(0, 3);
            int row2 = _random.Next(0, 3);

            while (row1 == row2)
            { 
                row2 = _random.Next(0, 3);
            }

            row1 = area * 3 + row1;
            row2 = area * 3 + row2;

            for (int i = 0; i < GRID_DIMENSION; i++)
            {
                swap= grid[row1, i];
                grid[row1, i] = grid[row2, i];
                grid[row2, i] = swap;
            }

            return grid;
        }

        public int[,] SwapColumnsSmall(int[,] grid)
        {
            int swap;
            int area = _random.Next(0, 3);
            int column1 = _random.Next(0, 3);
            int column2 = _random.Next(0, 3);

            while (column1 == column2)
            {
                column2 = _random.Next(0, 3);
            }

            column1 = area * 3 + column1;
            column2 = area * 3 + column2;

            for (int i = 0; i < GRID_DIMENSION; i++)
            {
                swap = grid[i, column1];
                grid[i, column1] = grid[i, column2];
                grid[i, column2] = swap;
            }

            return grid;
        }

        public int[,] SwapRowsArea(int[,] grid)
        {
            int swap;
            int area1 = _random.Next(0, 3);
            int area2 = _random.Next(0, 3);

            while (area1 == area2)
            {
                area2 = _random.Next(0, 3);
            }

            int row1 = area1 * 3;
            int row2 = area2 * 3;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < GRID_DIMENSION; j++)
                {
                    swap = grid[row1, j];
                    grid[row1, j] = grid[row2, j];
                    grid[row2, j] = swap;
                }

                row1++;
                row2++;
            }

            return grid;
        }

        public int[,] SwapColumnsArea(int[,] grid)
        {
            int swap;
            int area1 = _random.Next(0, 3);
            int area2 = _random.Next(0, 3);

            while (area1 == area2)
            {
                area2 = _random.Next(0, 3);
            }

            int column1 = area1 * 3;
            int column2 = area2 * 3;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < GRID_DIMENSION; j++)
                {
                    swap = grid[j, column1];
                    grid[j, column1] = grid[j, column2];
                    grid[j, column2] = swap;
                }

                column1++;
                column2++;
            }

            return grid;
        }

        public List<Cell> GridToCells(int[,] grid)
        {
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < GRID_DIMENSION; i++)
            {
                for (int j = 0; j < GRID_DIMENSION; j++)
                {
                    Cell cell = new Cell
                    {
                        X = i + 1,
                        Y = j + 1,
                        Value = grid[i, j]
                    };
                    cells.Add(cell);
                }
            }

            return cells;
        }

        /// <summary>    
        /// Shuffles cells and rows randomly to generate new grid
        /// </summary>
        public List<Cell> GetRandomCells()
        {
            int[,] grid = GetBaseGrid();
            List<Func<int[,], int[,]>> methods = new List<Func<int[,], int[,]>>();
            Random rand = new Random();
            int iterations = rand.Next(50, 100);

            methods.Add(Transposition);
            methods.Add(SwapRowsSmall);
            methods.Add(SwapColumnsSmall);
            methods.Add(SwapRowsArea);
            methods.Add(SwapColumnsArea);

            for (int i = 0; i < iterations; i++)
            {
                grid = methods[rand.Next(0, 5)].Invoke(grid);
            }

            return GridToCells(grid);
        }

        /// <summary>    
        /// Shuffles cells and rows randomly to generate new grid
        /// </summary>
        public int[,] GetRandomGrid()
        {
            int[,] grid = GetBaseGrid();
            List<Func<int[,], int[,]>> methods = new List<Func<int[,], int[,]>>();
            Random rand = new Random();
            int iterations = rand.Next(50, 100);

            methods.Add(Transposition);
            methods.Add(SwapRowsSmall);
            methods.Add(SwapColumnsSmall);
            methods.Add(SwapRowsArea);
            methods.Add(SwapColumnsArea);

            for (int i = 0; i < iterations; i++)
            {
                grid = methods[rand.Next(0, 5)].Invoke(grid);
            }

            return grid;
        }

        /// <summary>    
        /// Removes count of cells from the filled initGrid
        /// </summary>
        public int[,] RemoveCells(int[,] initGrid, int count)
        {
            Random rand = new Random();
            int[,] grid = GetRandomGrid();
            Array.Copy(initGrid, grid, GRID_DIMENSION * GRID_DIMENSION);
            int i;
            int j;

            for (int index = 0; index < count; index++)
            {
                do
                {
                    i = rand.Next(0, 9);
                    j = rand.Next(0, 9);
                } while (grid[i, j] == 0);

                grid[i, j] = 0;
            }

            return grid;
        }
       
        public List<Cell> GetSudoku(DifficultyLevel level)
        {
            Random rand = new Random();
            int[,] grid = GetRandomGrid();
            int i, j;
            int count = 0;

            switch (level)
            {
                case DifficultyLevel.Easy:
                    count = rand.Next(41, 44); //filled in cells (38-40)
                    break;

                case DifficultyLevel.Normal:
                    count = rand.Next(48, 51); //filled in cells (31-33)
                    break;

                case DifficultyLevel.Hard:
                    count = rand.Next(55, 58); //filled in cells (24-26)
                    break;
            }

            for (int index = 0; index < count; index++)
            {
                do
                {
                    i = rand.Next(0, 9);
                    j = rand.Next(0, 9);
                } while (!IsCanRemoveCell(grid, level, i, j));

                grid[i, j] = 0;
            }


            return GridToCells(grid);
        }

        private bool IsCanRemoveCell(int[,] grid, DifficultyLevel level, int x, int y)
        {
            if (grid[x, y] == 0)
            {
                return false;
            }

            int countInLine = 0;
            switch (level)
            {
                case DifficultyLevel.Easy:
                    countInLine = 5;
                    break;

                case DifficultyLevel.Normal:
                    countInLine = 4;
                    break;

                case DifficultyLevel.Hard:
                    countInLine = 3;
                    break;
            }

            if (CountEmptyInRow(grid, x) < countInLine)
            {
                return false;
            }

            if (CountEmptyInColumn(grid, y) < countInLine)
            {
                return false;
            }

            return true;
        }

        private int CountEmptyInRow(int[,] grid, int x)
        {
            int count = 0;
            for (int i = 0; i < GRID_DIMENSION; i++)
            {
                if (grid[x, i] != 0)
                {
                    count++;
                }
            }

            return count;
        }

        private int CountEmptyInColumn(int[,] grid, int y)
        {
            int count = 0;
            for (int i = 0; i < GRID_DIMENSION; i++)
            {
                if (grid[i, y] != 0)
                {
                    count++;
                }
            }

            return count;
        }
    }
}

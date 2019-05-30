using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Services
{
    public class GenerationMatrixService
    {
        private const int MATRIX_DIMENSION = 9;
        private readonly Random _random = new Random();

        public int[,] GetBaseMatrix()
        {
            int[,] baseMatrix = new int[MATRIX_DIMENSION, MATRIX_DIMENSION]{
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

            return baseMatrix;
        }

        public int[,] Transposition(int[,] sMatrix)
        {
            int[,] trans = new int[MATRIX_DIMENSION, MATRIX_DIMENSION];

            for (int i = 0; i < MATRIX_DIMENSION; i++)
            {
                for (int j = 0; j < MATRIX_DIMENSION; j++)
                {
                    trans[i, j] = sMatrix[j, i];
                }
            }

            return trans;
        }

        public int[,] SwapRowsSmall(int[,] matrix)
        {
            int swap;
            int area = _random.Next(0, 2);
            int row1 = _random.Next(0, 2);
            int row2 = _random.Next(0, 2);

            while (row1 == row2)
            { 
                row2 = _random.Next(0, 2);
            }

            row1 = area * 3 + row1;
            row2 = area * 3 + row2;

            for (int i = 0; i < MATRIX_DIMENSION; i++)
            {
                swap= matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = swap;
            }

            return matrix;
        }

        public int[,] SwapColumnsSmall(int[,] matrix)
        {
            int swap;
            int area = _random.Next(0, 2);
            int column1 = _random.Next(0, 2);
            int column2 = _random.Next(0, 2);

            while (column1 == column2)
            {
                column2 = _random.Next(0, 2);
            }

            column1 = area * 3 + column1;
            column2 = area * 3 + column2;

            for (int i = 0; i < MATRIX_DIMENSION; i++)
            {
                swap = matrix[i, column1];
                matrix[i, column1] = matrix[i, column2];
                matrix[i, column2] = swap;
            }

            return matrix;
        }

        public int[,] SwapRowsArea(int[,] matrix)
        {
            int swap;
            int area1 = _random.Next(0, 2);
            int area2 = _random.Next(0, 2);

            while (area1 == area2)
            {
                area2 = _random.Next(0, 2);
            }

            int row1 = area1 * 3;
            int row2 = area2 * 3;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < MATRIX_DIMENSION; j++)
                {
                    swap = matrix[row1, j];
                    matrix[row1, j] = matrix[row2, j];
                    matrix[row2, j] = swap;
                }

                row1++;
                row2++;
            }

            return matrix;
        }

        public int[,] SwapColumnsArea(int[,] matrix)
        {
            int swap;
            int area1 = _random.Next(0, 2);
            int area2 = _random.Next(0, 2);

            while (area1 == area2)
            {
                area2 = _random.Next(0, 2);
            }

            int column1 = area1 * 3;
            int column2 = area2 * 3;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < MATRIX_DIMENSION; j++)
                {
                    swap = matrix[j, column1];
                    matrix[j, column1] = matrix[j, column2];
                    matrix[j, column2] = swap;
                }

                column1++;
                column2++;
            }

            return matrix;
        }

        public int[,] RemoveCells(int[,] initMatrix, int count)
        {
            Random rand = new Random();
            int[,] matrix = new int[MATRIX_DIMENSION, MATRIX_DIMENSION];
            Array.Copy(initMatrix, matrix, MATRIX_DIMENSION * MATRIX_DIMENSION);
            int i;
            int j;
            int ind = 0;

            for (int index = 0; index < count; index++)
            {
                do
                {
                    ind++;
                    i = rand.Next(0, 9);
                    j = rand.Next(0, 9);
                } while (matrix[i, j] == 0);

                matrix[i, j] = 0;
            }

            return matrix;
        }
    }
}

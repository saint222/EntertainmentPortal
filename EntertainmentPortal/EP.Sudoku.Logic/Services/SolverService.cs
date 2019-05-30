using System;
using System.Diagnostics;
using Microsoft.SolverFoundation.Solvers;

namespace EP.Sudoku.Logic.Services
{
    public class SolverService
    {
        private CspTerm[] GetSlice(CspTerm[][] sudoku, int rowA, int rowB, int columnA, int columnB)
        {
            CspTerm[] slice = new CspTerm[9];
            int i = 0;
            for (int row = rowA; row < rowB + 1; row++)
            for (int col = columnA; col < columnB + 1; col++)
            {
                {
                    slice[i++] = sudoku[row][col];
                }
            }
            return slice;
        }

        public int[,] Solver(int[,] matrix)
        {
            ConstraintSystem constraintSystem = ConstraintSystem.CreateSolver();
            CspDomain cspDomain = constraintSystem.CreateIntegerInterval(1, 9);
            CspTerm[][] sudoku = constraintSystem.CreateVariableArray(cspDomain, "cell", 9, 9);
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (matrix[row, col] > 0)
                    {
                        constraintSystem.AddConstraints(constraintSystem.Equal(matrix[row, col], sudoku[row][col]));
                    }
                }
                constraintSystem.AddConstraints(constraintSystem.Unequal(GetSlice(sudoku, row, row, 0, 8)));
            }
            for (int col = 0; col < 9; col++)
            {
                constraintSystem.AddConstraints(constraintSystem.Unequal(GetSlice(sudoku, 0, 8, col, col)));
            }
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    constraintSystem.AddConstraints(constraintSystem.Unequal(GetSlice(sudoku, a * 3, a * 3 + 2, b * 3, b * 3 + 2)));
                }
            }
            ConstraintSolverSolution solution = constraintSystem.Solve();
            object[,] result = new object[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    solution.TryGetValue(sudoku[row][col], out result[row, col]);
                }
            }

            int[,] solvedMatrix = new int[9,9];
            Array.Copy(result, solvedMatrix, 81);

            return solvedMatrix;
        }
    }
}

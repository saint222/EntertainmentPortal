using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web
{
    /// <summary>
    /// Here are the values of TVP (Token Validation Parameters) constants for it's (JWT's) local creating.
    /// </summary>
    public static class SudokuConstants
    {
        /// <summary>
        /// Here is the values of ISSUER_NAME.
        /// </summary>
        public const string ISSUER_NAME = "Sudoku";

        /// <summary>
        /// Here is the values of AUDIENCE_NAME.
        /// </summary>
        public const string AUDIENCE_NAME = "SudokuApp";

        /// <summary>
        /// Here is the values of SECRET.
        /// </summary>
        public const string SECRET = "My-Super-Secret_Key";
    }
}

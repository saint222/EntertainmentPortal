using EP.SeaBattle.Common.Enums;
using System;

namespace EP.SeaBattle.Logic.Models
{
    public class Cell
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="status">Status</param>
        public Cell(byte x, byte y, CellStatus status)
        {
            X = x;
            Y = y;
            Status = status;
        }

        /// <summary>
        /// x-coordinate
        /// </summary>
        public byte X { get; set; }

        /// <summary>
        /// y-coordinate
        /// </summary>
        public byte Y { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public CellStatus Status { get; set; }

        public Ship Ship { get; }

        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;
            if (cell == null)
                return false;

            return this.X == cell.X && this.Y == cell.Y && this.Status == cell.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Status);
        }
    }
}

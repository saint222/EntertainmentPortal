

namespace EP.SeaBattle.Logic.Models
{
    public class Cell
    {
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
        public bool IsAlive { get; set; }

        public Ship Ship { get; }
    }
}

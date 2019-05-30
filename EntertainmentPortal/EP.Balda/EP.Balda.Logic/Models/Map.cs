using System.Collections.Generic;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     Game map with the value (size) of the measurement of the playing
    ///     space transferred to the constructor
    /// </summary>
    public class Map
    {
        private static readonly IInitially InitGame = new Initially(new Step());

        public int Size { get; }

        public List<Cell> Fields { get; }

        public Map(int size)
        {
            Size = size;
            Fields = InitGame.InitMap(size);
        }
    }
}
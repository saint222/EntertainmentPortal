using System.Collections.Generic;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Interfaces
{
    /// <summary>
    ///     Game init component interface
    /// </summary>
    public interface IInitGame
    {
        List<Cell> InitMap(int size);
    }
}
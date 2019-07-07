using System.Collections.Generic;
using System.Linq;
using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Logic.Models
{
    /// <summary>
    /// The class is responsible for checking the accuracy of the ships
    /// </summary>
    /// <remarks>
    /// Stores the playing field with marked cells
    /// </remarks>
    public class FieldManager
    {
        public const byte SIZE = 10;
        private ICollection<ForbiddenArea> forbiddenAreas;
        public FieldManager(IEnumerable<Ship> ships)
        {
            forbiddenAreas = new List<ForbiddenArea>();
            foreach (var ship in ships)
            {
                var cellMin = ship.Cells.OrderBy(c => c.X).ThenBy(c => c.Y).First();
                var cellMax = ship.Cells.OrderBy(c => c.X).ThenBy(c => c.Y).Last();
                forbiddenAreas.Add(new ForbiddenArea
                {
                    Xmin = (byte)(cellMin.X > 0 ? (cellMin.X - 1) : 0),
                    Ymin = (byte)(cellMin.Y > 0 ? cellMin.Y - 1 : 0),
                    Xmax = (byte)(cellMax.X < (SIZE - 1) ? (cellMax.X + 1) : (SIZE - 1)),
                    Ymax = (byte)(cellMax.Y < (SIZE - 1) ? (cellMax.Y + 1) : (SIZE - 1))
                });
            }
        }

        public bool CheckShip(byte x, byte y, ShipOrientation shipOrientation, ShipRank rank)
        {
            byte xMax;
            byte yMax;
            if (shipOrientation == ShipOrientation.Horizontal)
            {
                xMax = (byte)(x + rank);
                yMax = y;
            }
            else
            {
                yMax = (byte)(y + rank);
                xMax = x;
            }

            if (xMax >= SIZE || yMax >= SIZE)
            {
                return false;
            }
            foreach (var area in forbiddenAreas)
            {
                if ((x >= area.Xmin &&
                   x <= area.Xmax &&
                   y >= area.Ymin &&
                   y <= area.Ymax) ||
                   (xMax >= area.Xmin &&
                   xMax <= area.Xmax &&
                   yMax >= area.Ymin &&
                   yMax <= area.Ymax))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
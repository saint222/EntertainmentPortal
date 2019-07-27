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
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="fieldSize">Size of playing field</param>
        public FieldManager(IEnumerable<Ship> ships, IEnumerable<Shot> shots = null)
        {
            Cells = new Cell[SIZE, SIZE];
            for (byte x = 0; x < SIZE; x++)
            {
                for (byte y = 0; y < SIZE; y++)
                {
                    Cells[x, y] = new Cell(x, y, CellStatus.None);
                }
            }
            SetShipsOnField(ships);

            if (shots != null && shots.Count() > 0)
                SetShots(shots);
        }

        /// <summary>
        /// Cells collection
        /// </summary>
        public Cell[,] Cells { get; }

        //TODO Пересмотреть наличие данного метода и попытаться заменить его на GenerateCell 
        /// <summary>
        /// Add the ship at player field
        /// </summary>
        /// <param name="startPositionX">Start coord X</param>
        /// <param name="startPositionY">Start coord Y</param>
        /// <param name="shipOrientation">Ship orientation (vertical or horizontal)</param>
        /// <param name="rank">Ship rank (one, two, three or four)</param>
        /// <returns>return true if ship created at this cells</returns>
        public bool AddShip(byte startPositionX, byte startPositionY, ShipOrientation shipOrientation, ShipRank rank)
        {
            if (CanArrageShip(startPositionX, startPositionY, shipOrientation, rank))
            {
                List<Cell> cells = new List<Cell>((int)rank);
                if (shipOrientation == ShipOrientation.Horizontal)
                {
                    for (byte i = 0; i < (byte)rank; i++)
                    {
                        Cells[startPositionX, startPositionY].Status = CellStatus.Alive;
                        cells.Add(new Cell(startPositionX++, startPositionY, CellStatus.Alive));
                    }
                }
                else
                {
                    for (byte i = 0; i < (byte)rank; i++)
                    {
                        Cells[startPositionX, startPositionY].Status = CellStatus.Alive;
                        cells.Add(new Cell(startPositionX, startPositionY++, CellStatus.Alive));
                    }
                }
                SetForbiddenCellsAround(cells);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Checks if the ship can be placed in the cells
        /// </summary>
        /// <param name="startPositionX">Start coord X</param>
        /// <param name="startPositionY">Start coord Y</param>
        /// <param name="shipOrientation">Ship orientation (vertical or horizontal)</param>
        /// <param name="rank">Ship rank (one, two, three or four)</param>
        /// <returns>true if ship can be placed</returns>
        private bool CanArrageShip(byte startPositionX, byte startPositionY, ShipOrientation shipOrientation, ShipRank rank)
        {
            if (startPositionX >= SIZE || startPositionY >= SIZE)
                return false;

            var startCell = Cells[startPositionX, startPositionY];
            if (startCell.Status == CellStatus.Alive || startCell.Status == CellStatus.Forbidden)
                return false;

            if (shipOrientation == ShipOrientation.Horizontal)
                return CanArrageHorizontalShip(startCell, rank);
            else
                return CanArrageVerticalShip(startCell, rank);
        }

        /// <summary>
        /// Checks if the vertical ship can be placed in the cells
        /// </summary>
        /// <param name="startPoint">Start cell</param>
        /// <param name="rank">Ship rank</param>
        /// <returns>true if ship can be placed</returns>
        private bool CanArrageVerticalShip(Cell startPoint, ShipRank rank)
        {
            for (byte i = 0; i < (byte)rank; i++)
            {
                if ((startPoint.Y + i) < SIZE)
                {
                    var checkPoint = Cells[startPoint.X, startPoint.Y + i];
                    if (checkPoint.Status == CellStatus.Alive || checkPoint.Status == CellStatus.Forbidden)
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the horizontal ship can be placed in the cells
        /// </summary>
        /// <param name="startPoint">Start cell</param>
        /// <param name="rank">Ship rank</param>
        /// <returns>true if ship can be placed</returns>
        private bool CanArrageHorizontalShip(Cell startPoint, ShipRank rank)
        {
            for (byte i = 0; i < (byte)rank; i++)
            {
                if ((startPoint.X + i) < SIZE)
                {
                    var checkPoint = Cells[startPoint.X + i, startPoint.Y];
                    if (checkPoint.Status == CellStatus.Alive || checkPoint.Status == CellStatus.Forbidden)
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Marks the cells next to the ship as forbidden to install other ships
        /// </summary>
        /// <param name="cells">Сells around which you need to set a zone of forbidden cells</param>
        private void SetForbiddenCellsAround(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                for (int x = cell.X - 1; x <= cell.X + 1; x++)
                {
                    for (int y = cell.Y - 1; y <= cell.Y + 1; y++)
                    {
                        if (IsCellExistAndNotShip(x, y))
                        {
                            Cells[x, y].Status = CellStatus.Forbidden;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether there is such a cell reference, and if there is a ship
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>true if cell exists and cell not ship</returns>
        private bool IsCellExistAndNotShip(int x, int y)
        {
            if (x >= 0 && x < SIZE
                && y >= 0 && y < SIZE
                && Cells[x, y].Status != CellStatus.Alive
                && Cells[x, y].Status != CellStatus.Destroyed)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether there is such a cell reference, and if there is a ship
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>true if cell exists and cell not ship</returns>
        private bool IsCellExist(int x, int y)
        {
            if (x >= 0 && x < SIZE
                && y >= 0 && y < SIZE)
                return true;
            return false;
        }

        /// <summary>
        /// Marks cell at field as Ship and forbidden area
        /// </summary>
        /// <param name="ships"></param>
        private void SetShipsOnField(IEnumerable<Ship> ships)
        {
            foreach (var ship in ships)
            {
                SetShipCells(ship.Cells);
                SetForbiddenCellsAround(ship.Cells);
            }
        }

        /// <summary>
        /// Mark cell at field as Ship (alive or destroyed)
        /// </summary>
        /// <param name="cells"></param>
        private void SetShipCells(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Cells[cell.X, cell.Y].Status = cell.Status;
            }
        }

        /// <summary>
        /// Mark cell with status ShotWithOutHit
        /// </summary>
        /// <param name="shots"></param>
        private void SetShots(IEnumerable<Shot> shots)
        {
            foreach (var shot in shots)
            {
                Cells[shot.X, shot.Y].Status = shot.Status;
            }
        }
    }
} 
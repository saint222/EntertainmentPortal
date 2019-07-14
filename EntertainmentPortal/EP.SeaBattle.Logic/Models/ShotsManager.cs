using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Models
{
    public class ShotsManager
    {
        readonly Player _player;
        public bool isFinishedGame { get; set; }
        public ShotsManager(Player player)
        {
            _player = player;
        }

        public Ship TryShoot(byte x, byte y)
        {
            Ship shipResult = null;
            isFinishedGame = true;
            foreach (Ship ship in _player.Ships)
            {
                foreach (Cell cell in ship.Cells)
                {
                    if (cell.X == x && cell.Y == y)
                    {
                        cell.IsAlive = false;
                        shipResult = ship;
                        if (isFinishedGame == false)
                        {
                            break;
                        }
                    }
                }
                if (ship.IsAlive)
                {
                    isFinishedGame = false;
                    if(shipResult != null)
                    {
                        break;
                    }
                }
            }
            return shipResult;
        }
    }
}

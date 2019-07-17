using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Models
{
    public class ShotManager
    {
        private readonly Game _game;
        private readonly Player _player;
        private readonly IEnumerable<Ship> _ships;
        private FieldManager _fieldManager;
        List<Shot> _shots;

        public ShotManager(Game game, Player player, IEnumerable<Ship> ships, IEnumerable<Shot> shots)
        {
            _game = game;
            _player = player;
            _ships = ships;
            _shots = shots.ToList();
            _fieldManager = new FieldManager(_ships, _shots);
        }

        public IEnumerable<Shot> Shots { get => _shots; }

        /// <summary>
        /// Shoot on enemy field and receive result
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>Check <see cref="TryShoot(byte, byte)"/> status first. If can't shoot return shot with null playerId and null gameId</returns>
        public Shot Shoot(byte x, byte y)
        {
            if (TryShoot(x, y))
            {
                var shot = new Shot(_player.Id, _game.Id, x, y, CellStatus.None);
                if (_fieldManager.Cells[x, y].Status == CellStatus.Alive)
                    shot.Status = CellStatus.Destroyed;
                else
                    shot.Status = CellStatus.ShootWithoutHit;

                _shots.Add(shot);
                return shot;
            }
            return new Shot();
        }

        public bool TryShoot(byte x, byte y)
        {
            if (CanShot && (_fieldManager.Cells[x, y].Status == CellStatus.Alive 
                || _fieldManager.Cells[x, y].Status == CellStatus.None
                || _fieldManager.Cells[x, y].Status == CellStatus.Forbidden))
            {
                return true;
            }
            else
                return false;
        }

        private bool CanShot
        {
            get
            {
                return !_game.Finish && _game.PlayerAllowedToMove.Equals(_player);
            }
        }
    }
}

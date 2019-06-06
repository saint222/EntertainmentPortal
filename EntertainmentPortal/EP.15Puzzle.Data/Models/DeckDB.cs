using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Bogus;

namespace EP._15Puzzle.Data.Models
{
    public class DeckDb
    {
        public int Id { get; set; }

        public UserDb User { get; set; }
        public int UserId { get; set; }

        public int Score { get; set; } = 0;
        public bool Victory { get; set; } = false;

        public ICollection<TileDb> Tiles { get; set; }
        public TileDb EmptyTile { get; set; }

        public DeckDb(int size)
        {
            //now size = 4
            User = new UserDb(){};
            List<TileDb> tiles = new List<TileDb>();
            for (int i = 1; i <= 15; i++)
            {
                tiles.Add(new TileDb(i));
            }
            Tiles = tiles;
            Score = 0;
            Victory = false;

            EmptyTile = new TileDb(16);
        }

        public DeckDb()
        {

        }
    }
}

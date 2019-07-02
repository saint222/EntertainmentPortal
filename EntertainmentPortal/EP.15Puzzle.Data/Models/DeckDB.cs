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
        public int Size { get; set; }
        public UserDb User { get; set; }
        public string UserId { get; set; }

        public int Score { get; set; }
        public bool Victory { get; set; }

        public ICollection<TileDb> Tiles { get; set; }

        
    }
}

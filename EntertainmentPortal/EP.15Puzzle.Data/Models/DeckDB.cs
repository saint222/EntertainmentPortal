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

        /// <summary>
        /// UserId property
        /// </summary>
        /// <value>Represents ID of user the deck belongs</value>
        public int UserId { get; set; }

        /// <summary>
        /// Score property
        /// </summary>
        /// <value>Represents count of turns user already did</value>
        public int Score { get; set; }

        /// <summary>
        /// Victory property
        /// </summary>
        /// <value>Flag represents winning state of deck</value>
        public bool Victory { get; set; }

        [MaxLength(40)]
        public string _tiles { get; set; }

        /// <summary>
        /// Tiles property
        /// </summary>
        /// <remarks>
        ///Tiles[0] represents empty tile
        /// </remarks>
        /// <value>Represents a string with positions as tile numbers and values as their relevant positions on deck</value>
        [NotMapped]
        public List<int> Tiles
        {
            get { return _tiles.Split('|').Select(s => int.Parse(s)).ToList(); }
            set { _tiles = string.Join('|', value); }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using EP._15Puzzle.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Data.Context
{
    public class DeckDbContext : DbContext
    {
        public DeckDbContext(DbContextOptions<DeckDbContext> options)
            : base(options:options)
        { }

        public DbSet<DeckDb> Decks { get; set; }
    }
}

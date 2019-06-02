using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.Hangman.Data.Context
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) 
            : base(options: options)
        {
                
        }

        public DbSet<GameDb> Games { get; set; }
    }
}

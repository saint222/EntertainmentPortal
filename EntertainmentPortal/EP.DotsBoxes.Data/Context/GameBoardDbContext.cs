using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Data.Context
{
    public class GameBoardDbContext : DbContext
    {
        public GameBoardDbContext(DbContextOptions<GameBoardDbContext> options) 
            :base(options: options)
        {}

        public DbSet<GameBoardDb> GameBoard { get; set; }
    }
}

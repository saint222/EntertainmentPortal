using EP.Sudoku.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Context
{
    public class SudokuDbContext : DbContext
    {
        public SudokuDbContext(DbContextOptions<SudokuDbContext> options)
            : base(options: options)
        { }

        public DbSet<PlayerDb> Players { get; set; }
        public DbSet<AvatarIconDb> Icons { get; set; }
        public DbSet<CellDb> Cells { get; set; }
        public DbSet<SessionDb> Sessions { get; set; }
    }
}

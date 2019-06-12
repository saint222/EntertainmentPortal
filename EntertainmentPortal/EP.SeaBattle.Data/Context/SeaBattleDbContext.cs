using System;
using System.Collections.Generic;
using System.Text;
using EP.SeaBattle.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace EP.SeaBattle.Data.Context
{
    public class SeaBattleDbContext : DbContext
    {
        public SeaBattleDbContext(DbContextOptions<SeaBattleDbContext> options)
            : base(options: options)
        {

        }

        public DbSet<PlayerDb> Players { get; set; }

        public DbSet<GameDb> Games { get; set; }

        public DbSet<ShipDb> Ships { get; set; }

        public DbSet<CellDb> Cells { get; set; }
    }
}

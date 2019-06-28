using EP.TicTacToe.Data.Context.Configuration;
using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.TicTacToe.Data.Context
{
    public class TicTacDbContext : DbContext
    {
        public DbSet<PlayerDb> Players { get; set; }
        public DbSet<GameDb> Games { get; set; }
        public DbSet<MapDb> Maps { get; set; }
        public DbSet<CellDb> Cells { get; set; }
        public DbSet<PlayerGameDb> PlayerGames { get; set; }
        public DbSet<StepDb> Steps { get; set; }

        public TicTacDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerGameConfiguration());
            modelBuilder.ApplyConfiguration(new StepConfiguration());
            modelBuilder.ApplyConfiguration(new MapConfiguration());
            modelBuilder.ApplyConfiguration(new CellConfiguration());
        }
    }
}

// *******************************************
// * Example syntax for use migrations:
// * Add-Migration InitialCreateDb -OutputDir Migrations\GameDbMigrations -Context TicTacDbContext -Project EP.TicTacToe.Data -StartupProject EP.TicTacToe.Web
// * Update-Database InitialCreateDb -Context TicTacDbContext -Project EP.TicTacToe.Data -StartupProject EP.TicTacToe.Web
// *******************************************
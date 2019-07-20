using EP.DotsBoxes.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Data.Context
{
    public class GameBoardDbContext : IdentityDbContext
    {
        public GameBoardDbContext(DbContextOptions<GameBoardDbContext> options)
            : base(options: options)
        {

        }

        public DbSet<GameBoardDb> GameBoard { get; set; }
        public DbSet<CellDb> Cells { get; set; }
        public DbSet<PlayerDb> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var gameBoardEntity = modelBuilder.Entity<GameBoardDb>();
            gameBoardEntity.HasMany(g => g.Cells)
                .WithOne(c => c.GameBoard);
            gameBoardEntity.HasMany(g => g.Players)
               .WithOne(c => c.GameBoard);         
        }
    }
}

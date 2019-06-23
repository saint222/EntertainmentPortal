using EP.DotsBoxes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Data.Context
{
    public class GameBoardDbContext : DbContext
    {
        public GameBoardDbContext(DbContextOptions<GameBoardDbContext> options) 
            :base(options: options)
        {

        }

        public DbSet<GameBoardDb> GameBoard { get; set; }
        public DbSet<CellDb> Cells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CellDb>()
                .HasOne(p => p.GameBoard)
                .WithMany(b => b.Cells)
                .HasForeignKey(p => p.GameBoardId);
        }
    }
}

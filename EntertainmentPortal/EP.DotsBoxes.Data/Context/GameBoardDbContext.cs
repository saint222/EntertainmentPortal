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
        public DbSet<PlayerDb> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var gameBoardEntity = modelBuilder.Entity<GameBoardDb>();
            gameBoardEntity.HasKey(g => g.Id);
            gameBoardEntity.HasMany(g => g.Cells)
                .WithOne(g => g.GameBoard);

            var cellEntity = modelBuilder.Entity<CellDb>();
            cellEntity.HasKey(c => c.Id);
            cellEntity.Property(c => c.Row).IsRequired();
            cellEntity.Property(c => c.Column).IsRequired();
            cellEntity.Property(c => c.Top).IsRequired();
            cellEntity.Property(c => c.Bottom).IsRequired();
            cellEntity.Property(c => c.Left).IsRequired();
            cellEntity.Property(c => c.Right).IsRequired();
            cellEntity.HasOne(c => c.GameBoard)
                .WithMany(c => c.Cells)
                .HasForeignKey(c => c.GameBoardId);

            //var playerEntity = modelBuilder.Entity<PlayerDb>();
            //playerEntity.HasKey(p => p.Id);
            //playerEntity.Property(c => c.Name).IsRequired();
            //playerEntity.Property(c => c.Color).IsRequired();
            //playerEntity.Property(c => c.Score).IsRequired();
            //playerEntity.HasOne(c => c.GameBoard)
            //    .WithMany(c => c.Players)
            //    .HasForeignKey(c => c.GameBoardId);
        }
    }
}

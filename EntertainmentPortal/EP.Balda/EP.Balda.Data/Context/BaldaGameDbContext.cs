using EP.Balda.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Data.Context
{
    public class BaldaGameDbContext : DbContext
    {
        public BaldaGameDbContext(DbContextOptions<BaldaGameDbContext> options)
            : base(options: options)
        {
        }

        public DbSet<PlayerDb> Players { get; set; }

        public DbSet<GameDb> Games { get; set; }
        
        public DbSet<MapDb> Maps { get; set; }

        public DbSet<CellDb> Cells { get; set; }

        public DbSet<WordDb> Words { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var playerEntity = modelBuilder.Entity<PlayerDb>();
            playerEntity.HasKey(p => p.Id);
            playerEntity.Property(p => p.Login).IsRequired().HasMaxLength(30);
            playerEntity.Property(p => p.NickName).IsRequired().HasMaxLength(30);
            playerEntity.Property(p => p.Password).IsRequired();
            playerEntity.HasMany(p => p.Words).WithOne();
            
            var gameEntity = modelBuilder.Entity<GameDb>();
            gameEntity.HasKey(g => g.Id);
            gameEntity.HasMany(g => g.Players).WithOne();

            var mapEntity = modelBuilder.Entity<MapDb>();
            mapEntity.HasKey(m => m.Id);
            mapEntity.HasMany(m => m.Cells).WithOne().HasForeignKey(m => m.MapID);

            var cellEntity = modelBuilder.Entity<CellDb>();
            cellEntity.HasKey(c => c.Id);
            cellEntity.HasOne(c => c.Map).WithMany().HasForeignKey(c => c.MapID);

            var wordEntity = modelBuilder.Entity<WordDb>();
            wordEntity.HasKey(w => w.Id);
        }
    }
}

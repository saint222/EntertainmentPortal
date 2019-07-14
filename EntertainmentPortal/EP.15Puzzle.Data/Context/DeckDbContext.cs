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

        public DbSet<DeckDb> DeckDbs { get; set; }
        public DbSet<UserDb> UserDbs { get; set; }
        public DbSet<RecordDb> RecordDbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userEntity = modelBuilder.Entity<UserDb>();
            userEntity
                .HasOne(u => u.Deck)
                .WithOne(d => d.User)
                .HasForeignKey<DeckDb>(d => d.UserId);
            
            
            var deckEntity = modelBuilder.Entity<DeckDb>();
            deckEntity.HasMany(t => t.Tiles);
            deckEntity.Property(d => d.Victory).IsRequired();
            deckEntity.Property(d => d.Score).IsRequired();

            var tileEntity = modelBuilder.Entity<TileDb>();
            tileEntity.Property(f => f.Num).IsRequired();
            tileEntity.Property(f => f.Pos).IsRequired();
            
            

        }
    }
}

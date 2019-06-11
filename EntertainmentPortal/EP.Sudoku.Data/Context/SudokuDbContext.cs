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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvatarIconDb>().HasData(
                new AvatarIconDb[]
                {
                new AvatarIconDb { Id=1, Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png", IsBaseIcon = true},
                new AvatarIconDb { Id=2, Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Elf-icon.png", IsBaseIcon = false},
                new AvatarIconDb { Id=3, Uri = "http://icons.iconarchive.com/icons/paomedia/small-n-flat/64/cat-icon.png", IsBaseIcon = false},
                new AvatarIconDb { Id=4, Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Sorceress-Witch-icon.png", IsBaseIcon = false},
                new AvatarIconDb { Id=5, Uri = "http://icons.iconarchive.com/icons/rockettheme/free-christmas/64/Gingerman-icon.png", IsBaseIcon = false},
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}

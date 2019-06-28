﻿using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<GameDb>
    {
        public void Configure(EntityTypeBuilder<GameDb> builder)
        {
            builder.ToTable("Games");
            builder
                .HasOne(g => g.Map)
                .WithOne(g => g.Game)
                .HasForeignKey<MapDb>(g => g.GameId);
        }
    }
}
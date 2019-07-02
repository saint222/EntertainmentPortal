using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<GameDb>
    {
        public void Configure(EntityTypeBuilder<GameDb> builder)
        {
            builder.ToTable("Games").HasKey(g => g.Id);
            builder.HasMany(g => g.Maps).WithOne();
            builder.HasMany(g => g.Players).WithOne();
        }
    }
}
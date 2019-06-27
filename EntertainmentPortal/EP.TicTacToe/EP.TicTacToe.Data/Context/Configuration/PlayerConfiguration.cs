using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<PlayerDb>
    {
        public void Configure(EntityTypeBuilder<PlayerDb> builder)
        {
            builder.ToTable("Players").HasKey(p => p.Id);
            builder.Property(p => p.Login).IsRequired().HasMaxLength(30);
            builder.Property(p => p.NickName).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(8);
        }
    }
}
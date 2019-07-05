using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class FirstPlayerConfiguration : IEntityTypeConfiguration<FirstPlayerDb>
    {
        public void Configure(EntityTypeBuilder<FirstPlayerDb> builder)
        {
            builder.ToTable("FirstPlayers").HasKey(p => p.Id);
            builder.HasOne(f => f.Haunter).WithMany(f => f.FirstPlayers)
                .HasForeignKey(f => f.HaunterId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
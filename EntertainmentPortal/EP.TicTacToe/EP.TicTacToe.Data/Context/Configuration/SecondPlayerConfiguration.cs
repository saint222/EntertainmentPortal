using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class SecondPlayerConfiguration : IEntityTypeConfiguration<SecondPlayerDb>
    {
        public void Configure(EntityTypeBuilder<SecondPlayerDb> builder)
        {
            builder.ToTable("SecondPlayers").HasKey(p => p.Id);
            builder.HasOne(f => f.Haunter).WithMany(f => f.SecondPlayers)
                .HasForeignKey(f => f.HaunterId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
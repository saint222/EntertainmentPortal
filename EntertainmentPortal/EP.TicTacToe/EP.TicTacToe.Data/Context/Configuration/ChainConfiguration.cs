using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class ChainConfiguration : IEntityTypeConfiguration<ChainDb>
    {
        public void Configure(EntityTypeBuilder<ChainDb> builder)
        {
            builder.ToTable("Chains");
            builder.HasKey(c => c.Id);
            builder.HasAlternateKey(c => c.PlayerId);
            builder.HasIndex(c => c.PlayerId);
            builder.Property(c => c.PlayerId).IsRequired();
            builder.HasOne(c => c.Game).WithMany(c => c.Chains).HasForeignKey(c=>c.Id);
        }
    }
}
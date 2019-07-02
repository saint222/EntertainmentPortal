using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class ChainConfiguration : IEntityTypeConfiguration<ChainDb>
    {
        public void Configure(EntityTypeBuilder<ChainDb> builder)
        {
            builder.ToTable("Chains").HasKey(c => c.Id);
        }
    }
}
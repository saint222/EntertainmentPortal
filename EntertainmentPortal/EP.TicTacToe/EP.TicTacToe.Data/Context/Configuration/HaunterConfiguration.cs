using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class HaunterConfiguration : IEntityTypeConfiguration<HaunterDb>
    {
        public void Configure(EntityTypeBuilder<HaunterDb> builder)
        {
            builder.ToTable("Haunters").HasKey(p => p.Id);
        }
    }
}
using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class CellConfiguration : IEntityTypeConfiguration<CellDb>
    {
        public void Configure(EntityTypeBuilder<CellDb> builder)
        {
            builder.ToTable("Cells").HasKey(c => c.Id);
            builder.Property(c => c.X).IsRequired();
            builder.Property(c => c.Y).IsRequired();
            builder.Property(c => c.TicTac).IsRequired();
        }
    }
}
using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class StepConfiguration : IEntityTypeConfiguration<StepDb>
    {
        public void Configure(EntityTypeBuilder<StepDb> builder)
        {
            builder.ToTable("Steps");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.X).IsRequired();
            builder.Property(c => c.Y).IsRequired();
        }
    }
}
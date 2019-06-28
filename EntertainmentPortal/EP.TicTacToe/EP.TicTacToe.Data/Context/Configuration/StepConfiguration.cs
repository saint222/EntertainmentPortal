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
            builder
                .HasOne(s => s.Player)
                .WithMany(s => s.Steps)
                .IsRequired();
            builder
                .HasOne(s => s.Cell)
                .WithOne(s => s.StepDb)
                .HasForeignKey<CellDb>(s => s.StepId);
        }
    }
}
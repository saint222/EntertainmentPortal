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
                .HasOne(s => s.PlayerDb)
                .WithOne(s => s.StepDb)
                .HasForeignKey<PlayerDb>(s => s.StepId);
            builder
                .HasOne(s => s.CellDb)
                .WithOne(s => s.StepDb)
                .HasForeignKey<CellDb>(s => s.StepId);
        }
    }
}
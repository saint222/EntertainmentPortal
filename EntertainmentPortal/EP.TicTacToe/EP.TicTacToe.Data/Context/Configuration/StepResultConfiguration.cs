using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class StepResultConfiguration : IEntityTypeConfiguration<StepResultDb>
    {
        public void Configure(EntityTypeBuilder<StepResultDb> builder)
        {
            builder.ToTable("StepResults").HasKey(p => p.Id);
        }
    }
}
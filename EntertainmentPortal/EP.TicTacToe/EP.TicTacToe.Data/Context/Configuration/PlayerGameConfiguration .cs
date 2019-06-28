using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class PlayerGameConfiguration : IEntityTypeConfiguration<PlayerGameDb>
    {
        public void Configure(EntityTypeBuilder<PlayerGameDb> builder)
        {
            builder.ToTable("PlayerGames");
            builder
                .HasKey(pg => new {pg.PlayerId, pg.GameId});
            builder
                .HasOne(pg => pg.Player)
                .WithMany(pg => pg.PlayerGames)
                .HasForeignKey(pg => pg.PlayerId);
            builder
                .HasOne(pg => pg.Game)
                .WithMany(pg => pg.PlayerGames)
                .HasForeignKey(pg => pg.GameId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using EP.TicTacToe.Data.Models;

namespace EP.TicTacToe.Data.Context
{
    public class PlayerDbContext: DbContext
    {
        public PlayerDbContext (DbContextOptions<PlayerDbContext> options)
            : base(options: options)
        {

        }

        public DbSet<PlayerDb> Players { get; set; }

    //    protected override void OnModelCreating()
    }
}

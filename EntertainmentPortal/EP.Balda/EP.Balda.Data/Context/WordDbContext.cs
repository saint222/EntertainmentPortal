using EP.Balda.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Data.Context
{
    /// <summary>
    ///     words database context
    /// </summary>
    public class WordDbContext : DbContext
    {
        public DbSet<WordDb> Words { get; set; }

        public WordDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
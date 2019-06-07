using EP.Balda.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Data.Context
{
    public class WordDbContext : DbContext
    {
        public WordDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<WordDb> Words { get; set; }
    }
}

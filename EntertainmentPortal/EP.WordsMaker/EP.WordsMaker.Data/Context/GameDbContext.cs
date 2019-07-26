using EP.WordsMaker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Data.Context
{
	public class GameDbContext : DbContext
	{
		public GameDbContext(DbContextOptions<GameDbContext> options)
			: base(options: options)
		{ }
		public DbSet<PlayerDb> Players { get; set; }
		public DbSet<GameDb> Games { get; set; }
		public DbSet<WordDb> Words { get; set; }
	}
}
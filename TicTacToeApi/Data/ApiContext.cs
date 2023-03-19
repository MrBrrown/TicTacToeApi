using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Models;

namespace TicTacToeApi.Data
{
	public class ApiContext : DbContext
	{
		public DbSet<Game> Games { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Board> Boards { get; set; }

		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}
	}
}


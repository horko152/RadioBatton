using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
	public class RadioBattonDbContext : DbContext
	{
		public RadioBattonDbContext() { }
		public DbSet<User> Users { get; set; }
		public DbSet<Song> Songs { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Like> Likes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=DESKTOP-MFSAQ2U;Database=RadioDb;Trusted_Connection=True;");
		}

		//public RadioBattonDbContext(DbContextOptions<RadioBattonDbContext> options) : base(options)
		//{

		//}

		//public static string GetConnectionString()
		//{
		//	return Startup.ConnectionString;
		//}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	if (!optionsBuilder.IsConfigured)
		//	{
		//		var con = GetConnectionString();
		//		optionsBuilder.UseSqlServer(con);
		//	}
		//}
	}
}

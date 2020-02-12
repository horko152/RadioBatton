using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
	class RadioBattonDbContext : DbContext
	{
		public RadioBattonDbContext() { }
		public DbSet<User> Users { get; set; }
		public DbSet<Song> Songs { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Like> Likes { get; set; }

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

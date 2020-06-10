using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
	public class SongInfo
	{
		public int Id { get; set; }
		public string Artist { get; set; }
		public string Name { get; set; }
		public int Ranking { get; set; }
	}
}

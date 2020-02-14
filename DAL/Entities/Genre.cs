using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("genres")]
	public class Genre
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("genrename")]
		public string GenreName { get; set; }

		[Column("description")]
		public string Description { get; set; }

		[JsonIgnore]
		public ICollection<Song> Songs { get; set; }
	}
}

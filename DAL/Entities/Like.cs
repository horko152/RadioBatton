using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("likes")]
	public class Like
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Column("like")]
		public bool LikeValue { get; set; }

		//[JsonIgnore]
		[Column("songId")]
		public int SongId { get; set; }

		[ForeignKey("SongId")]
		[JsonIgnore]
		public Song Song { get; set; }

	}
}

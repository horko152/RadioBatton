using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("songs")]
	class Song
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("artist")]
		public string Artist { get; set; }

		[Required]
		[Column("songname")]
		public string SongName { get; set; }

		[Column("album")]
		public string Album { get; set; }

		[Column("releasedate")]
		public DateTime ReleaseDate { get; set; }

		[Required]
		[Column("file")]
		public string File { get; set; }

		//[JsonIgnore]
		[Column("userid")]
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		[JsonIgnore]
		public User User { get; set; }


	}
}

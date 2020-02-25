﻿using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("users")]
	public class User
	{
		public User()
		{
			Created_At = DateTime.Now;
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }
		[Required]
		[Column("created_at")]
		public DateTime Created_At { get; set; }
		[Required]
		[Column("email")]
		public string Email { get; set; }
		[Required]
		[Column("username")]
		public string UserName { get; set; }
		[Required]
		[Column("password")]
		public byte Password { get; set; }

		[Required]
		[Column("role")]
		public Role Role { get; set; }

		[JsonIgnore]
		public ICollection<Song> Songs { get; set; }
	}
}

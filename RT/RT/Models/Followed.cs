﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Followed
	{
		[Key]
		public int ID { get; set; }


		[ForeignKey("ApplicationUser")]
		public string FollowedUserID { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
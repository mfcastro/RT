using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Follower_Followed_Join
	{
		[Key]
		public int ID { get; set; }


		[ForeignKey("Followers")]
		public int ? FollowerUserID { get; set; }

		public virtual Followers Followers { get; set; }

		[ForeignKey("Followed")]
		public int ? FollowedUserID { get; set; }

		public virtual Followed Followed { get; set; }
	}
}
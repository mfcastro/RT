using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class RecipeCollection
	{
		[Key]
		public int ID { get; set; }

		public string CollectionName { get; set; }


		[ForeignKey("ApplicationUser")]
		public string UserID { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }


		public bool IsList { get; set; }
		public bool IsBox { get; set; }
	}
}
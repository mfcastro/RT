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


		[Display(Name = "Collection Name")]
		public string CollectionName { get; set; }


		[ForeignKey("ApplicationUser")]
		public string UserID { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }

		[Display(Name = "Is this a Private Collection?")]
		public bool PrivateCollection { get; set; }

		[Display(Name = "Is this a Public Collection?")]
		public bool PublicCollection { get; set; }

		public bool IncludeCollection { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Author
	{
		[Key]
		public int ID { get; set; }

		[Display(Name = "Original Author")]
		public string AuthorName { get; set; }

		[Display(Name = "Recipe URL")]
		public string RecipeURL { get; set; }

	}
}
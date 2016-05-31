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

		public string AuthorName { get; set; }

		public string RecipeURL { get; set; }

	}
}
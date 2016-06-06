using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Direction
	{
		[Key]
		public int ID { get; set; }

		[Display(Name = "Directions:")]
		public string RecipeDirection1 { get; set; }
		public string RecipeDirection2 { get; set; }

		public string RecipeDirection3 { get; set; }
		public string RecipeDirection4 { get; set; }
		public string RecipeDirection5 { get; set; }
		public string RecipeDirection6 { get; set; }
		public string RecipeDirection7 { get; set; }
		public string RecipeDirection8 { get; set; }

		public string RecipeDirection9 { get; set; }
		public string RecipeDirection10 { get; set; }
		public string RecipeDirection11 { get; set; }
		public string RecipeDirection12 { get; set; }
		public string RecipeDirection13 { get; set; }
		public string RecipeDirection14 { get; set; }
		public string RecipeDirection15 { get; set; }
		public int DirectionOrderNumber { get; set; }

	}
}
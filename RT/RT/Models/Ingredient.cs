using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Ingredient
	{
		[Key]
		public int ID { get; set; }

		public string RecipeIngredient { get; set; }
		public int RecipeOrderNumber { get; set; }

	}
}
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

		[Display(Name ="Ingredients:")]
		public string RecipeIngredient1 { get; set; }

		public string RecipeIngredient2 { get; set; }

		public string RecipeIngredient3 { get; set; }

		public string RecipeIngredient4 { get; set; }
		
		public string RecipeIngredient5 { get; set; }

		public string RecipeIngredient6 { get; set; }

		public string RecipeIngredient7 { get; set; }

		public string RecipeIngredient8 { get; set; }

		public string RecipeIngredient9{ get; set; }

		public string RecipeIngredient10{ get; set; }

		public string RecipeIngredient11 { get; set; }
		public string RecipeIngredient12 { get; set; }
		public string RecipeIngredient13 { get; set; }
		public string RecipeIngredient14 { get; set; }
		public string RecipeIngredient15 { get; set; }

		public int RecipeOrderNumber { get; set; }

	}
}
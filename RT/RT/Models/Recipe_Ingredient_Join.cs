using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Recipe_Ingredient_Join
	{
		[Key]
		public int ID { get; set; }

		[ForeignKey("Ingredient")]
		public int IngredientID { get; set; }

		public virtual Ingredient Ingredient { get; set; }


		[ForeignKey("Recipe")]
		public int RecipeID { get; set; }
		public virtual Recipe Recipe { get; set; }
	}
}
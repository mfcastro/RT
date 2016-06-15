using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Recipe_Image_Join
	{
		[Key]
		public int ID { get; set; }

		[ForeignKey("RecipeImage")]
		public int RecipeImageID { get; set; }

		public virtual RecipeImage RecipeImage { get; set; }


		[ForeignKey("Recipe")]
		public int RecipeID { get; set; }
		public virtual Recipe Recipe { get; set; }
	}
}
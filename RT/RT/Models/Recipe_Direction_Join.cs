using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Recipe_Direction_Join
	{
		[Key]
		public int ID { get; set; }

		//[ForeignKey("Direction")]
		//public int DirectionID { get; set; }

		//public virtual RecipeCollection RecipeCollection { get; set; }


		[ForeignKey("Recipe")]
		public int RecipeID { get; set; }
		public virtual Recipe Recipe { get; set; }
	}
}
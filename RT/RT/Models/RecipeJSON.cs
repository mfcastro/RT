using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class RecipeJSON
	{
		public string recipeTitle { get; set; }
		public List<string> ingredients { get; set; }
		public List<string> directions { get; set; }
		public string recipeURL { get; set; }
	}
}
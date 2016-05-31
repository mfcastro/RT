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

		public string RecipeDirection { get; set; }
		public int DirectionOrderNumber { get; set; }

	}
}
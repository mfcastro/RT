using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RT.Models
{
	public class Recipe
	{
		[Key]
		public int ID { get; set; }

		public string Title { get; set; }
		[Display(Name = "Cooking Time")]
		public string CookingTime { get; set; }


		
		[ForeignKey("Author")]
		public int AuthorID { get; set; }

		public virtual Author Author { get; set; }


		//[ForeignKey("PostedByUser")]
		//public int ?  PostedByID { get; set; }
		//public virtual PostedByUser PostedByUser { get; set; }


	}
}
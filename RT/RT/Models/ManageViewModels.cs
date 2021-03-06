﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace RT.Models
{
	public class IndexViewModel
	{
		public bool HasPassword { get; set; }
		public IList<UserLoginInfo> Logins { get; set; }
		public string PhoneNumber { get; set; }
		public bool TwoFactor { get; set; }
		public bool BrowserRemembered { get; set; }
	}

	public class ManageLoginsViewModel
	{
		public IList<UserLoginInfo> CurrentLogins { get; set; }
		public IList<AuthenticationDescription> OtherLogins { get; set; }
	}

	public class FactorViewModel
	{
		public string Purpose { get; set; }
	}

	public class SetPasswordViewModel
	{
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangePasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Current password")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class AddPhoneNumberViewModel
	{
		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		public string Number { get; set; }
	}

	public class VerifyPhoneNumberViewModel
	{
		[Required]
		[Display(Name = "Code")]
		public string Code { get; set; }

		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
	}

	public class ConfigureTwoFactorViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
	}


	public class RecipeViewModel
	{

		public Recipe Recipe { get; set; }

		public Author Author { get; set; }

		public RecipeImage RecipeImage { get; set; }
		public Ingredient Ingredients { get; set; }

		public Direction Directions { get; set; }


		public Recipe_Ingredient_Join Recipe_Ingredient_Join { get; set; }

		public Recipe_Direction_Join Recipe_Direction_Join { get; set; }

		public Recipe_Image_Join Recipe_Image_Join { get; set; }

	}

	public class RecipeCollectionViewModel
	{
		public List<RecipeCollection> RecipeCollectionList { get; set; }
		public RecipeCollection RecipeCollection { get; set; }

		public Ingredient Ingredients { get; set; }

		public Direction Directions { get; set; }

		//public List<Recipe> RecipeList { get; set; }
		public Recipe Recipe { get; set; }

		public List<Recipe_Collection_Join> Recipe_Collection_Join_List { get; set; }

		public Recipe_Collection_Join Recipe_Collection_Join { get; set; }

		public List<RecipeViewModel> RecipeViewModelList{ get; set; }



	}

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using EdgeJs;
using System.IO;
using Newtonsoft.Json;

namespace RT.Controllers
{
	[Authorize]
    public class RecipesController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
		protected ApplicationDbContext ApplicationDbContext { get; set; }
		protected UserManager<ApplicationUser> UserManager { get; set; }



		//Linked up. Need to change it to a relative path.
		static Func<object, Task<object>> outsideFunc = Edge.Func(@"return require('C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\Scripts\\myfunc.js')");


		// GET: Recipes
		public ActionResult Index()
        {
			RecipeImageViewModel recipeWithImages = new RecipeImageViewModel();
			recipeWithImages.ListRecipeViewModel = new List<RecipeViewModel>();

			var recipe = db.Recipe.Include(r => r.Author).ToList();


			////---------------------------------------------------------------------------------------------------------

			for (int i = 0; i < recipe.Count; i++)
			{
				RecipeViewModel recipeViewModel = new RecipeViewModel();
				var recipeItem = recipe[i];


				if (recipeItem.ID == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				recipeViewModel.Recipe = db.Recipe.Find(recipeItem.ID);

				//var ingredientID = db.Recipe_Ingredient_Join.Where(r => r.RecipeID == recipeItem.ID).SingleOrDefault().IngredientID;
				//var directionID = db.Recipe_Direction_Join.Where(r => r.RecipeID == recipeItem.ID).SingleOrDefault().DirectionID;


				//recipeViewModel.Ingredients = db.Ingredient.Find(ingredientID);
				//recipeViewModel.Directions = db.Direction.Find(directionID);


				try
				{
					var imageID = db.Recipe_Image_Join.Where(r => r.RecipeID == recipeItem.ID).SingleOrDefault().RecipeImageID;
					recipeViewModel.RecipeImage = db.RecipeImage.Find(imageID);
				}
				catch
				{

				}


				recipeWithImages.ListRecipeViewModel.Add(recipeViewModel);
			}

			//return View(recipe);
			return View(recipeWithImages.ListRecipeViewModel);

		}

        // GET: Recipes/Details/5
        public ActionResult Details(/*int? id*/ Recipe recipe)
        {

			RecipeViewModel recipeViewModel = new RecipeViewModel();

			if (recipe.ID == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			recipeViewModel.Recipe = db.Recipe.Find(recipe.ID);

			var ingredientID = db.Recipe_Ingredient_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().IngredientID;
			var directionID = db.Recipe_Direction_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().DirectionID;


			recipeViewModel.Ingredients = db.Ingredient.Find(ingredientID);
			recipeViewModel.Directions = db.Direction.Find(directionID);


			try
			{
				var imageID = db.Recipe_Image_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().RecipeImageID;
				recipeViewModel.RecipeImage = db.RecipeImage.Find(imageID);
			}
			catch
			{

			}
			

			if (recipeViewModel.Recipe == null)
			{
				return HttpNotFound();
			}
			return View(recipeViewModel);

		}

		//public ActionResult AddRecipe()
		//{
		//	return View();
		//}

		//[HttpPost]
		//public ActionResult AddRecipe(RecipeViewModel recipe)
		//{
		//	return View("Index");
		//}


		// GET: Recipes/Create
		public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Author, "ID", "AuthorName");
            return View();
        }

		// POST: Recipes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(/*[Bind(Include = "ID,Title,CookingTime,AuthorID")]*/ RecipeViewModel recipeViewModel)
		{

			try
			{
					HttpPostedFileBase File = Request.Files[0];

					//if (File.ContentLength > (2 * 1024 * 1024))
					//{
					//	ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
					//	//return View();
					//}
					//if (!(File.ContentType == "image/jpeg" || File.ContentType == "image/gif"))
					//{
					//	ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
					//	//return View();
					//}

					recipeViewModel.RecipeImage.FileName = File.FileName;
					recipeViewModel.RecipeImage.ImageSize = File.ContentLength;

					byte[] data = new byte[File.ContentLength];
					File.InputStream.Read(data, 0, File.ContentLength);

					recipeViewModel.RecipeImage.ImageData = data;

					db.RecipeImage.Add(recipeViewModel.RecipeImage);
				
		
			}
			catch
			{

			}
			



			//if (recipeViewModel.Author.RecipeURL.Contains("www"))
			//{
			//	return RedirectToAction("GetRecipeFromURL", "Recipe",recipeViewModel.Recipe);
			//}

			//else 
			if (ModelState.IsValid)
            {
				recipeViewModel.Recipe.UserID = User.Identity.GetUserId();


				db.Recipe.Add(recipeViewModel.Recipe);
				db.Author.Add(recipeViewModel.Author);
				db.Ingredient.Add(recipeViewModel.Ingredients);
				db.Direction.Add(recipeViewModel.Directions);

				Recipe_Direction_Join join_direction_recipe = new Recipe_Direction_Join();
				Recipe_Ingredient_Join join_ingredient_recipe = new Recipe_Ingredient_Join();
				Recipe_Image_Join join_image_recipe = new Recipe_Image_Join();


				join_direction_recipe.DirectionID = recipeViewModel.Directions.ID;
				join_direction_recipe.RecipeID = recipeViewModel.Recipe.ID;

				join_ingredient_recipe.IngredientID = recipeViewModel.Ingredients.ID;
				join_ingredient_recipe.RecipeID = recipeViewModel.Recipe.ID;

				join_image_recipe.RecipeImageID = recipeViewModel.RecipeImage.ID;
				join_image_recipe.RecipeID = recipeViewModel.Recipe.ID;

				recipeViewModel.Recipe_Direction_Join = join_direction_recipe;
				recipeViewModel.Recipe_Ingredient_Join = join_ingredient_recipe;
				recipeViewModel.Recipe_Image_Join = join_image_recipe;


				db.Recipe_Direction_Join.Add(recipeViewModel.Recipe_Direction_Join);
				db.Recipe_Ingredient_Join.Add(recipeViewModel.Recipe_Ingredient_Join);
				db.Recipe_Image_Join.Add(recipeViewModel.Recipe_Image_Join);

				db.SaveChanges();

				return RedirectToAction("Index");
            }
			
			

            ViewBag.Recipe = new SelectList(db.Recipe, "ID", "Title", recipeViewModel.Recipe.Title);
            return View(recipeViewModel);
        }


        // GET: Recipes/Edit/5
        public ActionResult Edit(Recipe recipe)
        {
			//if (id == null)
			//{
			//    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//}
			//Recipe recipe = db.Recipe.Find(id);
			//if (recipe == null)
			//{
			//    return HttpNotFound();
			//}
			//ViewBag.AuthorID = new SelectList(db.Author, "ID", "AuthorName", recipe.AuthorID);
			//return View(recipeRecipeViewModel recipeViewModel = new RecipeViewModel();

			RecipeViewModel recipeViewModel = new RecipeViewModel();


			if (recipe.ID == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			recipeViewModel.Recipe = db.Recipe.Find(recipe.ID);

			var ingredientID = db.Recipe_Ingredient_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().IngredientID;
			var directionID = db.Recipe_Direction_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().DirectionID;

			recipeViewModel.Ingredients = db.Ingredient.Find(ingredientID);
			recipeViewModel.Directions = db.Direction.Find(directionID);

			recipeViewModel.Author = db.Author.Find(recipe.AuthorID);


			if (recipeViewModel.Recipe == null)
			{
				return HttpNotFound();
			}
			return View(recipeViewModel);


		}

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "ID,Title,CookingTime,AuthorID")] Recipe recipe*/ RecipeViewModel recipeViewModel)
        {
			if (ModelState.IsValid)
            {
                db.Entry(recipeViewModel.Recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.AuthorID = new SelectList(db.Author, "ID", "AuthorName", recipeViewModel.Author.ID);
            return View(recipeViewModel);

        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(/*int? id*/ Recipe recipe)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Recipe recipe = db.Recipe.Find(id);
            //if (recipe == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(recipe);

			RecipeViewModel recipeViewModel = new RecipeViewModel();

			if (recipe.ID == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			recipeViewModel.Recipe = db.Recipe.Find(recipe.ID);

			var ingredientID = db.Recipe_Ingredient_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().IngredientID;
			var directionID = db.Recipe_Direction_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().DirectionID;

			recipeViewModel.Ingredients = db.Ingredient.Find(ingredientID);
			recipeViewModel.Directions = db.Direction.Find(directionID);


			if (recipeViewModel.Recipe == null)
			{
				return HttpNotFound();
			}
			return View(recipeViewModel);

		}

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Recipe recipe = db.Recipe.Find(id);
            db.Recipe.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



		public ActionResult GetRecipeFromURL()
		{
			return View();
		}


		[HttpPost]
		public async Task<ActionResult>  GetRecipeFromURL(Recipe recipe)
		{

			//System.IO.File.WriteAllText("C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\Scripts\\data.json", string.Empty);


			try
			{
				//DO NOT DELETE!!!!!!!!!!!!!!!!!!	
				await outsideFunc(recipe.Author.RecipeURL);

				RecipeViewModel recipeViewModel = new RecipeViewModel();


				using (StreamReader r = new StreamReader("C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\Scripts\\data.json"))
				{
					string json = r.ReadToEnd();
					RecipeJSON recipeJSON = JsonConvert.DeserializeObject<RecipeJSON>(json);
					//List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);

					Ingredient JSON_Ingredients = new Ingredient();
					Direction JSON_Directions = new Direction();

					try
					{
						JSON_Ingredients.RecipeIngredient1 = recipeJSON.ingredients[0];
						JSON_Ingredients.RecipeIngredient2 = recipeJSON.ingredients[1];
						JSON_Ingredients.RecipeIngredient3 = recipeJSON.ingredients[2];
						JSON_Ingredients.RecipeIngredient4 = recipeJSON.ingredients[3];
						JSON_Ingredients.RecipeIngredient5 = recipeJSON.ingredients[4];
						JSON_Ingredients.RecipeIngredient6 = recipeJSON.ingredients[5];
						JSON_Ingredients.RecipeIngredient7 = recipeJSON.ingredients[6];
						JSON_Ingredients.RecipeIngredient8 = recipeJSON.ingredients[7];
						JSON_Ingredients.RecipeIngredient9 = recipeJSON.ingredients[8];
						JSON_Ingredients.RecipeIngredient10 = recipeJSON.ingredients[9];
						JSON_Ingredients.RecipeIngredient11 = recipeJSON.ingredients[10];
						JSON_Ingredients.RecipeIngredient12 = recipeJSON.ingredients[11];
						JSON_Ingredients.RecipeIngredient13 = recipeJSON.ingredients[12];
						JSON_Ingredients.RecipeIngredient14 = recipeJSON.ingredients[13];
						JSON_Ingredients.RecipeIngredient15 = recipeJSON.ingredients[14];
					}
					catch (Exception e)
					{

					}


					try
					{
						JSON_Directions.RecipeDirection1 = recipeJSON.directions[0];
						JSON_Directions.RecipeDirection2 = recipeJSON.directions[1];
						JSON_Directions.RecipeDirection3 = recipeJSON.directions[2];
						JSON_Directions.RecipeDirection4 = recipeJSON.directions[3];
						JSON_Directions.RecipeDirection5 = recipeJSON.directions[4];
						JSON_Directions.RecipeDirection6 = recipeJSON.directions[5];
						JSON_Directions.RecipeDirection7 = recipeJSON.directions[6];
						JSON_Directions.RecipeDirection8 = recipeJSON.directions[7];
						JSON_Directions.RecipeDirection9 = recipeJSON.directions[8];
						JSON_Directions.RecipeDirection10 = recipeJSON.directions[9];
						JSON_Directions.RecipeDirection11 = recipeJSON.directions[10];
						JSON_Directions.RecipeDirection12 = recipeJSON.directions[11];
						JSON_Directions.RecipeDirection13 = recipeJSON.directions[12];
						JSON_Directions.RecipeDirection14 = recipeJSON.directions[13];
						JSON_Directions.RecipeDirection15 = recipeJSON.directions[14];

					}
					catch (Exception e)
					{

					}
					Recipe JSON_Recipe = new Recipe();
					JSON_Recipe.Title = recipeJSON.recipeTitle;

					Author JSON_Author = new Author();
					JSON_Author.AuthorName = recipeJSON.recipeURL;

					RecipeImage JSON_Image = new RecipeImage();

					JSON_Image.FileName = recipeJSON.recipeImage;


					recipeViewModel.Ingredients = JSON_Ingredients;
					recipeViewModel.Directions = JSON_Directions;
					recipeViewModel.Recipe = JSON_Recipe;
					recipeViewModel.Author = JSON_Author;
					recipeViewModel.RecipeImage = JSON_Image;
				}



				db.Recipe.Add(recipeViewModel.Recipe);
				db.Author.Add(recipeViewModel.Author);
				db.Ingredient.Add(recipeViewModel.Ingredients);
				db.Direction.Add(recipeViewModel.Directions);
				db.RecipeImage.Add(recipeViewModel.RecipeImage);

				Recipe_Direction_Join join_direction_recipe = new Recipe_Direction_Join();
				Recipe_Ingredient_Join join_ingredient_recipe = new Recipe_Ingredient_Join();
				Recipe_Image_Join join_image_recipe = new Recipe_Image_Join();


				join_direction_recipe.DirectionID = recipeViewModel.Directions.ID;
				join_direction_recipe.RecipeID = recipeViewModel.Recipe.ID;

				join_ingredient_recipe.IngredientID = recipeViewModel.Ingredients.ID;
				join_ingredient_recipe.RecipeID = recipeViewModel.Recipe.ID;

				join_image_recipe.RecipeImageID = recipeViewModel.RecipeImage.ID;
				join_image_recipe.RecipeID = recipeViewModel.Recipe.ID;

				recipeViewModel.Recipe_Direction_Join = join_direction_recipe;
				recipeViewModel.Recipe_Ingredient_Join = join_ingredient_recipe;
				recipeViewModel.Recipe_Image_Join = join_image_recipe;


				db.Recipe_Direction_Join.Add(recipeViewModel.Recipe_Direction_Join);
				db.Recipe_Ingredient_Join.Add(recipeViewModel.Recipe_Ingredient_Join);
				db.Recipe_Image_Join.Add(recipeViewModel.Recipe_Image_Join);


				db.SaveChanges();

				System.IO.File.WriteAllText("C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\Scripts\\data.json", string.Empty);



				return View("Details", recipeViewModel);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return View("Error");
			}
			
		}


		public ActionResult Error()
		{
			return View();
		}


		public ActionResult AddToCollection(Recipe recipe)
		{

			RecipeCollectionViewModel recipeCollectionViewModel = new RecipeCollectionViewModel();

			recipeCollectionViewModel.Recipe = db.Recipe.Find(recipe.ID);


			//recipeCollectionViewModel.RecipeCollectionList = db.RecipeCollection.ToList();

			string currentUserId = User.Identity.GetUserId();
			ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

			recipeCollectionViewModel.RecipeCollectionList = db.RecipeCollection.Where(r=>r.UserID == currentUser.Id).ToList();

			var ingredientID = db.Recipe_Ingredient_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().IngredientID;
			var directionID = db.Recipe_Direction_Join.Where(r => r.RecipeID == recipe.ID).SingleOrDefault().DirectionID;

			recipeCollectionViewModel.Ingredients = db.Ingredient.Find(ingredientID);
			recipeCollectionViewModel.Directions = db.Direction.Find(directionID);

			return View(recipeCollectionViewModel);
		}

		[HttpPost]
		public ActionResult AddToCollection(RecipeCollectionViewModel recipeCollectionViewModel)
		{
			recipeCollectionViewModel.Recipe = db.Recipe.Find(recipeCollectionViewModel.Recipe.ID);

			var ingredientID = db.Recipe_Ingredient_Join.Where(r => r.RecipeID == recipeCollectionViewModel.Recipe.ID).SingleOrDefault().IngredientID;
			var directionID = db.Recipe_Direction_Join.Where(r => r.RecipeID == recipeCollectionViewModel.Recipe.ID).SingleOrDefault().DirectionID;
			recipeCollectionViewModel.Ingredients = db.Ingredient.Find(ingredientID);
			recipeCollectionViewModel.Directions = db.Direction.Find(directionID);

			Recipe_Collection_Join join_collection_recipe = new Recipe_Collection_Join();

			join_collection_recipe.RecipeCollectionID = recipeCollectionViewModel.RecipeCollection.ID;
			join_collection_recipe.RecipeID = recipeCollectionViewModel.Recipe.ID;
			db.Recipe_Collection_Join.Add(join_collection_recipe);
			db.SaveChanges();

			return RedirectToAction("Index");
			//return View(recipeCollectionViewModel);
		}





		/////////////////////////////////////////////////////////////////////

		public ActionResult GalleryTemp()
		{
			List<RecipeImage> all = new List<RecipeImage>();

			all = db.RecipeImage.ToList();

			return View(all);
		}


		public ActionResult UploadTemp()
		{
			return View();
		}


		[HttpPost]
		public ActionResult UploadTemp(RecipeImage IG)
		{
			HttpPostedFileBase File = Request.Files[0];

			// Apply Validation Here


			if (File.ContentLength > (2 * 1024 * 1024))
			{
				ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
				return View();
			}
			if (!(File.ContentType == "image/jpeg" || File.ContentType == "image/gif"))
			{
				ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
				return View();
			}

			IG.FileName = File.FileName;
			IG.ImageSize = File.ContentLength;

			byte[] data = new byte[File.ContentLength];
			File.InputStream.Read(data, 0, File.ContentLength);

			IG.ImageData = data;

			db.RecipeImage.Add(IG);
			db.SaveChanges();

			return RedirectToAction("GalleryTemp");
		}


		public void UploadRecipeImage(RecipeImage IG)
		{
			HttpPostedFileBase File = Request.Files[0];

			// Apply Validation Here


			//if (File.ContentLength > (2 * 1024 * 1024))
			//{
			//	ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
			//	return View();
			//}
			//if (!(File.ContentType == "image/jpeg" || File.ContentType == "image/gif"))
			//{
			//	ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
			//	return View();
			//}

			IG.FileName = File.FileName;
			IG.ImageSize = File.ContentLength;

			byte[] data = new byte[File.ContentLength];
			File.InputStream.Read(data, 0, File.ContentLength);

			IG.ImageData = data;

			db.RecipeImage.Add(IG);
			db.SaveChanges();
		}

	}
}

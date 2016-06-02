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

namespace RT.Controllers
{
	[Authorize]
    public class RecipesController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Recipes
        public ActionResult Index()
        {
			RecipeViewModel getRecipes = new RecipeViewModel();



			var recipe = db.Recipe.Include(r => r.Author).ToList();
			//var recipe = db.Recipe.ToList();

			return View(recipe);

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


			if (recipeViewModel.Recipe == null)
			{
				return HttpNotFound();
			}
			return View(recipeViewModel);

		}

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
            if (ModelState.IsValid)
            {


                db.Recipe.Add(recipeViewModel.Recipe);
				db.Author.Add(recipeViewModel.Author);
				db.Ingredient.Add(recipeViewModel.Ingredients);
				db.Direction.Add(recipeViewModel.Directions);

				Recipe_Direction_Join join_direction_recipe = new Recipe_Direction_Join();
				Recipe_Ingredient_Join join_ingredient_recipe = new Recipe_Ingredient_Join();

				join_direction_recipe.DirectionID = recipeViewModel.Directions.ID;
				join_direction_recipe.RecipeID = recipeViewModel.Recipe.ID;

				join_ingredient_recipe.IngredientID = recipeViewModel.Ingredients.ID;
				join_ingredient_recipe.RecipeID = recipeViewModel.Recipe.ID;

				recipeViewModel.Recipe_Direction_Join = join_direction_recipe;
				recipeViewModel.Recipe_Ingredient_Join = join_ingredient_recipe;

				db.Recipe_Direction_Join.Add(recipeViewModel.Recipe_Direction_Join);
				db.Recipe_Ingredient_Join.Add(recipeViewModel.Recipe_Ingredient_Join);

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
    }
}

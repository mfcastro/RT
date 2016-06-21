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
    public class RecipeCollectionsController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


		protected ApplicationDbContext ApplicationDbContext { get; set; }
		protected UserManager<ApplicationUser> UserManager { get; set; }




		// GET: RecipeCollections
		public ActionResult Index()
		{
			//var recipeCollection = db.RecipeCollection.Include(r => r.ApplicationUser);
			//return View(recipeCollection.ToList());




			string currentUserId = User.Identity.GetUserId();
			ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
			//var recipeCollection = db.RecipeCollection.Include(r => r.ApplicationUser).Where(c => c.ApplicationUser == currentUser);
			var recipeCollection = db.RecipeCollection.ToList().Where(c => c.ApplicationUser == currentUser);
			return View(recipeCollection);
		}

		//// GET: RecipeCollections/Details/5
		//public ActionResult Details(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	RecipeCollection recipeCollection = db.RecipeCollection.Find(id);

		//	RecipeCollectionViewModel recipeCollectionViewModel = new RecipeCollectionViewModel();
		//	recipeCollectionViewModel.RecipeCollection = recipeCollection;


		//	recipeCollectionViewModel.Recipe_Collection_Join_List = db.Recipe_Collection_Join.Where(r => r.RecipeCollection.ID == recipeCollectionViewModel.RecipeCollection.ID).ToList();


		//	////------------------------------------------------------------------------------------------------------------------------------------
		//	//for (int i = 0; i < recipeCollectionViewModel.Recipe_Collection_Join_List.Count; i++)
		//	//{
		//	//	RecipeViewModel recipeViewModel = new RecipeViewModel();
		//	//	var recipeItem = recipeCollectionViewModel.Recipe_Collection_Join_List[i];


		//	//	if (recipeItem.ID == null)
		//	//	{
		//	//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	//	}
		//	//	recipeViewModel.Recipe = db.Recipe.Find(recipeItem.ID);

		//	//	try
		//	//	{
		//	//		var imageID = db.Recipe_Image_Join.Where(r => r.RecipeID == recipeItem.ID).SingleOrDefault().RecipeImageID;
		//	//		recipeViewModel.RecipeImage = db.RecipeImage.Find(imageID);
		//	//	}
		//	//	catch
		//	//	{

		//	//	}
		//	//	//------------------------------------------------------------------------------------------------------------------------------------
		//	//	//if (recipeCollection == null)
		//	//	//{
		//	//	//    return HttpNotFound();
		//	//	//}

		//	//}

		//	return View(recipeCollectionViewModel);

		//}

		//--------------TEMORARY DETAILS CONTROLLER EXPERIEMENT-----------------------------------------------------------------------------------------------------------------------
		// GET: RecipeCollections/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			RecipeCollection recipeCollection = db.RecipeCollection.Find(id);

			RecipeCollectionViewModel recipeCollectionViewModel = new RecipeCollectionViewModel();
			//List<RecipeViewModel> ListRecipeViewModel = new List<RecipeViewModel>();

			recipeCollectionViewModel.RecipeViewModelList = new List<RecipeViewModel>();

			var ListRecipeViewModel = recipeCollectionViewModel.RecipeViewModelList;



			recipeCollectionViewModel.RecipeCollection = recipeCollection;


			recipeCollectionViewModel.Recipe_Collection_Join_List = db.Recipe_Collection_Join.Where(r => r.RecipeCollection.ID == recipeCollectionViewModel.RecipeCollection.ID).ToList();



			for(int i = 0; i < recipeCollectionViewModel.Recipe_Collection_Join_List.Count(); i++)
			{
				////------------------------------------------------------------------------------------------------------------------------------------
				//for (int i = 0; i < recipeCollectionViewModel.Recipe_Collection_Join_List.Count; i++)
				//{
					RecipeViewModel recipeViewModel = new RecipeViewModel();
					var recipeItem = recipeCollectionViewModel.Recipe_Collection_Join_List[i];

				recipeViewModel.Recipe = recipeItem.Recipe;
				

				//	if (recipeItem.ID == null)
				//	{
				//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				//	}
				//	recipeViewModel.Recipe = db.Recipe.Find(recipeItem.ID);

				try
				{
					var imageID = db.Recipe_Image_Join.Where(r => r.RecipeID == recipeItem.Recipe.ID).SingleOrDefault().RecipeImageID;
					recipeViewModel.RecipeImage = db.RecipeImage.Find(imageID);
				}
				catch
				{

				}
				//	//------------------------------------------------------------------------------------------------------------------------------------
				//	//if (recipeCollection == null)
				//	//{
				//	//    return HttpNotFound();
				//	//}

				//}

				ListRecipeViewModel.Add(recipeViewModel);

			}

			return View(recipeCollectionViewModel);

		}



		//--------------END OF NEW DETAILS CONTROLLER EXPERIMENT-----------------------------------------------------------------------------------------------------------------------





		// GET: RecipeCollections/Create
		public ActionResult Create()
        {
           // ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            return View();
        }

        // POST: RecipeCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CollectionName,UserID,IsList,IsBox")] RecipeCollection recipeCollection)
        {

			recipeCollection.UserID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
				db.RecipeCollection.Add(recipeCollection);

				db.SaveChanges();

				

				return RedirectToAction("Index");
            }

            //ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FirstName", recipeCollection.UserID);
            return View(recipeCollection);
        }

        // GET: RecipeCollections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeCollection recipeCollection = db.RecipeCollection.Find(id);
            if (recipeCollection == null)
            {
                return HttpNotFound();
            }
           // ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FirstName", recipeCollection.UserID);
            return View(recipeCollection);
        }

        // POST: RecipeCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CollectionName,UserID,IsList,IsBox")] RecipeCollection recipeCollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeCollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FirstName", recipeCollection.UserID);
            return View(recipeCollection);
        }

        // GET: RecipeCollections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeCollection recipeCollection = db.RecipeCollection.Find(id);
            if (recipeCollection == null)
            {
                return HttpNotFound();
            }
            return View(recipeCollection);
        }

        // POST: RecipeCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecipeCollection recipeCollection = db.RecipeCollection.Find(id);
            db.RecipeCollection.Remove(recipeCollection);
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

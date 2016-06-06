using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RT.Models;

namespace RT.Controllers
{
    public class RecipeCollectionsController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecipeCollections
        public ActionResult Index()
        {
            var recipeCollection = db.RecipeCollection.Include(r => r.ApplicationUser);
            return View(recipeCollection.ToList());
        }

        // GET: RecipeCollections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeCollection recipeCollection = db.RecipeCollection.Find(id);

			RecipeCollectionViewModel recipeCollectionViewModel = new RecipeCollectionViewModel();
			recipeCollectionViewModel.RecipeCollection = recipeCollection;


			recipeCollectionViewModel.Recipe_Collection_Join_List = db.Recipe_Collection_Join.Where(r => r.RecipeCollection.ID == recipeCollectionViewModel.RecipeCollection.ID).ToList();
			//recipeCollectionViewModel.RecipeCollectionList = db.RecipeCollection.ToList();


			//if (recipeCollection == null)
			//{
			//    return HttpNotFound();
			//}

			return View(recipeCollectionViewModel);
			//return View(recipeCollection);
        }

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

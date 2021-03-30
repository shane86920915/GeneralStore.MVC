using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View(_db.products.ToList());//Returns the created products from database
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //get : Delete
        //product/Delete/{id}
        public ActionResult Delete (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Product product = _db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _db.products.Find(id);
            _db.products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
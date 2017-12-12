using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SongPlayer.Models;

namespace SongPlayer.Controllers
{
    public class GenresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View();
        }

        // Get Products list

        public ActionResult GetProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var tblGenres = db.Genres.ToList();

            return Json(tblGenres, JsonRequestBehavior.AllowGet);
        }

        // Get product by id
        public ActionResult Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var Genre = db.Genres.ToList().Find(x => x.GenreId == id);
            return Json(Genre, JsonRequestBehavior.AllowGet);
        }

        // Create a new product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Exclude = "GenreId,Songs")] Genre Genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(Genre);
                db.SaveChanges();
            }

            return Json(Genre, JsonRequestBehavior.AllowGet);
        }

        // Update product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([Bind(Exclude = "Songs")]Genre Genre)
        {

            if (ModelState.IsValid)
            {
                db.Entry(Genre).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(Genre, JsonRequestBehavior.AllowGet);
        }

        // Delete product by id
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var Genre = db.Genres.ToList().Find(x => x.GenreId == id);
            if (Genre != null)
            {
                db.Genres.Remove(Genre);
                db.SaveChanges();
            }

            return Json(Genre, JsonRequestBehavior.AllowGet);
        }
    }
}

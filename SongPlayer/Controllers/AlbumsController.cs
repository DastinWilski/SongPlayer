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
    public class AlbumsController : Controller
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
            var tblAlbums = db.Albums.ToList();

            return Json(tblAlbums, JsonRequestBehavior.AllowGet);
        }
       

        // Get product by id
        public ActionResult Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var Album = db.Albums.ToList().Find(x => x.AlbumId == id);
            return Json(Album, JsonRequestBehavior.AllowGet);
        }

        // Create a new product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Exclude = "AlbumId,Songs")] Album Album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(Album);
               db.SaveChanges();
            }

            return Json(Album, JsonRequestBehavior.AllowGet);
        }

        // Update product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([Bind(Exclude = "Songs")]Album Album)
        {
          
            if (ModelState.IsValid)
            {
                db.Entry(Album).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(Album, JsonRequestBehavior.AllowGet);
        }

        // Delete product by id
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var Album = db.Albums.ToList().Find(x => x.AlbumId == id);
            if (Album != null)
            {
                db.Albums.Remove(Album);
                db.SaveChanges();
            }

            return Json(Album, JsonRequestBehavior.AllowGet);
        }

    }
}

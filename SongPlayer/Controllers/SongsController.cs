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
    public class SongsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Songs
        
        public ActionResult Index()
        {
            var myFiles = db.myFiles.Include(s => s.Album).Include(s => s.Genre).Include(s => s.myType);
            return View(myFiles.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.myFiles.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        [Authorize]
        public ActionResult Create()
        {
           
            ViewBag.AlbumID = new SelectList(db.Albums, "AlbumId", "AlbumName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreId", "GenreName");
            ViewBag.myTypeID = new SelectList(db.myTypes, "myTypeId", "TypeName");
            return View();
        }

        // POST: Songs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileId,FileName,Path,AlbumID,GenreID,myTypeID")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.myFiles.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumID = new SelectList(db.Albums, "AlbumId", "AlbumName", song.AlbumID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreId", "GenreName", song.GenreID);
            ViewBag.myTypeID = new SelectList(db.myTypes, "myTypeId", "TypeName", song.myTypeID);
            return View(song);
        }

        // GET: Songs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.myFiles.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumID = new SelectList(db.Albums, "AlbumId", "AlbumName", song.AlbumID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreId", "GenreName", song.GenreID);
            ViewBag.myTypeID = new SelectList(db.myTypes, "myTypeId", "TypeName", song.myTypeID);
            return View(song);
        }

        // POST: Songs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,FileName,Path,AlbumID,GenreID,myTypeID")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumID = new SelectList(db.Albums, "AlbumId", "AlbumName", song.AlbumID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreId", "GenreName", song.GenreID);
            ViewBag.myTypeID = new SelectList(db.myTypes, "myTypeId", "TypeName", song.myTypeID);
            return View(song);
        }

        // GET: Songs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.myFiles.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.myFiles.Find(id);
            db.myFiles.Remove(song);
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

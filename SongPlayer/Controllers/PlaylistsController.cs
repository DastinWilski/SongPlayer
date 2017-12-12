using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SongPlayer.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace SongPlayer.Controllers
{
    public class PlaylistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BusinessLayer.BusinessLayerx BL = new BusinessLayer.BusinessLayerx();
       [Authorize]
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            
            IEnumerable<Playlist> movies = new List<Playlist>();
            if ((!string.IsNullOrEmpty(userid))&&User.IsInRole("User"))
            {
                movies = db.Playlists.Where(x => x.ApplicationUserId == userid);
            }else if(!string.IsNullOrEmpty(userid))
            {
                movies = db.Playlists;
            }

            return View(movies);
        }

        // GET: Playlists/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(BL.GetPlaylist(id,playlist));
        }

        // GET: Playlists/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Playlists/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaylistId,PlaylistName,ApplicationUserId")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                playlist.ApplicationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Playlists.Add(playlist);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", playlist.ApplicationUserId);
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", playlist.ApplicationUserId);
            return View(BL.EditPlaylist(id,playlist));
        }

        // POST: Playlists/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlaylistViewModel playlist)
        {
            if (ModelState.IsValid)
            {
                var MyPlaylist = db.Playlists.Find(playlist.PlaylistId);
                MyPlaylist.PlaylistName = playlist.PlaylistName;

                foreach (var item in db.FilesToPlaylists)
                {
                    if (item.PlaylistID == playlist.PlaylistId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in playlist.Songs)
                {
                    if (item.Checked)
                    {
                        db.FilesToPlaylists.Add(new SongsToPlaylist() { PlaylistID = playlist.PlaylistId, FileID = item.Id });
                    }

                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            db.Playlists.Remove(playlist);
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

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
    public class myTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: myTypes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.myTypes.ToList());
        }

        // GET: myTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            myType myType = db.myTypes.Find(id);
            if (myType == null)
            {
                return HttpNotFound();
            }
            return View(myType);
        }

        // GET: myTypes/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: myTypes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "myTypeId,TypeName")] myType myType)
        {
            if (ModelState.IsValid)
            {
                db.myTypes.Add(myType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myType);
        }

        // GET: myTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            myType myType = db.myTypes.Find(id);
            if (myType == null)
            {
                return HttpNotFound();
            }
            return View(myType);
        }

        // POST: myTypes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "myTypeId,TypeName")] myType myType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myType);
        }

        // GET: myTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            myType myType = db.myTypes.Find(id);
            if (myType == null)
            {
                return HttpNotFound();
            }
            return View(myType);
        }

        // POST: myTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            myType myType = db.myTypes.Find(id);
            db.myTypes.Remove(myType);
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

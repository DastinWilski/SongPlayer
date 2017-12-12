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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            return View();
        }

        // Get Products list
        [Authorize(Roles = "Admin")]
        public ActionResult GetProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var tblmyTypes = db.myTypes.ToList();

            return Json(tblmyTypes, JsonRequestBehavior.AllowGet);
        }

        // Get product by id
        [Authorize(Roles = "Admin")]
        public ActionResult Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var myType = db.myTypes.ToList().Find(x => x.myTypeId == id);
            return Json(myType, JsonRequestBehavior.AllowGet);
        }

        // Create a new product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Exclude = "myTypeId,Songs")] myType myType)
        {
            if (ModelState.IsValid)
            {
                db.myTypes.Add(myType);
                db.SaveChanges();
            }

            return Json(myType, JsonRequestBehavior.AllowGet);
        }

        // Update product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([Bind(Exclude = "Songs")]myType myType)
        {

            if (ModelState.IsValid)
            {
                db.Entry(myType).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(myType, JsonRequestBehavior.AllowGet);
        }

        // Delete product by id
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var myType = db.myTypes.ToList().Find(x => x.myTypeId == id);
            if (myType != null)
            {
                db.myTypes.Remove(myType);
                db.SaveChanges();
            }

            return Json(myType, JsonRequestBehavior.AllowGet);
        }
    }
}

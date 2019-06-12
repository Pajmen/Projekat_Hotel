using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekat_Hotel.Models;

namespace Projekat_Hotel.Controllers.Admin
{
    [Authorize]
    public class SobasController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();

        
        public ActionResult Index()
        {
            var sobas = db.Sobas.Include(s => s.StatusSobe);
            return View(sobas.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobas.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.StatusSobes, "StatusID", "NazivStatusa", soba.StatusID);
            return View(soba);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.StatusSobes, "StatusID", "NazivStatusa", soba.StatusID);
            return View(soba);
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

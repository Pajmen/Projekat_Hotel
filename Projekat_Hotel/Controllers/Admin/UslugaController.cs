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
    public class UslugaController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();

        // GET: Uslugas
        public ActionResult SveUsluge()
        {
            return View(db.Uslugas.ToList());
        }




        [Authorize(Roles = "Admin")]
        public ActionResult NovaUsluga()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaUsluga([Bind(Include = "UslugaID,NazivUsluge,Cena")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                db.Uslugas.Add(usluga);
                db.SaveChanges();
                return RedirectToAction("SveUsluge");
            }

            return View(usluga);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Izmeni(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Uslugas.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni(Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usluga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SveUsluge");
            }
            return View(usluga);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ObrisiUslugu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Uslugas.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        
        [HttpPost, ActionName("ObrisiUslugu")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usluga usluga = db.Uslugas.Find(id);
            db.Uslugas.Remove(usluga);
            db.SaveChanges();
            return RedirectToAction("SveUsluge");
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

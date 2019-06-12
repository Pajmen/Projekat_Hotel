using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekat_Hotel.Models;

namespace Projekat_Hotel.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    public class AlatiController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();


        public ActionResult SveUloge()
        {
            return View(db.Ulogas.ToList());
        }




        public ActionResult NovaUloga()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaUloga(Uloga uloga)
        {
            if (ModelState.IsValid)
            {
                db.Ulogas.Add(uloga);
                db.SaveChanges();
                return RedirectToAction("SveUloge");
            }

            return View(uloga);
        }


        public ActionResult Izmeni(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uloga uloga = db.Ulogas.Find(id);
            if (uloga == null)
            {
                return HttpNotFound();
            }
            return View(uloga);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni(Uloga uloga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uloga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SveUloge");
            }
            return View(uloga);
        }

        // GET: Ulogas/Delete/5
        public ActionResult Obrisi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uloga uloga = db.Ulogas.Find(id);
            if (uloga == null)
            {
                return HttpNotFound();
            }
            return View(uloga);
        }

        // POST: Ulogas/Delete/5
        [HttpPost, ActionName("Obrisi")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uloga uloga = db.Ulogas.Find(id);
            db.Ulogas.Remove(uloga);
            db.SaveChanges();
            return RedirectToAction("SveUloge");
        }



        public ActionResult SviStatusi()
        {
            return View(db.StatusSobes.ToList());
        }



        public ActionResult NoviStatus()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoviStatus(StatusSobe statusSobe)
        {
            if (ModelState.IsValid)
            {
                db.StatusSobes.Add(statusSobe);
                db.SaveChanges();
                return RedirectToAction("SviStatusi");
            }

            return View(statusSobe);
        }


        public ActionResult IzmeniStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusSobe statusSobe = db.StatusSobes.Find(id);
            if (statusSobe == null)
            {
                return HttpNotFound();
            }
            return View(statusSobe);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IzmeniStatus(StatusSobe statusSobe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusSobe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SviStatusi");
            }
            return View(statusSobe);
        }

        
        public ActionResult ObrisiStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusSobe statusSobe = db.StatusSobes.Find(id);
            if (statusSobe == null)
            {
                return HttpNotFound();
            }
            return View(statusSobe);
        }


        [HttpPost, ActionName("ObrisiStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSasvim(int id)
        {
            StatusSobe statusSobe = db.StatusSobes.Find(id);
            db.StatusSobes.Remove(statusSobe);
            db.SaveChanges();
            return RedirectToAction("SviStatusi");
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

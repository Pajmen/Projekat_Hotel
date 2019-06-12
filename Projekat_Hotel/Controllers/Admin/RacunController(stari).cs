using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekat_Hotel.Models;

namespace Projekat_Hotel.Controllers
{
    public class RacunController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();

        // GET:
        public ActionResult SviRacuni()
        {
            var racuns = db.Racuns.Include(r => r.Rezervacija);
            return View("SviRacuni", racuns.ToList());
        }

        
        public ActionResult Detalji(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // Pravimo select listu načina plaćanja i šaljemo na View
        public ActionResult NoviRacun()
        {
            var selectList = new SelectList(
             new List<SelectListItem>
                {
                new SelectListItem {Text = "Gotovina", Value = "Gotovina"},
                new SelectListItem {Text = "Kartica", Value = "Kartica"},
                 new SelectListItem {Text = "Čekovi", Value = "Čekovi"},
                }, "Value", "Text");

            ViewBag.nacinPlacanja = selectList;
            ViewBag.RezervacijaID = new SelectList(db.Rezervacijas, "RezervacijaID", "RezervacijaID");

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoviRacun(Racun racun)
        {
            if (ModelState.IsValid)
            {
                Rezervacija rezervacijaKojaSeBrise = (from re in db.Rezervacijas where re.RezervacijaID == racun.RezervacijaID select re).Single();
                Soba sobaKojaSeOslobadja = (from s in db.Sobas where s.SobaID == rezervacijaKojaSeBrise.SobaID select s).Single();
                sobaKojaSeOslobadja.StatusID = 1004;
                Racun r = new Racun();
                r.RezervacijaID = racun.RezervacijaID;

                r.NacinPlacanja = racun.NacinPlacanja;
                r.Iznos = (double)rezervacijaKojaSeBrise.Cena;
                db.Racuns.Add(r);

                db.SaveChanges();
                int t = r.RacunID;
                return RedirectToAction("Detalji", new { @id = t });
                //return RedirectToAction("Index");
            }

            ViewBag.RezervacijaID = new SelectList(db.Rezervacijas, "RezervacijaID", "GostID", racun.RezervacijaID);
            return View(racun);
        }
        
        public ActionResult Storno(int? id)
        {
          //  int id =r.RacunID;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }

            try {
                racun.Storno = "STORNO";
                db.SaveChanges();
                
            }
            catch ( NullReferenceException e) { }
            return View("Storno");


        }


        public ActionResult Izmeni(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            ViewBag.RezervacijaID = new SelectList(db.Rezervacijas, "RezervacijaID", "RezervacijaID", racun.RezervacijaID);
            return View(racun);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni([Bind(Include = "RacunID,RezervacijaID,NacinPlacanja,Iznos")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SviRacuni");
            }
            ViewBag.RezervacijaID = new SelectList(db.Rezervacijas, "RezervacijaID", "GostID", racun.RezervacijaID);
            return View(racun);
        }

        //Ovo ne bi trebalo da postoji nego da bude STORNO
        //public ActionResult ObrisiRacun(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Racun racun = db.Racuns.Find(id);
        //    if (racun == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(racun);
        //}

        
        //[HttpPost, ActionName("ObrisiRacun")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Racun racun = db.Racuns.Find(id);
        //    db.Racuns.Remove(racun);
        //    db.SaveChanges();
        //    return RedirectToAction("SviRacuni");
        //}

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

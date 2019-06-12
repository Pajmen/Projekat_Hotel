using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekat_Hotel.Models;

namespace Projekat_Hotel.Controllers.Admin
{
    [Authorize]
    public class RadnikController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();


        public ActionResult Index()
        {

            return View();
        }





        [Authorize(Roles ="Admin")]
        public ActionResult SviRadnici()
        {
            var radniks = db.Radniks.Include(r => r.Uloga);
            return View(radniks.ToList());
        }

        
        public ActionResult Detalji(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radnik radnik = db.Radniks.Find(id);
            if (radnik == null)
            {
                return HttpNotFound();
            }
            return View(radnik);
        }

        [Authorize(Roles = "Admin, Menadžer")]
        public ActionResult NoviRadnik()
        {
            
            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "NazivUloge");
            return View();
        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult NoviRadnik(Radnik radnik)
        
        {
            try
            {
                Radnik t = (from r in db.Radniks where r.JMBG == radnik.JMBG select r).Single();
                if (t != null)
                {
                    TempData["Greska"] = "Taj JMBG vec postoji  bazi";
                    //return RedirectToAction("NoviRadnik");
                }

            }
            catch (Exception e) { }

            if (ModelState.IsValid)

                try
                {
                    if (radnik.KorisnickoIme == "" || radnik.KorisnickoIme == null)
                    { radnik.UlogaID = 4; }
                    db.Radniks.Add(radnik);
                    db.SaveChanges();
                    return RedirectToAction("SviRadnici");
                }

                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are:", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    return View();
                }
                catch (Exception e) { }



            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "NazivUloge", radnik.UlogaID);

            return View(radnik);
        }


        [Authorize(Roles ="Admin")]
        public ActionResult Izmeni(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radnik radnik = db.Radniks.Find(id);
            if (radnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "NazivUloge", radnik.UlogaID);
            return View(radnik);
        }

       
       
        [HttpPost]
        //[ValidateAntiForgeryToken]
           public ActionResult Izmeni(Radnik radnik)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Radnik izmene = (from r in db.Radniks where r.JMBG == radnik.JMBG select r).Single();
                    izmene.JMBG = radnik.JMBG;
                    izmene.Ime = radnik.Ime;
                    izmene.Prezime = radnik.Prezime;
                    izmene.Adresa = radnik.Adresa;
                    izmene.DatumZaposlenja = radnik.DatumZaposlenja;
                    izmene.RadnoMesto = radnik.RadnoMesto;
                    izmene.KorisnickoIme = radnik.KorisnickoIme;
                    izmene.Lozinka = radnik.Lozinka;
                    izmene.Telefon = radnik.Telefon;
                    if (radnik.KorisnickoIme == "" || radnik.KorisnickoIme == null)
                    { izmene.UlogaID = 4; }
                    else { izmene.UlogaID = radnik.UlogaID; }
                    db.SaveChanges();
                    return RedirectToAction("SviRadnici");
                }
                catch(Exception e) { }
            }
            ViewBag.UlogaID = new SelectList(db.Ulogas, "UlogaID", "NazivUloge", radnik.UlogaID);
            return View(radnik);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ObrisiRadnika(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radnik radnik = db.Radniks.Find(id);
            if (radnik == null)
            {
                return HttpNotFound();
            }
            return View(radnik);
        }

        
        [HttpPost, ActionName("ObrisiRadnika")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Radnik radnik = db.Radniks.Find(id);
            db.Radniks.Remove(radnik);
            db.SaveChanges();
            return RedirectToAction("SviRadnici");
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

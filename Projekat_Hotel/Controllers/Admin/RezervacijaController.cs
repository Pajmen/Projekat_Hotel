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
    public class RezervacijaController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();

        
        public ActionResult SveRezervacije()
        {
            var rezervacijas = db.Rezervacijas.Include(r => r.Gost).Include(r => r.Soba).Include(r => r.Usluga);
            return View(rezervacijas.ToList());
        }

        
        public ActionResult Detalji(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        
        public ActionResult NovaRezervacija()

        {
           
            ViewBag.GostID = new SelectList(db.Gosts, "GostID", "Ime");
            ViewBag.SobaID = new SelectList(Ponuda.SveSobe(), "SobaID", "SobaID");
            ViewBag.UslugaID = new SelectList(db.Uslugas, "UslugaID", "NazivUsluge");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaRezervacija(ModelFormularRezervacija model)

        {
          if (ModelState.IsValid)
           {
               
                Soba rezervisanaSoba = (from s in db.Sobas where s.SobaID == model.SobaID select s).Single();
                Gost noviGost = (from go in db.Gosts where go.GostID == model.GostID select go).FirstOrDefault();
                

                
                Rezervacija rez = new Rezervacija();

                if (rezervisanaSoba != null)
                {
                    rezervisanaSoba.StatusID = rez.InitStatusSobe(model.Check_In);
                }
                rez.GostID = model.GostID;
                rez.SobaID = model.SobaID;
                rez.Check_In = model.Check_In;
                rez.Check_Out = model.Check_Out;
                rez.UslugaID = model.UslugaID;
                rez.Cena = Ponuda.IznosRacuna(rez);
                db.Rezervacijas.Add(rez);

                if (noviGost == null)
                {
                    Gost g = new Gost();
                    g.GostID = model.GostID;
                    g.Ime = model.Ime;
                    g.Prezime = model.Prezime;
                    g.Email = model.Email;
                    db.Gosts.Add(g);
                }
                
                try
               {
                   
                    db.SaveChanges();
               }
                  catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                   { // Retrieve the error messages as a list of strings.
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
           
            return RedirectToAction("SveRezervacije");

        }
            
            //ViewBag.GostID = new SelectList(db.Gosts, "GostID", "Ime", model.GostID);
            ViewBag.SobaID = new SelectList(db.Sobas, "SobaID", "SobaID", model.SobaID);
            ViewBag.UslugaID = new SelectList(db.Uslugas, "UslugaID", "NazivUsluge", model.UslugaID);
            return View(model);
        }

        
        public ActionResult Izmeni(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            //ViewBag.GostID = new SelectList(db.Gosts, "GostID", "Ime", rezervacija.GostID);
            ViewBag.SobaID = new SelectList(db.Sobas, "SobaID", "SobaID", rezervacija.SobaID);
            ViewBag.UslugaID = new SelectList(db.Uslugas, "UslugaID", "NazivUsluge", rezervacija.UslugaID);
            return View(rezervacija);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni(Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervacija).State = EntityState.Modified;
                rezervacija.Cena = Ponuda.IznosRacuna(rezervacija);
                db.SaveChanges();
                return RedirectToAction("SveRezervacije");
            }
            //ViewBag.GostID = new SelectList(db.Gosts, "GostID", "Ime", rezervacija.GostID);
            ViewBag.SobaID = new SelectList(db.Sobas, "SobaID", "SobaID", rezervacija.SobaID);
            ViewBag.UslugaID = new SelectList(db.Uslugas, "UslugaID", "NazivUsluge", rezervacija.UslugaID);
            return View(rezervacija);
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

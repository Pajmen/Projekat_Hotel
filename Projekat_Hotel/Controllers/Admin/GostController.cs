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
    public class GostController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();

        [Authorize(Roles = "Admin, Menadžer")]
        public ActionResult SviGosti()
        {
            var danas = DateTime.Now;

            var sviGosti = (from   r in db.Rezervacijas
                            where (DateTime.Compare(r.Check_In ,danas)<= 0 && DateTime.Compare(r.Check_Out, danas) >=0) select r).ToList();
           
            
            return View(sviGosti);
        }

        [Authorize(Roles = "Admin, Menadžer")]
        public ActionResult BazaGostiju()
        {
           
        var bazaGostiju = db.Gosts.ToList();
            return View(bazaGostiju);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IzmeniGosta(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               return Content("Hi there! ID");
            }
           
            Gost gost = (from g in db.Gosts where g.GostID.Trim() == id select g).FirstOrDefault();
            if (gost == null)
            {
                //  return HttpNotFound();
                return Content("Hi there! GOST");
            }
            return View(gost);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IzmeniGosta(Gost gost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BazaGostiju");
            }
            return View(gost);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ObrisiGosta(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gosts.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
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

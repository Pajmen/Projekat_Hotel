using Projekat_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat_Hotel.Controllers.Recepcija
{
    [Authorize]
    public class RecepcijaController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SviZaposleni()
        {
            //var sviRadnici = db.Radniks;
            return RedirectToAction("SviZaposleni", "Manager");
        }

        public ActionResult SveSobe()
        {
            //var sveSobe = db.Sobas;
            return  RedirectToAction("SveSobe", "Manager");
        }
        //// Da li je ovo potrebno recepciji?
        //public ActionResult SviRacuni()
        //{

        //    return RedirectToAction("SviRacuni", "Manager");
        //}
        public ActionResult SviGosti()

        {

            return RedirectToAction("SviGosti", "Manager");
            //var sviGosti = (from r in db.Rezervacijas where r.Soba.StatusID == 1002 select r).ToList();

            //return View(sviGosti);
        }
        public ActionResult SveRezervacije()
        {

            return RedirectToAction("SveRezervacije", "Rezervacija");

        }

        public ActionResult NoviRacun()
        {
            return RedirectToAction("NoviRacun", "Racun");
        }


        //public ActionResult DetaljiORezervaciji(int? id)
        //{

        //    return RedirectToAction("Detalji", "Rezervacija");

        //}


        //public ActionResult NovaRezervacija()

        //{
        //    return RedirectToAction("NovaRezervacija", "Rezervacija");
        //    }


        //public ActionResult Izmeni(int? id)
        //{

        //    return RedirectToAction("Izmeni", "Rezervacija");

        //}
    }
}
using Projekat_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat_Hotel.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private Hotel_DREntities db = new Hotel_DREntities();
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Menadžer")]
        public ActionResult SviZaposleni()
        {
            var sviRadnici = db.Radniks;
            return View(sviRadnici.ToList());
        } 

        public ActionResult SveSobe()
        {
            var sveSobe = db.Sobas;
            return View(sveSobe.ToList());
        }

        public ActionResult SviRacuni()
        {
            var sviRacuni = db.Racuns;
            return View(sviRacuni);
        }
        public ActionResult SviGosti()
        {
            var danas = DateTime.Now;
            var sviGosti = (from r in db.Rezervacijas where (r.Check_In <= danas && r.Check_Out >= danas) select r).ToList();
            return View(sviGosti);
        }

        public ActionResult NoviRacun()
        {
            return RedirectToAction("NoviRacun", "Racun");
        }


        public ActionResult SveRezervacije()
        {

            return RedirectToAction("SveRezervacije", "Rezervacija");
            
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
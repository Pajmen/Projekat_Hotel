using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projekat_Hotel.Models;

namespace Projekat_Hotel.Controllers
{
    public class HomeController : Controller
    {
        static Hotel_DREntities db = new Hotel_DREntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SlobodneSobe()
        {

            return View();
        }

        public ActionResult SveUsluge()
        {

            return View();
        }
    }
}
using Projekat_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekat_Hotel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Uloguj(Radnik userModel)
        {
            try
            {

                Hotel_DREntities db = new Hotel_DREntities();
                var podaciOUseru = (from r in db.Radniks where userModel.KorisnickoIme == r.KorisnickoIme && userModel.Lozinka == r.Lozinka select r).Single();

                if (podaciOUseru != null)
                {
                    int privilege = (int)podaciOUseru.UlogaID;
                    Session["UserId"] = podaciOUseru.JMBG;
                    Session["Ime"] = podaciOUseru.Ime;
                    Session["Privilegije"] = podaciOUseru.UlogaID;
                    FormsAuthentication.SetAuthCookie(podaciOUseru.Uloga.NazivUloge, false);
                    switch (privilege)
                    {
                        case 1: return RedirectToAction("Index", "Radnik"); break;
                        case 2: return RedirectToAction("Index", "Manager"); break;
                        default: return RedirectToAction("Index", "Recepcija"); break;

                    }
                }

                else
                {
                    userModel.LogingErrorMessage = "Pogrešno korisničko ime ili lozinka";
                    return View("Index", userModel); // na stranicu za logovanje saljemo porukuo greski i postavljamo pidatke na prazno
                }
            } //try

            catch
            {
                userModel.LogingErrorMessage = "Pogrešno korisničko ime ili lozinka";
                return View("Index", userModel); // na stranicu za logovanje saljem o porukuo greski i postavljamo pidatke na prazno

            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat_Hotel.Models
{
    public class Ponuda
    {
        public static List<Soba> SveSobe()
        {
            Hotel_DREntities dbEntitet = new Hotel_DREntities();
            List<Soba> slobodneSobe = (from s in dbEntitet.Sobas where s.StatusID == 1001 || s.StatusID == 1004 select s).ToList();
            return slobodneSobe;
        }

        public static List<Usluga> SveUsluge()
        {
            Hotel_DREntities dbEntitet = new Hotel_DREntities();
            List<Usluga> usluge = (from u in dbEntitet.Uslugas select u).ToList();
            return usluge;
        }

        public static List<Rezervacija> SveRezervacije()
        {
            Hotel_DREntities dbEntitet = new Hotel_DREntities();
            List<Rezervacija> rezervacije = (from r in dbEntitet.Rezervacijas select r).ToList();
            return rezervacije;
        }

        public static List<Racun> SviRacuni()
        {
            Hotel_DREntities dbEntitet = new Hotel_DREntities();
            List<Racun> racuni = (from r in dbEntitet.Racuns select r).ToList();
            return racuni;
        }

        public static double IznosRacuna(Rezervacija rezervacija)
        {
            Hotel_DREntities db = new Hotel_DREntities();
            Soba rezervisanaSoba = (from s in db.Sobas where s.SobaID == rezervacija.SobaID select s).Single();
            Usluga koriscenaUsluga = (from u in db.Uslugas where u.UslugaID == rezervacija.UslugaID select u).Single();
            double tempCenaSobe = rezervisanaSoba.Cena;
            double tempCenaUsluge = koriscenaUsluga.Cena;
            double timeElapsed = Math.Round((rezervacija.Check_Out - rezervacija.Check_In).TotalDays + 1, 0);
            double iznos = tempCenaSobe * tempCenaUsluge * timeElapsed;
            return iznos;
        }
    }
}
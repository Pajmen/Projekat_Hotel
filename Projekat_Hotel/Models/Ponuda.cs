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
    }
}
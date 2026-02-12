using System;
using System.Collections.Generic;
using System.Linq;

namespace gyumolcs_cons
{
    public class Gyumolcs
    {
        public int Gyumolcsid { get; set; }
        public string Nev { get; set; }
        public string Megjegyzes { get; set; }
        public string Nev_eng { get; set; }
        public string Alt_szoveg { get; set; }
        public string Src { get; set; }
        public List<Erkezes> Erkezesek { get; set; }

        public Gyumolcs(int gyumolcsid, string nev, string megjegyzes, string nev_eng, string alt_szoveg, string src, List<Erkezes> erkezesek)
        {
            Gyumolcsid = gyumolcsid;
            Nev = nev;
            Megjegyzes = megjegyzes;
            Nev_eng = nev_eng;
            Alt_szoveg = alt_szoveg;
            Src = src;
            Erkezesek = erkezesek;
        }

        public int ossz_mennyiseg => Erkezesek.Sum(e => e.Mennyiseg);
        public double ossz_ertek => Erkezesek.Sum(e => e.OsszErtek);
        public double legdragabb => Erkezesek.Max(e => e.Egysegar);
        public double alma_ertek => Erkezesek.Where(e => e.Gyumolcsid == 1).Sum(e => e.OsszErtek);
        public int februar_szallmanyok => Erkezesek.Count(e => e.ErkezesDatum.Month == 2 && e.ErkezesDatum.Year == 2026);
    }
}

using System;

namespace gyumolcs_cons
{
    public class Erkezes
    {
        public int Gyumolcsid { get; set; }
        public int Mennyiseg { get; set; }
        public double Egysegar { get; set; }
        public DateTime ErkezesDatum { get; set; }

        public Erkezes(int gyumolcsid, int mennyiseg, double egysegar, DateTime erkezesDatum)
        {
            Gyumolcsid = gyumolcsid;
            Mennyiseg = mennyiseg;
            Egysegar = egysegar;
            ErkezesDatum = erkezesDatum;
        }

        public double OsszErtek => Mennyiseg * Egysegar;
    }
}

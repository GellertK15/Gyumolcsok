using System;
using System.Collections.Generic;
using System.Linq;
using gyumolcs_cons;
using System.IO;


namespace gyumolcs_cons
{
    internal class Program
    {
        static string gyumolcsokFilePath = "gyumolcsok.csv";
        static string erkezesekFilePath = "erkezesek.csv";

        static List<Gyumolcs> gyumolcsok;

        static void Main(string[] args)
        {
            gyumolcsok = CsvReader.BetoltGyumolcsok(gyumolcsokFilePath);

            foreach (Erkezes erkezes in CsvReader.BetoltErkezesek(erkezesekFilePath))
            {
                gyumolcsok.Where(g => g.Gyumolcsid == erkezes.Gyumolcsid).FirstOrDefault()?.Erkezesek.Add(erkezes);
            }

            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();

            Console.ReadKey();
        }

        private static void Feladat1()
        {
            Console.WriteLine("\n1. feladat");
            Console.WriteLine("\tAz összes gyümölcs mennyisége: " + gyumolcsok.Sum(g => g.ossz_mennyiseg) + " kg");
        }

        private static void Feladat2()
        {
            Console.WriteLine("\n2. feladat");
            double osszErtek = gyumolcsok.Sum(g => g.ossz_ertek);
            Console.WriteLine("\tAz összes gyümölcs értéke: " + osszErtek + " Ft");
        }

        private static void Feladat3()
        {
            Console.WriteLine("\n3. feladat");
            Gyumolcs legdragabbGyumolcs = gyumolcsok.OrderByDescending(g => g.legdragabb).FirstOrDefault();
            if (legdragabbGyumolcs != null)
                Console.WriteLine("\tA legdrágább gyümölcs: " + legdragabbGyumolcs.Nev + ", egységára: " + legdragabbGyumolcs.legdragabb + " Ft");
        }

        private static void Feladat4()
        {
            Console.WriteLine("\n4. feladat");
            Gyumolcs alma = gyumolcsok.FirstOrDefault(g => g.Gyumolcsid == 1);
            if (alma != null)
                Console.WriteLine("\tAz Alma összértéke: " + alma.alma_ertek + " Ft");
        }

        private static void Feladat5()
        {
            Console.WriteLine("\n5. feladat");
            Gyumolcs legtobbszor = gyumolcsok.OrderByDescending(g => g.Erkezesek.Count).FirstOrDefault();
            if (legtobbszor != null)
                Console.WriteLine("\tA legtöbbször érkező gyümölcs: " + legtobbszor.Nev + ", érkezések száma: " + legtobbszor.Erkezesek.Count);
        }

        private static void Feladat6()
        {
            Console.WriteLine("\n6. feladat");
            int februarSzallmanyok = gyumolcsok.Sum(g => g.februar_szallmanyok);
            Console.WriteLine("\t2026 februárjában érkezett szállítmányok száma: " + februarSzallmanyok);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace gyumolcs_cons
{
    public class CsvReader
    {
        public static List<Gyumolcs> BetoltGyumolcsok(string filePath)
        {
            List<Gyumolcs> gyumolcsok = new List<Gyumolcs>();
            try
            {
                using (var sr = new StreamReader(filePath, new UTF8Encoding(true))) 
                {
                    sr.ReadLine(); // Fejléc átugrása
                    while (!sr.EndOfStream)
                    {
                        string sor = sr.ReadLine();
                        string[] adatok = sor.Replace("\"", "").Split(';');

                        gyumolcsok.Add(new Gyumolcs(
                        int.Parse(adatok[0]),
                        adatok[1],
                        adatok[2],
                        adatok[3],
                        adatok[4],
                        adatok[5],
                        new List<Erkezes>() ));

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a gyumolcsok fájl olvasása közben: " + ex.Message);
            }

            return gyumolcsok;
        }

        public static List<Erkezes> BetoltErkezesek(string filePath)
        {
            List<Erkezes> erkezesek = new List<Erkezes>();
            try
            {
                using (var sr = new StreamReader(filePath, Encoding.UTF8)) 
                {
                    sr.ReadLine(); // Fejléc átugrása
                    while (!sr.EndOfStream)
                    {
                        string sor = sr.ReadLine();
                        string[] adatok = sor.Replace("\"", "").Split(';');

                        erkezesek.Add(new Erkezes(
                        int.Parse(adatok[0]),                            
                        int.Parse(adatok[1]),                           
                        double.Parse(adatok[2], CultureInfo.InvariantCulture), 
                        DateTime.Parse(adatok[3])));

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba az erkezesek fájl olvasása közben: " + ex.Message);
            }

            return erkezesek;
        }
    }
}

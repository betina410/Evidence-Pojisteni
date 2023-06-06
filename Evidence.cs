using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Evidence_Pojisteni
{
    internal class Evidence
    {
        //Inicializace list objektů Pojistenec
        private readonly List<Pojistenec> pojistenci = new();
        //Bool pro cyklus celého programu. V případě false se program ukončí
        public bool pokracovat = true;

        //Metoda pro vytvoření nového objektu Pojistenec
        public void VytvoritPojistence()
        {
            Console.WriteLine("\nZadejte jméno nového pojištěného:");
            string jmeno = NactiJmeno();
            string prijmeni = NactiPrijmeni();
            int vek = NactiVek();
            string telefonniCislo = NactiTelefonniCislo();
            //Konstruktor s validovanými parametry
            Pojistenec pojistenec = new (
                jmeno,
                prijmeni,
                vek,
                telefonniCislo
            );
            //Vložení objektu Pojistenec do kolekce listu
            pojistenci.Add(pojistenec);
            Console.WriteLine("\nData uložena.");
        }

        //Metoda pro vypsání všech pojištěných
        public void VypisPojistence()
        {
            //V případě, že kolekce list není prázdný
            if (pojistenci.Count > 0)
            {
                //Vypsání "hlavičky" seznamu
                Console.WriteLine(ToString());
                //LINQ dotaz - seřazení podle abecedy podle jména a příjmení
                var vypisPojistenych = from u in pojistenci
                                         orderby u.Jmeno, u.Prijmeni
                                         select u;
                //Výpis jednotlivých pojištěnců
                foreach (Pojistenec p in vypisPojistenych)
                {
                    Console.WriteLine(p);
                }
            }
            else
            {
                Console.WriteLine("\nNemáme žádné pojištěné.");
            }
        }

        //Metoda pro hledání konkrétního pojištěného
        public void HledaniPojistence()
        {
            Console.WriteLine("\nZadejte jméno pojištěného:");
            string jmenoValidovano = NactiJmeno();
            string prijmeniValidovano = NactiPrijmeni();

            //LINQ dotaz - nalezení pojištěných podle zadaného jména, příjmení, seřazení podle abecedy
            var hledaniPojistenych = from u in pojistenci
                                     where (u.Jmeno == jmenoValidovano && u.Prijmeni == prijmeniValidovano)
                                     orderby u.Jmeno, u.Prijmeni
                                     select u;

            //V případě, že kolekce z LINQ dotazu není prázdná
            if (hledaniPojistenych.Any())
            {
                //Vypsání "hlavičky" seznamu
                Console.WriteLine(ToString());
                //Výpis nalezených pojištěnců
                foreach (Pojistenec p in hledaniPojistenych)
                    Console.WriteLine(p);
            }
            else
            {
                Console.WriteLine("\nHledání neodpovídá žádný záznam.");
            }
        }

        //Metoda pro hledání konkrétního pojištěného
        public void VymazaniPojistence()
        {
            Console.WriteLine("\nZadejte jméno pojištěného k vymazání:");
            string jmenoValidovano = NactiJmeno();
            string prijmeniValidovano = NactiPrijmeni();
            int vekValidovano = NactiVek();
            string telefonniCisloValidovano = NactiTelefonniCislo();

            //LINQ dotaz - nalezení pojištěných podle zadaného jména, příjmení, věku i telefonního čísla. Musí se shodovat vše.
            var pojistenecKVymazani = pojistenci.SingleOrDefault(
                s => s.Jmeno == jmenoValidovano
                && s.Prijmeni == prijmeniValidovano
                && s.Vek == vekValidovano
                && s.TelefonniCislo == telefonniCisloValidovano
            );

            //V případě, že kolekce z LINQ dotazu není prázdná. 
            if (pojistenecKVymazani != null)
            {
                Console.WriteLine("\nZáznam tohoto pojištence bude vymazán.");
                Console.WriteLine(ToString());  //Vypsání "hlavičky" seznamu
                Console.WriteLine(pojistenecKVymazani); //Výpis nalezeného pojištěnce
                Console.WriteLine("\nJste si opravdu jisti?");
                Console.WriteLine("\nZadejte:");
                Console.WriteLine("\"ANO\" - pro vymazání");
                Console.WriteLine("\"NE\" - pro ponechání a vrácení se do hlavní nabídky");
                //Uživatelský vstup převeden na velká písmena a odstraněny bílé znaky
                string vstup = Console.ReadLine().ToUpper().Trim();
                //V případě, že uživatel zadá "ano", dojde k vymazání. Po jakémkoliv jiném vstupu budo data ponechána
                if (vstup == "ANO")
                {
                    pojistenci.Remove(pojistenecKVymazani);
                    Console.WriteLine("\nData vymazána.");
                }
                else
                {
                    Console.WriteLine("\nData ponechána.");
                }
            }
            else
            {
                Console.WriteLine("\nHledání nedpovídá žádný záznam.");
            }
        }

        //Metoda pro ukončení aplikace - volba: 5, bool pokracovat = false
        public void UkonceniAplikace()
        {
            pokracovat = false;
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("Děkujeme za použití našeho programu.");
            Console.WriteLine("\nStiskem libovolné klávesy program ukončíte.");
            Console.ReadKey();
        }

        //Metoda pro pokračování do "hlavního menu"
        public static void Pokracovat()
        {
            Console.WriteLine("\nPokračujte stiskem libovolné klávesy.");
            Console.ReadKey();
            Console.Clear();
        }

        //Metoda pro načtení a validaci jmena
        public static string NactiJmeno()
        {
            //Výraz pro jméno - nesmí obsahovat číslice a speciální znaky
            Regex rx = new(@"^\b[^\d\W]+\b$");
            bool spravnyVstup = false;
            string jmeno = "";
            //V případě, že vstup odpovídá regulárnímu výrazu, cyklus skončí
            while (!spravnyVstup)
            {
                //Uživatelský vstup převeden na malá písmena a odstraněny bílé znaky
                jmeno = Console.ReadLine().ToLower().Trim();
                if (rx.IsMatch(jmeno))
                {
                    spravnyVstup = true;
                }
                else
                {
                    Console.WriteLine("\nJméno nesmí obsahovat číslice a speciální znaky.\nZadejte jméno:");
                }
            }
            return jmeno;
        }

        //Metoda pro načtení a validaci příjmení
        public static string NactiPrijmeni()
        {
            Console.WriteLine("Zadejte příjmeni:");
            //Regulární výraz pro příjmení - nesmí obsahovat číslice a speciální znaky
            Regex rx = new(@"^\b[^\d\W]+\b$");
            bool spravnyVstup = false;
            string prijmeni = "";
            //V případě, že vstup odpovídá regulárnímu výrazu, cyklus skončí
            while (!spravnyVstup)
            {
                //Uživatelský vstup převeden na malá písmena a odstraněny bílé znaky
                prijmeni = Console.ReadLine().ToLower().Trim();
                if (rx.IsMatch(prijmeni))
                {
                    spravnyVstup = true;
                }
                else
                {
                    Console.WriteLine("\nPříjmení nesmí obsahovat číslice a speciální znaky.\nZadejte Příjmení:");
                }
            }
            return prijmeni;
        }

        //Metoda pro načtení a validaci věku
        public static int NactiVek()
        {
            Console.WriteLine("Zadejte věk:");
            int vek ;
            int vstup;
            //Cyklus se opakuje dokud není zadáno číslo od 0 do 99
            while (!(int.TryParse(Console.ReadLine().Trim(), out vstup) && vstup >= 0 && vstup <= 99))
            {
                Console.WriteLine("\nNeplatné číslo. Zadejte věk v rozmezí 0-99.");
            }
            vek = vstup;
            return vek;
        }

        //Metoda pro načtení a validaci telefonního čísla
        public static string NactiTelefonniCislo()
        {
            //Regulární výraz pro telefonní číslo - smí obsahovat přesně 9 číslic 0-9
            Regex rx = new(@"^[0-9]{9}$");
            bool spravnyVstup = false;
            string telefonniCislo = "";
            //V případě, že vstup odpovídá regulárnímu výrazu, cyklus skončí
            while (!spravnyVstup)
            {
                Console.WriteLine("Zadejte telefonní číslo ve formátu 123456789:");
                //Uživatelský vstup převeden na malá písmena a odstraněny bílé znaky
                telefonniCislo = Console.ReadLine().ToLower().Trim();
                if (rx.IsMatch(telefonniCislo))
                {
                    spravnyVstup = true;
                }
                else
                {
                    Console.WriteLine("\nNeplatný formát telefonního čísla.");
                }
            }
            return telefonniCislo;
        }

        //Vrací "hlavičku" seznamu pro výpis
        public override string ToString()
        {
            return String.Format("\n{0,-10}\t{1,-10}\t{2,10}\t{3,-10}", "Jméno", "Příjmení", "Věk", "Telefonní číslo");
        }
    }
}


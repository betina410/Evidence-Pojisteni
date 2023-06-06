using Evidence_Pojisteni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Inicializace objektu Aplikace
Evidence aplikace = new();


//Cyklus celého programu. Opakuje se, dokud uživatel neukončí program - tedy: aplikace.pokracovat = false
while (aplikace.pokracovat)
{
    Console.WriteLine("--------------------");
    Console.WriteLine("Evidence pojištěných");
    Console.WriteLine("--------------------");
    Console.WriteLine("\nVyberte si akci");
    Console.WriteLine("1 - Přidat nového pojištěného");
    Console.WriteLine("2 - Vypsat všechny pojištěné");
    Console.WriteLine("3 - Vyhledat pojištěného");
    Console.WriteLine("4 - Vymazat pojištěného");
    Console.WriteLine("5 - Ukončit program");

    //Uživatelský vstup pro metodu switch
    int volba;

    //Cyklus se opakuje dokud není zadáno číslo od 1 do 5
    while (!(int.TryParse(Console.ReadLine().Trim(), out volba) && volba >= 1 && volba <= 5))
    {
        Console.WriteLine("\nNeplatná volba. Zadejte číslo v rozmezí 1-5.");
    }
    //Metoda switch na základě uživatelského vstupu 'volba'
    switch (volba)
    {
        case 1: //Vytvoření nového pojištěného
            aplikace.VytvoritPojistence();
            Evidence.Pokracovat();
            break;
        case 2: //Vypsání všech pojištěných
            aplikace.VypisPojistence();
            Evidence.Pokracovat();
            break;
        case 3: //Hledání pojištěného
            aplikace.HledaniPojistence();
            Evidence.Pokracovat();
            break;
        case 4: //Vymazání pojištěného
            aplikace.VymazaniPojistence();
            Evidence.Pokracovat();
            break;
        case 5: //Ukončení celého programu
            aplikace.UkonceniAplikace();
            break;
        default:
            Console.WriteLine("Nastala chyba. Zavřete aplikaci");
            break;
    }

}

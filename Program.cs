using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Net;

var radniciDictionary = new Dictionary<string, DateTime>()
{
    {"Mate Matic", new DateTime(2000, 1, 1) },
    {"Ana Anic", new DateTime(1995, 5, 10)},
    {"Iva Ivanic", new DateTime(1988, 11, 5)},
    {"Eva Evic", new DateTime(1950, 11, 20)}
};

var proizvodi = new Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)>()
{
     { "Kukuruz", (10, 4.8f, new DateTime(2023, 11, 9)) },    
     { "Pšenica", (15, 5.2f, new DateTime(2023, 11, 17)) },  
     { "Soja", (8, 4.5f, new DateTime(2023, 12, 6)) },       
     { "Jabuka", (20, 3.2f, new DateTime(2023, 11, 16)) },    
     { "Krumpir", (12, 2.9f, new DateTime(2023, 12, 4)) }

};
var racuni = new Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)>()
{


};
racuni.Add(1, (new DateTime(2023, 11, 18), 0, new List<(string ImeProizvoda, int Kolicina, float Cijena)>
        {
            ("Kukuruz", 2, 4.8f),
            ("Jabuka", 5, 3.2f),
            ("Pšenica", 3, 5.2f)
        }));

racuni.Add(2, (new DateTime(2023, 11, 19), 0, new List<(string ImeProizvoda, int Kolicina, float Cijena)>
        {
            ("Soja", 4, 4.5f),
            ("Krumpir", 8, 2.9f)
        }));

racuni.Add(3, (new DateTime(2023, 11, 20), 0, new List<(string ImeProizvoda, int Kolicina, float Cijena)>
        {
            ("Jabuka", 10, 3.2f),
            ("Pšenica", 5, 5.2f),
            ("Krumpir", 4, 2.9f)
        }));

foreach (var racun in racuni)
{
    float ukupnaCijena = racun.Value.UkupnaCijena;

    if (ukupnaCijena == 0)
    {
        foreach (var proizvod in racun.Value.proizvod)
        {
            ukupnaCijena += proizvod.Kolicina * proizvod.Cijena;
        }
    }

    racuni[racun.Key] = (racun.Value.VrijemeIzdavanja, ukupnaCijena, racun.Value.proizvod);
}

Izbornik(racuni, proizvodi, radniciDictionary);





static void Izbornik(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{

    while (true)
    {
        Console.WriteLine("1 - Artikili");
        Console.WriteLine("2 - Radnici");
        Console.WriteLine("3 - Racuni");
        Console.WriteLine("4 - Statistika");
        Console.WriteLine("0 - Izlaz iz aplikacije");
        int userInput;
        do
        {


            if (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 0)
            {
                Console.WriteLine("Neispravan unos");
            }

        } while (userInput < 0);
        switch (userInput)
        {
            case 1:
                Artikli(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 2:
                Radnici(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 3:
                Racuni(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 4:
               Statistika(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 0:
                return;

        }
    }
}
static void Artikli(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("1 - Unos artikla");
        Console.WriteLine("2 - Brisanje artikla");
        Console.WriteLine("3 - Uređivanje artikla");
        Console.WriteLine("4 - Ispis");
        Console.WriteLine("0 - Povratak na glavni izbornik");

        int izbor;
        do
        {
            Console.Write("Unesite svoj odabir: ");
        } while (!int.TryParse(Console.ReadLine(), out izbor));

        switch (izbor)
        {
            case 1:
                UnosArtikla(proizvodi);
                break;
            case 2:
                BrisanjeArtikla(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 3:
                UrediArtikal(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 4:
                IspisArtikla(racunDictionary, proizvodi, radniciDictionary);
                break;
            case 0:
                Console.Clear();
                Izbornik(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("Krivi unos");
                break;
        }
    }

}
static void UnosArtikla (Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Console.WriteLine("Unesite ime artikla");
    string ime = GetStringFromUser();
    Console.WriteLine("Unesite kolicinu artikla");
    int kolicina = GetInt();
    Console.WriteLine("Unesite cijenu artikla");
    float cijena = GetFloat();
    Console.WriteLine("Unesite rok trajanja artikla");
    DateTime rok = GetDateFromUser();

    if (AskUserToMakeChange())
    {
        proizvodi.Add(ime, (kolicina, cijena, rok));
        Console.WriteLine("Unos artikla je uspjesno obavljen");
    }
    else Console.WriteLine("Artikal nije unesen");

    return;
}
static void BrisanjeArtikla(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("a - brisanje po imenu");
        Console.WriteLine("b - brisanje artikala kojima je istekao rok trajanja");
        Console.WriteLine("0 - povratak na prosli izbornik");
        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                ArtikliBrisanjePoImenu(proizvodi);
                break;
            case "b":
                ArtikliBrisanjeRok(proizvodi);
                break;
            case "0":
                Console.Clear();
                Artikli(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("krivo znak ste unijeli");
                break;

        }
    }

}
static void ArtikliBrisanjePoImenu(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Console.WriteLine("Unesite ime artikla kojeg zelite obrisati");
    string imeArtikla = GetStringFromUser();

    foreach (var item in proizvodi)
    {
        if (item.Key == imeArtikla)
        {
            if (AskUserToMakeChange())
            {
                proizvodi.Remove(item.Key);
                Console.WriteLine($"Artikal {item.Key} je izbrisan");
                return;
            }
            else { Console.WriteLine("Artikal se nece izbrisati");
                return;
            }
        }
    }
    Console.WriteLine($"Ne postoji artikal s imenom {imeArtikla}");
}
static void ArtikliBrisanjeRok(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    if (AskUserToMakeChange())
    {
        var danas = DateTime.Now;
        List<string> itemsToRemove = new List<string>();
        foreach (var item in proizvodi)
        {
            
            if (item.Value.Rok < danas)
            {
                itemsToRemove.Add(item.Key);                
               
            }
        }
        foreach (var key in itemsToRemove)
        {
            proizvodi.Remove(key);
            Console.WriteLine($"Artiklu {key} je istekao rok pa je izbrisan");
        }
    }
    else Console.WriteLine("Artikli kojima je istekao rok se nece izbrisati");

}
static void UrediArtikal(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("a - Uređenje zasebno proizvoda");
        Console.WriteLine("b - popusti/poskupljenje na sve proizvode");
        Console.WriteLine("0 - za povratak na prethodni izbornik");
        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                UrediZasebnoArtikal(racunDictionary, proizvodi, radniciDictionary);
                break;
            case "b":
                PromjenaCijene(racunDictionary, proizvodi, radniciDictionary);
                break;
            case "0":
                Console.Clear();
                Artikli(racunDictionary, proizvodi, radniciDictionary);
                return;
            default:
                Console.WriteLine("krivo znak ste unijeli");
                break;

        }
    }
}
static void UrediZasebnoArtikal(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    Console.WriteLine("Unesite ime artikla kojeg zeltie urediti");
    string imeArtikla = GetStringFromUser();
    bool found = false;
    foreach(var item in proizvodi)
    {
        if (imeArtikla == item.Key) 
            found = true;
    }
    if (found) { while (true)
        {
            Console.WriteLine("Sto zelite promijeniti");
            Console.WriteLine("a - ime artikla");
            Console.WriteLine("b - kolicina artikla");
            Console.WriteLine("c - cijena artikla");
            Console.WriteLine("d - rok artikla");
            Console.WriteLine("0 - povratak na prosli izbornik");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "a":
                    UrediImeArtikla(proizvodi, imeArtikla);
                    break;
                case "b":
                   UrediKolicinuArtikla(proizvodi, imeArtikla);
                    break;
                case "c":
                   UrediCijenuArtikla(proizvodi, imeArtikla);
                    break;
                case "d":
                    UrediRokTrajanjaArtikla(proizvodi, imeArtikla);
                    break;
                case "0":
                    Console.Clear();
                    UrediArtikal(racunDictionary, proizvodi, radniciDictionary);
                    break;
            }
        } }

    

}
static void UrediImeArtikla(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, string imeArtikla)
{
    Console.WriteLine("Upisite novo ime artikla");
    string novoImeArtikla = GetStringFromUser();
    
        
        if (AskUserToMakeChange())
        {

            proizvodi.Add(novoImeArtikla, (proizvodi[imeArtikla].Kolicina, proizvodi[imeArtikla].Cijena, proizvodi[imeArtikla].Rok));
            proizvodi.Remove(imeArtikla);
            Console.WriteLine("Promjena je uspjesna");
        }
        else Console.WriteLine("Promjena se nece izvrsiti");
    

}
static void UrediKolicinuArtikla(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, string imeArtikla)
{
    Console.WriteLine("Upisite novu kolicinu");
    int novaKolicina = GetInt();
        
        if (AskUserToMakeChange())
        {
            var updatedValue = (novaKolicina, proizvodi[imeArtikla].Cijena, proizvodi[imeArtikla].Rok);
            proizvodi[imeArtikla] = updatedValue;
            Console.WriteLine("Promjena je uspjesna");
        }
        else Console.WriteLine("Promjena se nece izvrsiti");
    

}
static void UrediCijenuArtikla(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, string imeArtikla)
{
    Console.WriteLine("Upisite novu cijenu");

    float novaCijena = GetFloat();
        
        if (AskUserToMakeChange())
        {
            var updatedValue = (proizvodi[imeArtikla].Kolicina, novaCijena, proizvodi[imeArtikla].Rok);
            proizvodi[imeArtikla] = updatedValue;
            Console.WriteLine("Promjena je uspjesna");
        }
        else Console.WriteLine("Promjena se nece izvrsiti");
    

}
static void UrediRokTrajanjaArtikla(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, string imeArtikla)
{
    Console.WriteLine("Upisite novi rok trajanja");
    DateTime noviDatum = GetDateFromUser();
        
        if (AskUserToMakeChange())
        {
            var updatedValue = (proizvodi[imeArtikla].Kolicina, proizvodi[imeArtikla].Cijena, noviDatum);
            proizvodi[imeArtikla] = updatedValue;
            Console.WriteLine("Promjena je uspjesna");
        }
        else Console.WriteLine("Promjena se nece izvrsiti");
    

}
static void PromjenaCijene(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("a - poskupljenje svih proizvoda");
        Console.WriteLine("b - popusti svih proizvoda");
        Console.WriteLine("0 - povratak na prethodni izbornik");
        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                Poskupjenje(proizvodi);
                break;
            case "b":
                Popusti(proizvodi);
                break;
            case "0":
                Console.Clear();
                UrediArtikal(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("krivo znak ste unijeli");
                break;

        }
    }
}
static void Poskupjenje(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Console.WriteLine("Za koliko eura zelite da poskupe svi proizvodi");
    float cijena = GetFloat();
    if (AskUserToMakeChange())
    {

        foreach (var key in proizvodi.Keys.ToList())
        {
            proizvodi[key] = (proizvodi[key].Kolicina, proizvodi[key].Cijena + cijena, proizvodi[key].Rok);
        }
    }
    else Console.WriteLine("Poskupljenej se nece dogoditi");


}
static void Popusti(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Console.WriteLine("Za koliko eura zelite da pojeftine svi proizvodi");
    float cijena = GetFloat();
    if (AskUserToMakeChange())
    {

        foreach (var key in proizvodi.Keys.ToList())
        {
            proizvodi[key] = (proizvodi[key].Kolicina, proizvodi[key].Cijena - cijena, proizvodi[key].Rok);
        }
    }
    else Console.WriteLine("Pojeftinjenje se nece dogoditi");

    
}
static void IspisArtikla(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    

    while (true)
    {
        Console.WriteLine("a - Svih artikala kako su spremljeni");
        Console.WriteLine("b - Svih artikala sortirano po imenu");
        Console.WriteLine("c - Svih artikala sortirano po datumu silazno");
        Console.WriteLine("d - Svih artikala sortirano po datumu uzlazno");
        Console.WriteLine("e - Svih artikala sortirano po količini");
        Console.WriteLine("f - Najprodavaniji artikl");
        Console.WriteLine("g - Najmanje prodavan artikl");
        Console.WriteLine("0 - Povratak na prosli izbornik");
        var izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                PrintArtikli(proizvodi);
                break;

            case "b":
                PrintArtikliPoImenu(proizvodi);
                break;

            case "c":
                PrintArtikliPoDatumuSilazno(proizvodi);
                break;

            case "d":
                PrintArtikliPoDatumuUzlazno(proizvodi);
                break;

            case "e":
                PrintArtikliPoKolicini(proizvodi);
                break;

            case "f":
                PrintNajprodavanijiArtikl(racunDictionary, proizvodi);
                break;

            case "g":
                PrintNajmanjeProdavanArtikl(racunDictionary, proizvodi);
                break;

            case "0":
                Console.Clear();
                Artikli(racunDictionary, proizvodi, radniciDictionary);
                break;
                
            default:
                Console.WriteLine("Nepoznata opcija. Pokušajte ponovno.");
                break;
        }
    }
}
static void PrintArtikli (Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
    {
        
        foreach(var item in proizvodi)
        {
            var rok = item.Value.Rok  - DateTime.Now;
            int brojDana = rok.Days;
            if(brojDana > 0)
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana do isteka  {brojDana}");
            else
                Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana od isteka  {Math.Abs(brojDana)}");


        }
    }
static void PrintArtikliPoImenu(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{

    var sortedByIme = proizvodi.OrderBy(x => x.Key);
    foreach (var item in sortedByIme)
    {
        var rok = item.Value.Rok - DateTime.Now;
        int brojDana = rok.Days;
        if (brojDana > 0)
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana do isteka  {brojDana}");
        else
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana od isteka  {Math.Abs(brojDana)}");
    }
}
static void PrintArtikliPoDatumuSilazno(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{

    var sortedByDatumSilazno = proizvodi.OrderByDescending(x => x.Value.Rok);
    foreach (var item in sortedByDatumSilazno)
    {
        var rok = item.Value.Rok - DateTime.Now;
        int brojDana = rok.Days;
        if (brojDana > 0)
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana do isteka  {brojDana}");
        else
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana od isteka  {Math.Abs(brojDana)}");
    }
}
static void PrintArtikliPoDatumuUzlazno(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    var sortedByDatumUzlazno = proizvodi.OrderBy(x => x.Value.Rok);
    foreach (var item in sortedByDatumUzlazno)
    {
        var rok = item.Value.Rok - DateTime.Now;
        int brojDana = rok.Days;
        if (brojDana > 0)
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana do isteka  {brojDana}");
        else
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana od isteka  {Math.Abs(brojDana)}");
    }
}
static void PrintArtikliPoKolicini(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    var sortedByKolicina = proizvodi.OrderByDescending(x => x.Value.Kolicina);
    foreach (var item in sortedByKolicina)
    {
        var rok = item.Value.Rok - DateTime.Now;
        int brojDana = rok.Days;
        if (brojDana > 0)
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana do isteka  {brojDana}");
        else
            Console.WriteLine($"{item.Key} ({item.Value.Kolicina}) - {item.Value.Cijena} -  broj dana od isteka  {Math.Abs(brojDana)}");
    }
}
static void PrintNajprodavanijiArtikl(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Dictionary<string, int> prodaja = new Dictionary<string, int>();
    foreach (var racun in racunDictionary.Values)
    {
        foreach(var item in racun.proizvod)
        {
            string imeProizvoda = item.ImeProizvoda;

        
            if (prodaja.ContainsKey(imeProizvoda))
            {
                prodaja[imeProizvoda] += item.Kolicina;
            }
            else
            {
                prodaja.Add(imeProizvoda, item.Kolicina);
            }
        }
    }
    string najprodavanijiArtikal = prodaja.OrderByDescending(kvp => kvp.Value).First().Key;
    Console.WriteLine("Najprodavaniji artikal je " + najprodavanijiArtikal);

}
static void PrintNajmanjeProdavanArtikl(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Dictionary<string, int> prodaja = new Dictionary<string, int>();
    foreach (var racun in racunDictionary.Values)
    {
        foreach(var item in racun.proizvod)
        {
            string imeProizvoda = item.ImeProizvoda;

            
            if (prodaja.ContainsKey(imeProizvoda))
            {
                prodaja[imeProizvoda] += item.Kolicina;
            }
            else
            {
                prodaja.Add(imeProizvoda, item.Kolicina);
            }
        }
    }
    string najprodavanijiArtikal = prodaja.OrderBy(kvp => kvp.Value).First().Key;
    Console.WriteLine("Najmanje prodavan artikal je " + najprodavanijiArtikal);

}
static void Radnici(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{

    while (true) {
        Console.WriteLine("1 - Unos radnika");
        Console.WriteLine("2 - Brisanje radnika");
        Console.WriteLine("3 - Uredjivanje radnika");
        Console.WriteLine("4 - Ispis");
        Console.WriteLine("5 - Povratak na glavni izbornik");
        int izbor = GetInt();
        
        switch (izbor)
    {      
        case 1:
            UnosRadnika(radniciDictionary);
            break;
        case 2:
            BrisanjeRadnika(racunDictionary, proizvodi, radniciDictionary);
            break;
        case 3:
            UrediRadnika(racunDictionary, proizvodi, radniciDictionary);
            break;
        case 4:
            Ispis(racunDictionary, proizvodi, radniciDictionary);
            break;
        case 5:
            Console.Clear();
            Izbornik(racunDictionary, proizvodi, radniciDictionary);
            break;
        default:
            Console.WriteLine("Krivi unos");
            break;
    }
}
}
static void UnosRadnika (Dictionary<string, DateTime> radniciDictionary){
   

    Console.WriteLine("Unesite ime i prezime radnika:");
    var ime = GetStringFromUser();

    var dob = GetDateFromUser();

    if (AskUserToMakeChange())
    {
        radniciDictionary[ime] = (dob);
        Console.WriteLine("Radnik uspješno dodan!");
    }
    else Console.WriteLine("Radnik nece biti dodan");  
    

}
static void BrisanjeRadnika(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("a - za brisanje po imenu");
        Console.WriteLine("b - za brisanje straijih od 65");
        Console.WriteLine("0 - povratak na prosli izbornik");
        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                PoImenu(radniciDictionary);
                break;
            case "b":
                StarijiOd65(radniciDictionary);
                break;
            case "0":
                Console.Clear();
                Radnici(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("krivo slovo ste unijeli");
                break;
        }
    }    
}
static void PoImenu(Dictionary<string, DateTime> radniciDictionary)
{
    Console.WriteLine("Unesite ime radnika kojeg zelite obrisati");
    string ime = GetStringFromUser();

    foreach (var item in radniciDictionary)
    {
        if(item.Key == ime)
        {
            if (AskUserToMakeChange())
            {
                radniciDictionary.Remove(item.Key);
                Console.WriteLine("osoba s imenom " + ime + " je izbrisana");
                return;
            }
            else
            {
                Console.WriteLine("Radnik se nece izbrisati");
                return;
            }
        }
    }
    Console.WriteLine($"Radnik s imenom {ime} ne postoji");
    

}
static void StarijiOd65(Dictionary<string, DateTime> radniciDictionary)
{
    if (AskUserToMakeChange())
    {
        var danas = DateTime.Now;
        List<string> keysToRemove = new List<string>();
        foreach (var item in radniciDictionary)
        {
            int godine = danas.Year - item.Value.Year;
            if (godine >= 65)
            {
                keysToRemove.Add(item.Key);                
                
            }
        }
        foreach (var key in keysToRemove)
        {            
            radniciDictionary.Remove(key);
            Console.WriteLine($"Osoba {key} je izbrisana jer ima više od 65 godina");
        }
    }
    else Console.WriteLine("Radnici stariji od 65 godina se nece izbrisati");
}
static void UrediRadnika(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("Sto zelite promijeniti?");
        Console.WriteLine("a - za ime");
        Console.WriteLine("b - za datum rodjneja");
        Console.WriteLine("0 - Povratak na prosli izbornik");
        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                UrediImeRadnika(radniciDictionary);
                break;
            case "b":
                UrediDateRadnika(radniciDictionary);
                break;
            case "0":
                Console.Clear();
                Radnici(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("krivo znak ste unijeli");
                break;

        }
       
    }
}
static void UrediImeRadnika(Dictionary<string, DateTime> radniciDictionary)
    {
    Console.WriteLine("Unesite ime radnika");
    string ime = GetStringFromUser();
    bool found = false;

    Console.WriteLine("Upisite novo ime");
    string novoIme = GetStringFromUser();
    foreach (var item in radniciDictionary.ToList())
    {
        if (item.Key == ime)
        {
            found = true;
            if (AskUserToMakeChange())
            {
                found = true;
                radniciDictionary.Add(novoIme, item.Value);
                radniciDictionary.Remove(item.Key);
                Console.WriteLine("Promjena je uspjesna");
            }
            else Console.WriteLine("Promjena se nece izvrsiti");
        }
    }
    if(!found)
        Console.WriteLine($"Radnik s imenom {ime} ne postoji");
}
static void UrediDateRadnika(Dictionary<string, DateTime> radniciDictionary)
{
    Console.WriteLine("Unesite ime radnika");
    string ime = GetStringFromUser();
    bool found = false;    
    Console.WriteLine("Upisite novi datum");
    var dob = GetDateFromUser();
    foreach (var item in radniciDictionary.ToList())
    {
        if (item.Key == ime)
        {
            found = true;
            if (AskUserToMakeChange())
            {
                radniciDictionary[item.Key] = dob;
                Console.WriteLine("Promjena datuma je uspsjena");
            }
            else Console.WriteLine("promjena se nece izvrsiti"); return;

        }
    }
    if (!found)
        Console.WriteLine($"Radnik s imenom {ime} ne postoji");
}

static void Ispis(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
    while (true)
    {
        Console.WriteLine("a - ispis svih radnika ");
        Console.WriteLine("b - rodjendan u ovom mjesecu");
        Console.WriteLine("0 - povratak na prethodni izbornik");

        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                IspisSvihRadnika(radniciDictionary);
                break;
            case "b":
                RodjedanUOvomMjesecu(radniciDictionary);
                break;
            case "0":
                Console.Clear();
                Radnici(racunDictionary, proizvodi, radniciDictionary);                
                break;
            default:
                Console.WriteLine("Krivi unos!");
                break;
        }
    }
}
static void IspisSvihRadnika(Dictionary<string, DateTime> radniciDictionary)
{
    var danas = DateTime.Now;
    foreach (var radnik in radniciDictionary)
    {
        int godine = danas.Year - radnik.Value.Year;
        Console.WriteLine($"{radnik.Key} {godine}");
    }
}
static void RodjedanUOvomMjesecu(Dictionary<string, DateTime> radniciDictionary)
{
    var danasMjesec = DateTime.Now.Month;
    foreach (var radnik in radniciDictionary)
    {
        if(danasMjesec == radnik.Value.Month)
        {
            Console.WriteLine(radnik.Key);
        }
    }
}
static string GetStringFromUser()
{
    string userInput;

    do    {
        
        userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput) || IsNumeric(userInput))
        {
            Console.WriteLine("Neispravan unos stringa");
        }

    } while (string.IsNullOrWhiteSpace(userInput) || IsNumeric(userInput));

    return userInput;
}

static bool IsNumeric(string input)
{
    return double.TryParse(input, out _);
}

static DateTime GetDateFromUser()
{
    DateTime dob;

    while (true)
    {
        Console.WriteLine("Unesite datum (format: dd/MM/yyyy):");

        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
        {
            return dob;
        }
        else
        {
            Console.WriteLine("Neuspješan unos datuma. Molimo unesite ponovno unesite datum.");
        }
    }
}

static bool AskUserToMakeChange()
{
    while (true)
    {
        Console.WriteLine("Zelite li napraviti promjenu (y/n)");
        string userInput = Console.ReadLine();

        if (userInput.ToLower() == "y")
        {
            return true;
        }
        else if (userInput.ToLower() == "n")
        {
            return false;
        }
        else
        {
            Console.WriteLine("Krivi unos. Upisite y za da ili n za ne");
        }
    }
}

static int GetInt()
{
    int userInput;

    do
    {


        if (!int.TryParse(Console.ReadLine(), out userInput) || userInput <= 0)
        {
            Console.WriteLine("Neispravan unos");
        }

    } while (userInput <= 0);

    return userInput;

}
static float GetFloat()
{
    float userInput;

    do
    {


        if (!float.TryParse(Console.ReadLine(), out userInput) || userInput <= 0)
        {
            Console.WriteLine("Neispravan unos");
        }

    } while (userInput <= 0);

    return userInput;
}
static void Racuni(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
   
    while (true)
    {
        Console.WriteLine("1 - Unos novog računa");
        Console.WriteLine("2 - Ispis računa");
        Console.WriteLine("3 - Povratak na glavni izbornik");
        int choice = GetInt();
        switch (choice)
        {
            case 1:
                UnosNovogRacuna(proizvodi, racunDictionary);
                break;
            case 2:
                IspisRacunaDictionary(racunDictionary,proizvodi,radniciDictionary);
                break;
            case 3:
                Console.Clear();
                Izbornik(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("Krivi unos");
                break;
        }
    }
}
static void UnosNovogRacuna(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)>)> racuni)
{
    Console.WriteLine("Svi dostupni proizvodi u dućanu:");
    PrintArtikli(proizvodi);

    int noviRacunId = racuni.Keys.Any() ? racuni.Keys.Max() + 1 : 1; 

    DateTime vrijemeIzdavanja = DateTime.Now;
    float ukupnaCijena = 0;
    List<(string ImeProizvoda, int Kolicina, float Cijena)> stavkeRacuna = new List<(string ImeProizvoda, int Kolicina, float Cijena)>();

    string imeProizvoda = "";

    while (imeProizvoda != "kraj")
    {
        Console.WriteLine("Upišite ime proizvoda ili 'kraj' za završetak unosa:");
        imeProizvoda = GetStringFromUser();

        if (imeProizvoda != "kraj")
        {
            if (proizvodi.TryGetValue(imeProizvoda, out var proizvod))
            {
                PrintArtikli(proizvodi);

                Console.WriteLine("Upišite količinu:");
                int kolicinaProizvoda = GetInt();

                if (stavkeRacuna.Any(s => s.ImeProizvoda == imeProizvoda))
                {
                    Console.WriteLine("Proizvod već unesen. Unesite drugi proizvod.");
                }
                else
                {
                    float totalCijenaProizvoda = kolicinaProizvoda * proizvod.Cijena;

                    stavkeRacuna.Add((imeProizvoda, kolicinaProizvoda, totalCijenaProizvoda));

                    ukupnaCijena += totalCijenaProizvoda;

                    proizvodi[imeProizvoda] = (proizvod.Kolicina - kolicinaProizvoda, proizvod.Cijena, proizvod.Rok);
                }
            }
            else
            {
                Console.WriteLine("Proizvod nije pronađen. Unesite ispravno ime proizvoda.");
            }
        }
    }

    racuni.Add(noviRacunId, (vrijemeIzdavanja, ukupnaCijena, stavkeRacuna));


    Console.WriteLine($"Račun ID: {noviRacunId}, Vrijeme Izdavanja: {vrijemeIzdavanja}, Ukupna Cijena: {ukupnaCijena}");


    Console.WriteLine("Pritisnite 'c' za potvrdu računa ili 'p' za poništenje:");
    char action = Console.ReadKey().KeyChar;

    if (action == 'c')
    {
        proizvodi = proizvodi.Where(p => p.Value.Kolicina > 0).ToDictionary(p => p.Key, p => p.Value);

        Console.WriteLine("Račun je potvrđen. Artikli su ažurirani.");
    }
    else if (action == 'p')
    {
        foreach (var stavka in stavkeRacuna)
        {
            if (proizvodi.TryGetValue(stavka.ImeProizvoda, out var originalProizvod))
            {
                proizvodi[stavka.ImeProizvoda] = (originalProizvod.Kolicina + stavka.Kolicina, originalProizvod.Cijena, originalProizvod.Rok);
            }
            else
            {
                proizvodi.Add(stavka.ImeProizvoda, (stavka.Kolicina, 0, DateTime.Now)); // Add the product back to the available products
            }
        }

        Console.WriteLine("Račun je poništen. Artikli su vraćeni na stanje prije unosa računa.");
    }
    else
    {
        Console.WriteLine("Nepoznata akcija. Radnja nije izvršena.");
    }
    
}

static void IspisRacunaDictionary(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
   

    while (true)
    {
        Console.WriteLine("1 - Ispis svih računa");
        Console.WriteLine("2 - Odabir računa po ID-u");
        Console.WriteLine("3 - Povratak na prethodni izbornik");
        int choice = GetInt();
        switch (choice)
        {
            case 1:
                IspisSvihRacuna(racunDictionary);
                break;
            case 2:
                IspisRacunaPoIDu(racunDictionary);
                break;
            case 3:
                Console.Clear();
                Racuni(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("krivi odabir!");
                break;
        }
    }
}
static void IspisSvihRacuna(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary)
{
    foreach (var racun in racunDictionary)
    {
        Console.WriteLine($"{racun.Key} - {racun.Value.VrijemeIzdavanja} - {racun.Value.UkupnaCijena}");

    }

}
static void IspisRacunaPoIDu(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary)
{
    Console.WriteLine("Upisite id racuna");
    int trazeniID = GetInt();
    bool found = false;
    foreach(var racun in racunDictionary)
    {
        if(racun.Key == trazeniID)
        {
            found = true;
            Console.WriteLine($"{racun.Key} - {racun.Value.VrijemeIzdavanja} - {racun.Value.UkupnaCijena}");
            Console.WriteLine("Proizvodi na racunu");
            foreach (var proizvod in racun.Value.proizvod)
            {
                Console.WriteLine($"  Ime Proizvoda: {proizvod.ImeProizvoda}, Kolicina: {proizvod.Kolicina}, Cijena: {proizvod.Cijena}");
            }
            Console.WriteLine();
        }
    }
}
static void Statistika(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary, Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, Dictionary<string, DateTime> radniciDictionary)
{
   
    while (true)

    {
        Console.WriteLine("1 - Ukupan broj artikala u trgovini");
        Console.WriteLine("2 - Vrijednost artikala koji nisu još prodani");
        Console.WriteLine("3 - Vrijednost svih artikala koji su prodani");
        Console.WriteLine("4 - Stanje po mjesecima");
        Console.WriteLine("5 - Povratak na glavni izbornik");
        int choice = GetInt();
        switch (choice)
        {
            case 1:
                UkupanBrojArtikala(proizvodi);
                break;
            case 2:
                VrijednostArtikalaKojiNisuProdani(proizvodi);
                break;
            case 3:
                VrijednostArtikalaKojiSuProdani(racunDictionary);
                break;
            case 4:
                StanjePoMjesecima(racunDictionary);
                break;
            case 5:
                Console.Clear();
                Izbornik(racunDictionary, proizvodi, radniciDictionary);
                break;
            default:
                Console.WriteLine("Krivi unos");
                break;
        }
    }
}
static void UkupanBrojArtikala(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    int ukupno = 0;
    
    foreach(var proizvod in proizvodi)
    {
        ukupno += proizvod.Value.Kolicina;
    }
    Console.WriteLine("Ukupan broj artikala je " + ukupno);
}
static void VrijednostArtikalaKojiNisuProdani(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    float ukupno = 0;

    foreach (var proizvod in proizvodi)
    {
        ukupno += proizvod.Value.Cijena * proizvod.Value.Kolicina;
    }
    Console.WriteLine("Vrijednost artikala koji nisu prodani" + ukupno);

}
static void VrijednostArtikalaKojiSuProdani(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary)
{
    float ukupno = 0;
    foreach(var racun in racunDictionary)
    {
        ukupno += racun.Value.UkupnaCijena;

    }
    Console.WriteLine("Vrijednost artikala koji su prodani " + ukupno);
}
static void StanjePoMjesecima(Dictionary<int, (DateTime VrijemeIzdavanja, float UkupnaCijena, List<(string ImeProizvoda, int Kolicina, float Cijena)> proizvod)> racunDictionary)
{
    Console.WriteLine("Upisite godinu");
    int godina = GetInt();
    Console.WriteLine("Upisite mjesec");
    int mjesec = GetInt();
    Console.WriteLine("Unesite iznos place radnika");
    float place = GetFloat();
    Console.WriteLine("Unesite iznos najma");
    float najam = GetFloat();
    Console.WriteLine("Unesite ostale troskove");
    float ostaliTroskovi = GetFloat();

    float zaradaOdProdaje = 0;

    var filteredRacuni = racunDictionary
       .Where(pair => pair.Value.VrijemeIzdavanja.Year == godina && pair.Value.VrijemeIzdavanja.Month == mjesec);

    foreach (var racun in filteredRacuni)
    {
        zaradaOdProdaje += racun.Value.UkupnaCijena;
    }

    float ukupniTroskovi = place + najam + ostaliTroskovi;
    float profit = zaradaOdProdaje - ukupniTroskovi;

    if(profit >0)
    Console.WriteLine($"Profit za {mjesec}/{godina}: {profit}");
    else
        Console.WriteLine($"Gubitak za {mjesec}/{godina}: {profit}");


}


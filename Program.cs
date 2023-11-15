using System;
using System.Collections.Generic;
using System.Globalization;

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
     { "Soja", (8, 4.5f, new DateTime(2023, 11, 6)) },       
     { "Jabuke", (20, 3.2f, new DateTime(2023, 11, 16)) },    
     { "Krumpir", (12, 2.9f, new DateTime(2023, 11, 4)) }

};

Izbornik();


int izbor = int.Parse(Console.ReadLine());


switch (izbor)
{
    case 1:
        Artikli(proizvodi);
        break;
    case 2:
        Radnici(radniciDictionary);
        break;
       
}
static void Izbornik()
{
    Console.WriteLine("1 - Artikili");
    Console.WriteLine("2 - Radnici");
    Console.WriteLine("3 - Racuni");
    Console.WriteLine("4 - Statistika");
    Console.WriteLine("0 - Izlaz iz aplikacije");
}

static void Artikli(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    Console.WriteLine("1 - Unos artikla");
    Console.WriteLine("2 - Brisanje artikla");
    Console.WriteLine("3 - Uređivanje artikla");
    Console.WriteLine("4 - Ispis");

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
           BrisanjeArtikla(proizvodi);
            break;
        case 3:
           UrediArtikal(proizvodi);
            break;
        case 4:
            //IspisArtikla(proizvodi);
            break;
        case 0:
            break;
        default:
            Console.WriteLine("Krivi unos");
            break;
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
    
    Artikli(proizvodi);
}
static void BrisanjeArtikla(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
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
                Artikli(proizvodi);
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
static void UrediArtikal(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
{
    while (true)
    {
        Console.WriteLine("a - Uređenje zasebno proizvoda");
        Console.WriteLine("b - popusti/poskupljenje na sve proizvode");
        string izbor = Console.ReadLine();
        switch (izbor)
        {
            case "a":
                UrediZasebnoArtikal(proizvodi);
                break;
            case "b":
                PromjenaCijene(proizvodi);
                break;
            case "0":
                Artikli(proizvodi);
                break;
            default:
                Console.WriteLine("krivo znak ste unijeli");
                break;

        }
    }
}
static void UrediZasebnoArtikal(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
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
                    UrediArtikal(proizvodi);
                    break;
            }
        } }

    

}
static void UrediImeArtikla(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi, string imeArtikla)
{
   
    
        
        if (AskUserToMakeChange())
        {

            proizvodi.Add(imeArtikla, (proizvodi[imeArtikla].Kolicina, proizvodi[imeArtikla].Cijena, proizvodi[imeArtikla].Rok));
           // proizvodi.Remove(item.Key);
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
static void PromjenaCijene(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
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
                UrediArtikal(proizvodi);
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

    PromjenaCijene(proizvodi);
}static void Popusti(Dictionary<string, (int Kolicina, float Cijena, DateTime Rok)> proizvodi)
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

    PromjenaCijene(proizvodi);
}
static void Radnici(Dictionary<string, DateTime> radniciDictionary)
{
    Console.Clear();
    Console.WriteLine("1 - Unos radnika");
    Console.WriteLine("2 - Brisanje radnika");
    Console.WriteLine("3 - Uredjivanje radnika");
    Console.WriteLine("4 - Ispis");
    Console.WriteLine("0 - Povratak na glavni izbornik");
    int izbor = GetInt();

    switch (izbor)
    {
        case 1:
            UnosRadnika(radniciDictionary);
            break;
        case 2:
            BrisanjeRadnika(radniciDictionary);
            break;
        case 3:
            UrediRadnika(radniciDictionary);
            break;
        case 4:
            Ispis(radniciDictionary);
            break;
        case 0:
            break;
        default:
            Console.WriteLine("Krivi unos");
            break;
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
  
    Radnici(radniciDictionary);

}
static void BrisanjeRadnika(Dictionary<string, DateTime> radniciDictionary)
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
                Radnici(radniciDictionary);
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
static void UrediRadnika(Dictionary<string, DateTime> radniciDictionary)
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
                Radnici(radniciDictionary);
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

static void Ispis(Dictionary<string, DateTime> radniciDictionary)
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
                Radnici(radniciDictionary);
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
            Console.WriteLine("Promjena napravljena");
            return true;
        }
        else if (userInput.ToLower() == "n")
        {
            Console.WriteLine("Promjena se nece izvrsiti");
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
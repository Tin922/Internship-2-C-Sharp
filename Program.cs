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

Izbornik();


int izbor = int.Parse(Console.ReadLine());


switch (izbor)
{
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
static void Radnici(Dictionary<string, DateTime> radniciDictionary)
{
    Console.Clear();
    Console.WriteLine("1 - Unos radnika");
    Console.WriteLine("2 - Brisanje radnika");
    Console.WriteLine("3 - Uredjivanje radnika");
    Console.WriteLine("4 - Ispis");
    int izbor = int.Parse(Console.ReadLine());
   
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
    else Console.WriteLine("Radnja se nece izvrsiti");


}
static void BrisanjeRadnika(Dictionary<string, DateTime> radniciDictionary)
{
    string izbor = Console.ReadLine();
    switch (izbor)
    {
        case "a":
            PoImenu(radniciDictionary);
            break;
        case "b":
            StarijiOd65(radniciDictionary);
            break;
        default:
            Console.WriteLine("krivo slovo ste unijeli");
            break;
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
            }
            else Console.WriteLine("Radnik se nece izbrisati");
        }
    }
}
static void StarijiOd65(Dictionary<string, DateTime> radniciDictionary)
{
    if (AskUserToMakeChange())
    {
        var danas = DateTime.Now;
        foreach (var item in radniciDictionary)
        {
            int godine = danas.Year - item.Value.Year;
            if (godine >= 65)
            {
                radniciDictionary.Remove(item.Key);
                Console.WriteLine("Radnik " + item.Key + " je imao vise od 65 godina pa je izbirsan");
            }
        }
    }
    else Console.WriteLine("Radnici stariji od 65 godina se nece izbrisati");
}
static void UrediRadnika(Dictionary<string, DateTime> radniciDictionary)
{    
    string ime = GetStringFromUser();

    foreach (var item in radniciDictionary)
    {
        if (item.Key == ime)
        {
            Console.WriteLine("Sto zelite promijeniti?");
            Console.WriteLine("a - za ime");            
            Console.WriteLine("b - za datum rodjneja");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "a":
                    Console.WriteLine("upisite novo ime");
                    string novoIme = GetStringFromUser();
                    if (AskUserToMakeChange())
                    {
                        radniciDictionary.Add(novoIme, item.Value);
                        radniciDictionary.Remove(item.Key);
                        Console.WriteLine("Promjena je uspjesna");
                    }
                    else Console.WriteLine("Promjena se nece izvrsiti");
                    break;               
                case "b":
                    var dob = GetDateFromUser();
                    if (AskUserToMakeChange())
                    {
                        radniciDictionary[item.Key] = dob;
                        Console.WriteLine("Promjena je uspjesna");
                    }
                    else Console.WriteLine("Promjena se nece izvrsiti");
                    break;

                default:
                    Console.WriteLine("Krivi odabir.");
                    break;
            }
        }
        
        
            
        

    }
    Console.WriteLine("Ne postoji radnik s tim imenom");
}
static void Ispis(Dictionary<string, DateTime> radniciDictionary)
{
    Console.WriteLine("a - ispis svih radnika ");
    Console.WriteLine("b - rodjendan u ovom mjesecu");
    string izbor = Console.ReadLine();
    switch (izbor)
    {
        case "a":
            IspisSvihRadnika(radniciDictionary);
            break;
        case "b":
            RodjedanUOvomMjesecu(radniciDictionary);
            break;


        default:
            Console.WriteLine("krivi unos");
            break;
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

    do
    {
        Console.WriteLine("Upisite ime i prezime");
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
        Console.WriteLine("Unesite datum rođenja (format: dd/MM/yyyy):");

        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
        {
            return dob;
        }
        else
        {
            Console.WriteLine("Neuspješno unesen datuma rođenja. Molimo unesite ponovno datum rođenja.");
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
            Console.WriteLine("Promjen");
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
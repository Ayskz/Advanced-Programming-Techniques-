using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

public record City(string Name, int Population);
public record Person(string FirstName, string LastName, string City, int Height, string? Allergies);

//TASK 1: NUMBER QUERIES
public static class NumberQueries
{
    public static void Execute()
    {
        int[] numbers = [106, 104, 10, 5, 117, 174, 95, 61, 74, 145, 77, 95, 72, 59, 114, 95, 61, 116, 106, 66, 75, 85, 104, 62, 76, 87, 70, 17, 141, 39, 199, 91, 37, 139, 88, 84, 15, 166, 118, 54, 42, 123, 53, 183, 95, 101, 112, 26, 41, 135, 70, 48, 59, 69, 109, 93, 110, 153, 178, 117, 5];

        Console.WriteLine("\n=== TASK 1: NUMBER QUERIES ===");

        // a. Numbers > 80
        var numsAbove80 = numbers.Where(n => n > 80);
        Console.WriteLine("\nNumbers > 80:");
        Console.WriteLine(string.Join(", ", numsAbove80));

        // b. Ordered descending
        var descendingNums = numbers.OrderByDescending(n => n);
        Console.WriteLine("\nNumbers descending:");
        Console.WriteLine(string.Join(", ", descendingNums.Take(10)) + "..."); // Show first 10

        // c. Transform to strings
        var numberStrings = numbers.Select(n => $"Have number #{n}");
        Console.WriteLine("\nTransformed strings (first 5):");
        numberStrings.Take(5).ToList().ForEach(Console.WriteLine);

        // d. Count between 70-100
        var count70to100 = numbers.Count(n => n > 70 && n < 100);
        Console.WriteLine($"\nCount of numbers between 70-100: {count70to100}");
    }
}

//TASK 2: PERSON QUERIES
public static class PersonQueries
{
    public static void Execute()
    {
        // Initialize data
        City[] cities = [
            new City("Toronto", 100200),
            new City("Hamilton", 80923),
            new City("Ancaster", 4039),
            new City("Brantford", 500890)
        ];

        Person[] persons = [
                 new Person("Cedric","Coltrane","Toronto",157,null),
            new Person("Hank","Spencer","Peterborough",158,"Sulfa, Penicillin"),
            new Person("Sara","di","29",145,null),
            new Person("Daphne ","Seabright","Ancaster",146,null),
            new Person("Rick","Bennett","Ancaster",220,null),
            new Person("Amy","Leela","Hamilton",172,"Penicillin"),
            new Person("Woody","Bashir","Barrie",153,null),
            new Person("Tom", "Halliwell","Hamilton",179,"Codeine, Sulfa"),
            new Person("Rachel ","Winterbourne","Hamilton",163,null),
            new Person("John","West","Oakville",138,null),
            new Person("Jon","Doggett","Hamilton",194,"Peanut Oil"),
            new Person("Angel","Edwards","Brantford",176,null),
            new Person("Brodie","Beck","Carlisle",157,null),
            new Person("Beanie","Foster","Ancaster",154,"Ragweed, Codeine"),
            new Person("Nino","Andrews","Hamilton",186,null),
            new Person("John","Farley","Hamilton",213,null),
            new Person("Nea","Kobayakawa","Toronto",147,null),
            new Person("Laura","Halliwell","Brantford",146,null),
            new Person("Lucille","Maureen","Hamilton",184,null),
            new Person("Jim","Thoma","Ottawa",173,null),
            new Person("Roderick","Payne","Halifax",58,null),
            new Person("Sam","Threep","Hamilton",199,null),
            new Person("Bertha","Crowley","Delhi",125,"Peanuts, Gluten"),
            new Person("Roland","Edge","Brantford",199,null),


            new Person("Don","Wiggum","Hamilton",189,null),
            new Person("Anthony","Maxwell","Oakville",92,null),
            new Person("James","Sullivan","Delhi",139,null),
            new Person("Anne","Marlowe","Pickering",165,"Peanut Oil"),
            new Person("Kelly","Hamilton","Stoney",84,null),
            new Person("Charles","Andonuts","Hamilton",62,null),
            new Person("Temple ","Russert","Hamilton",166,"Sulphur"),
            new Person("Don","Edwards","Hamilton",215,null),
            new Person("Alice","Donovan","Hamilton",167,null),
            new Person("Stone","Cutting","Hamilton",110,null),
            new Person("Neil","Allan","Cambridge",203,null),
            new Person("Cross","Gordon","Ancaster",125,null),
            new Person("Phoebe","Bigelow","Thunder",183,null),
            new Person("Harry","Kuramitsu","Hamilton",210, null)
        ];

        Console.WriteLine("\n=== TASK 2: PERSON QUERIES ===");

        // a. Select by height
        IEnumerable<Person> GetPersonsByHeight(int height) =>
            persons.Where(p => p.Height == height);

        Console.WriteLine("\nPersons with height 175:");
        GetPersonsByHeight(175).ToList().ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName}"));

        // b. Name formatting
        var formattedNames = persons.Select(p => $"{p.FirstName[0]}. {p.LastName}");
        Console.WriteLine("\nFormatted names (first 5):");
        formattedNames.Take(5).ToList().ForEach(Console.WriteLine);

        // c. Distinct allergies
        var allergies = persons
            .Where(p => p.Allergies != null)
            .SelectMany(p => p.Allergies!.Split(','))
            .Select(a => a.Trim())
            .Distinct();
        Console.WriteLine("\nAll unique allergies:");
        allergies.ToList().ForEach(Console.WriteLine);

        // d. Cities starting with 'H'
        int hCities = persons.Select(p => p.City)
                           .Where(c => c.StartsWith("H"))
                           .Distinct()
                           .Count();
        Console.WriteLine($"\nCities starting with 'H': {hCities}");

        // e. Join with populated cities
        var bigCityPeople = from p in persons
                            join c in cities on p.City equals c.Name
                            where c.Population > 100000
                            select p;
        Console.WriteLine("\nPeople in big cities:");
        bigCityPeople.ToList().ForEach(p => Console.WriteLine($"{p.FirstName} ({p.City})"));

        // f. Manual city filter
        var targetCities = new List<string> { "Toronto", "Hamilton" };
        var inTarget = persons.Where(p => targetCities.Contains(p.City));
        Console.WriteLine($"\nPeople in target cities: {inTarget.Count()}");
    }
}

//TASK 3: XML CONVERSION
public static class XmlGenerator
{
    public static void GenerateXml()
    {
        // Sample data
        Person[] persons = [
            new Person("John", "Doe", "Toronto", 180, "Peanuts"),
            new Person("Jane", "Smith", "Hamilton", 165, null)
        ];

        var xml = new XElement("Persons",
            from p in persons
            select new XElement("Person",
                new XElement("FirstName", p.FirstName),
                new XElement("LastName", p.LastName),
                new XElement("City", p.City),
                new XElement("Height", p.Height),
                new XElement("Allergies", p.Allergies ?? "None")
            )
        );

        xml.Save("persons.xml");
        Console.WriteLine("\n=== XML Generated ===");
        Console.WriteLine("Saved to 'persons.xml' in your project folder");
    }
}

//MAIN PROGRAM
class Program
{
    static void Main()
    {
        NumberQueries.Execute();
        PersonQueries.Execute();
        XmlGenerator.GenerateXml();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
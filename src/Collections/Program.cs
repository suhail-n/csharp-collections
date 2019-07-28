using System.Collections.Immutable;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //Your code goes here
            var filePath = @"pop-by-largest-final.csv";
            // // ImportDataIntoArray(filePath);
            // ImportDataIntoArrayList(filePath);
            // TestArrayLists();
            // CountryDictionary();
            // ImportDataIntoDictionary(filePath);
            // TestLinqWithList(filePath);
            // ImportIntoSortedDictionary(filePath);
            ImportDataIntoImmutableArrayList(filePath);
        }

        /// <summary>
        /// Test array lists
        /// </summary>
        static void TestArrayLists()
        {
            List<string> daysOfWeek = new List<string>();
            daysOfWeek.Add("Monday");
            // add a range of items to the list in one element
            daysOfWeek.AddRange(new String[] { "Tuesday, Wednesday" });
            daysOfWeek.ForEach(ele => System.Console.WriteLine($"Day of week: {ele}"));

            List<int> nums = new List<int>
            {
                1,
                2,
                44,
                32
            };
            nums.ForEach(ele => System.Console.WriteLine($"Num: {ele}"));
        }

        /// <summary>Test arrays</summary> 
        static void TestArrays()
        {
            // String[] daysOfWeek = new String[2];
            // daysOfWeek[0] = "1";
            // daysOfWeek[1] = "2";

            String[] daysOfWeek = {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday"
            };

            // enumerate array for loop
            for (var i = 0; i < daysOfWeek.Length; i++)
            {
                System.Console.WriteLine($"For-Loop[{i}]={daysOfWeek[i]}");
            }
            // enumerate array foreach loop
            int j = 0;
            foreach (var dayOfWeek in daysOfWeek)
            {
                System.Console.WriteLine($"Foreach-Loop[{j}]={dayOfWeek}");
                j++;
            }
        }

        /// <summary> Import data from a source into an array </summary>
        static void ImportDataIntoArray(string filePath)
        {
            CsvReader reader = new CsvReader(filePath);
            Country[] countries = reader.ReadFirstNCountries(10);
            foreach (var country in countries)
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation((int)country.Population).PadLeft(15)}: {country.Name}");
            }

        }

        /// <summary>
        /// use lists to import data from the csv and enumerate through
        /// </summary>
        /// <param name="filePath"></param>
        static void ImportDataIntoArrayList(string filePath)
        {
            CsvReader reader = new CsvReader(filePath);
            List<Country> countries = (List<Country>)reader.ReadAllCountries();
            var lilliput = new Country
            {
                Name = "Lilliput",
                Code = "LIL",
                Region = "Somewhere",
                Population = 2000000
            };
            var index = countries.FindIndex(country => country.Population < 2000000);
            countries.Insert(index, lilliput);
            countries.RemoveAt(index);

            // remove all items in the list where the code is equal to "FIN"
            countries.RemoveAll(x => x.Code.Equals("FIN"));

            foreach (var country in countries)
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            }
        }
        static void ImportDataIntoImmutableArrayList(string filePath)
        {
            CsvReader reader = new CsvReader(filePath);
            List<Country> countries = (List<Country>)reader.ReadAllCountries();
            var countriesImmutableBuilder = ImmutableArray.CreateBuilder<Country>();
            countries.ForEach(x => countriesImmutableBuilder.Add(x));
            var countriesImmutable = countriesImmutableBuilder.ToImmutableArray();

            foreach (var country in countriesImmutable)
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            }
        }

        /// <summary>
        /// Use a Dictionary to store countries and enumerate through
        /// </summary>
        static void CountryDictionary()
        {
            Country norway = new Country("Norway", "NOR", "Europe", 5_282_223);
            Country finland = new Country("Finland", "FIN", "Europe", 5_111_303);
            var countries = new Dictionary<string, Country>();
            countries.Add(norway.Code, norway);
            countries.Add(finland.Code, finland);

            // This will throw an exception if the key does not exist
            string finlandCode = countries["NOR"].Code;

            Country countryNor = null;
            // returns bool if the key does not exist rather than throwing an exception
            bool exists = countries.TryGetValue("NOR", out countryNor);
            if (exists)
                System.Console.WriteLine("Code exists");
            else
                System.Console.WriteLine("Code does not exist");


            countries["NOR2"] = norway;


            // using collection initializer instead of above
            // does the same as above in the background
            var countries2 = new Dictionary<string, Country>
            {
                {norway.Code, norway},
                {finland.Code, finland}
            };

            // enumerating through a dictionary
            // KeyValuePair is a simple struct which only holds key and value property
            foreach (KeyValuePair<string, Country> country in countries)
            {
                System.Console.WriteLine($"{country.Key}: {country.Value.Name}");
            }

            System.Console.WriteLine("-------------------------------");
            // enumerate through all values in the countries dict
            foreach (Country country in countries.Values)
            {
                System.Console.WriteLine($"{country.Region}");
            }

            System.Console.WriteLine("-------------------------------");
            // enumerate through all Keys in the countries dict
            foreach (string code in countries.Keys)
            {
                System.Console.WriteLine($"{code}");
            }

            System.Console.WriteLine("------------ReadAllCountriesDict-------------------");

        }

        /// <summary>
        /// Import csv into a dictionary
        /// </summary>
        /// <param name="filePath"></param>
        static void ImportDataIntoDictionary(string filePath)
        {
            var csvReader = new CsvReader(filePath);
            var countries = csvReader.ReadAllCountriesDict();

            System.Console.WriteLine("Enter a country code to look up: ");
            string userInput = Console.ReadLine();
            bool countryFound = countries.TryGetValue(userInput, out Country country);
            if (!countryFound)
                System.Console.WriteLine($"Country Code {userInput} does not exist in record");
            else
                System.Console.WriteLine($"Found Country: {country.Name} - {PopulationFormatter.FormatPopulation(country.Population)}");
        }

        /// <summary>
        /// Testing out LINQ features
        /// </summary>
        /// <param name="filePath"></param>
        static void TestLinqWithList(string filePath)
        {
            System.Console.WriteLine("Using LINQ");
            var csv = new CsvReader(filePath);
            var countries = (List<Country>)csv.ReadAllCountries();
            System.Console.WriteLine("--------------First 5 using Take() ----------------");
            foreach (var country in countries.Take(5))
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation((int)country.Population).PadLeft(15)}: {country.Name}");
            }

            System.Console.WriteLine("-------------- Order elements by Name. using .OrderBy(lambda) ----------------");
            foreach (var country in countries.Take(5).OrderBy(x => x.Name))
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation((int)country.Population).PadLeft(15)}: {country.Name}");
            }

            System.Console.WriteLine("-------------- Language Integrated Query(LINQ) Query Syntax ----------------");
            // LINQ Query is good for long complex queries

            // both filters are doing the same
            var filteredCountries = countries.Where(x => !x.Code.Contains("FIN"));
            var filteredCountries2 = from country in countries
                                     where !country.Code.Contains("FIN")
                                     select country;
            foreach (var country in filteredCountries2.Take(5).OrderBy(x => x.Name))
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation((int)country.Population).PadLeft(15)}: {country.Name}");
            }

        }

        static void ImportIntoSortedDictionary(string filePath)
        {
            System.Console.WriteLine("-------------Using Sorted Dictionary-------------");

            // sortedli is also a dictionary with key value pairs
            // var sortedli = new SortedList<string, Country>();
            // sortedli.Add("NOR", new Country());

            var csv = new CsvReader(filePath);
            var countries = csv.ReadAllCountries();
            var countriesSorted = new SortedDictionary<string, Country>();
            foreach (var country in countries)
            {
                countriesSorted.Add(country.Code, country);
            }
            foreach (var country in countriesSorted.Take(10))
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation((int)country.Value.Population).PadLeft(15)}: {country.Value.Code}");
            }
        }
    }
}

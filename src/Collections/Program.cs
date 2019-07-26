using System.Collections.Generic;
using System;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"pop-by-largest-final.csv";
            // ImportDataIntoArray(filePath);
            ImportDataIntoArrayList(filePath);
            TestArrayLists();

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
            foreach (var country in countries)
            {
                System.Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            }
        }
    }
}

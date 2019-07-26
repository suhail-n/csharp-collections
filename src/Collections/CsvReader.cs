using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
namespace Collections
{
    public class CsvReader
    {
        private string _csvFilePath;
        public CsvReader(string csvFilePath)
        {
            this._csvFilePath = csvFilePath;
        }
        public Country[] ReadFirstNCountries(int nCountries)
        {
            Country[] countries = new Country[nCountries];
            using (StreamReader sr = new StreamReader(this._csvFilePath))
            using (var csv = new CsvHelper.CsvReader(sr))
            {
                for (int i = 0; i < nCountries; i++)
                {
                    var country = csv.GetRecord<Country>();
                    countries[i] = country;
                }

            }
            return countries;
        }
        public IList<Country> ReadAllCountries()
        {
            IList<Country> countries = new List<Country>();
            using (StreamReader sr = new StreamReader(this._csvFilePath))
            using (var csv = new CsvHelper.CsvReader(sr))
            {
                csv.Configuration.IgnoreBlankLines = false;
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.TypeConverterCache.AddConverter<int>(new CustomIntConverter());
                // var records = csv.GetRecords<Country>();
                while (csv.Read())
                {
                    Country country = csv.GetRecord<Country>();
                    countries.Add(country);
                }
            }
            return countries;
        }
        // public Country ReadCountryFromCsvLine(string csvLine)
        // {
        //     // string[] parts = csvLine.Split(",");
        //     // Country country = new Country(parts[0], parts[1], parts[2], int.Parse(parts[3]));
        //     // return country;

        // }
    }
}
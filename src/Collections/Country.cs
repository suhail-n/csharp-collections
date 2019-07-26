using CsvHelper.Configuration.Attributes;

namespace Collections
{
    public class Country
    {
        [Name("Country Name")]
        public string Name { get; set; }

        [Name(" Country Code")]
        public string Code { get; set; }

        [Name(" Continent")]
        public string Region { get; set; }

        [Optional]
        [Name(" Population 2017")]
        public int Population { get; set; }

        public Country() { }
        public Country(string name, string code, string region, int population)
        {
            this.Name = name;
            this.Code = code;
            this.Region = region;
            this.Population = population;
        }
    }
}
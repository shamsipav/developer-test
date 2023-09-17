namespace SecondTask.Models
{
    public class Country
    {
        public CountryName? Name { get; set; }
        public string[]? Capital { get; set; }
        public string? Region { get; set; }
        public Dictionary<string, string> Languages { get; set; }
    }

}
